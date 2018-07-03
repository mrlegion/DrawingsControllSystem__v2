using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.SqlServer.Server;

namespace DrawingsControllSystem.Model.Common
{
    public abstract class CreateStrategy
    {
        /// <summary>
        /// Константа для значения дюйма в мм
        /// </summary>
        protected double inch = 25.4d;

        /// <summary>
        /// Объект чертежа для временного создания
        /// </summary>
        protected Drawing drawing;

        /// <summary>
        /// Экземпляр объекта <see cref="Bitmap"/> для заполнения объекта чертежа
        /// </summary>
        protected Bitmap bitmap;

        /// <summary>
        /// Информация о изображении для обработки в объект <see cref="Drawing"/>
        /// </summary>
        protected FileInfo file;

        /// <summary>
        /// Вспомогательный объект с информацией о форматах чертежей
        /// </summary>
        protected Info info;

        /// <summary>
        /// Описание метода открытия изображения 
        /// </summary>
        protected virtual void OpenImage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Описание метода установки размеров изображения
        /// </summary>
        protected virtual void SetSize()
        {
            drawing = new Drawing()
            {
                Orientation = (bitmap.Width > bitmap.Height) ? OrientationType.Horizontal : OrientationType.Vertical,
                Path = file,
                Format = null,
            };

            switch (drawing.Orientation)
            {
                case OrientationType.Horizontal:
                    drawing.Width = bitmap.Width / bitmap.HorizontalResolution * inch;
                    drawing.Height = bitmap.Height / bitmap.VerticalResolution * inch;
                    break;
                case OrientationType.Vertical:
                    drawing.Width = bitmap.Height / bitmap.VerticalResolution * inch;
                    drawing.Height = bitmap.Width / bitmap.HorizontalResolution * inch;
                    break;
            }

            // Установка значения площади
            drawing.Area = Math.Floor(drawing.Width * drawing.Height);
        }

        /// <summary>
        /// Описание метода определения формата для изображения
        /// </summary>
        protected virtual void SetFormat()
        {
            // Проверка изображения на стандартные форматы из бд
            foreach (Format format in info.Formats)
            {
                // Первая проверка
                bool low = drawing.Area >= format.LowArea;
                bool up = drawing.Area <= format.UpArea;

                if (low && up)
                {
                    bool widthCheck = drawing.Width >= format.Width - 10 && drawing.Width <= format.Width + 10;
                    bool heightCheck = drawing.Height >= format.Height - 10 && drawing.Height <= format.Height + 10;

                    // Вторая проверка
                    if (widthCheck && heightCheck)
                    {
                        // Заполение объекта Drawing
                        drawing.Width = format.Width;
                        drawing.Height = format.Height;
                        drawing.Area = format.Area;
                        drawing.Format = format.Name;
                        drawing.Include = format.Includes;

                        // Прерываем цикл так как формат найден
                        break;
                    }
                }
            }

            if (drawing.Format != null) return;

            drawing.Area = Math.Round(drawing.Area);
            drawing.Width = Math.Round(drawing.Width);
            drawing.Height = Math.Round(drawing.Height);
            drawing.Format = $"{drawing.Width}x{drawing.Height}";
            double include = drawing.Area / 62370.0d;
            drawing.Include = (include % 1) > 0.2 ? Math.Ceiling(include) : Math.Round(include);
        }

        /// <summary>
        /// Описание метода для проверки принадлежности изображения к маркеру
        /// </summary>
        protected virtual void CheckOnMark()
        {
            // Проверка на соответсвию маркеру
            if (drawing.Format == "a3")
            {
                // Проверка на ориентацию страницы,
                // Если вертикальная, то ее нужно повернуть
                if (drawing.Orientation == OrientationType.Vertical)
                    bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // Проверка на центральную точну, если ее нет то это не маркер и далее код не делать
                Marker marker = info.Markers.FirstOrDefault(m => m.Position == MarkerPosition.Center);

                if (marker == null)
                    return;

                Color[,] colors = new Color[10, 10];

                int x = (int)Math.Floor(marker.X * bitmap.HorizontalResolution / inch) - (colors.GetLength(0) / 2);
                int y = (int)Math.Floor(marker.Y * bitmap.VerticalResolution / inch) - (colors.GetLength(1) / 2);

                for (int i = 0; i < colors.GetLength(0); i++)
                    for (int j = 0; j < colors.GetLength(1); j++)
                        colors[i, j] = bitmap.GetPixel(x + j, y + i);

                int count = 0;

                foreach (Color color in colors)
                    if (color.Name == "ff000000")
                        count++;

                drawing.IsMarker = count > colors.Length * 0.8;
            }
        }

        /// <summary>
        /// Описание метода для закрытия изображения
        /// </summary>
        protected virtual void CloseImage()
        {
            bitmap.Dispose();
            bitmap = null;
        }

        /// <summary>
        /// Обработка изображения перевода изображения чертежа типа <see cref="Drawing"/>
        /// </summary>
        /// <param name="file">Ссылка до изображения</param>
        /// <returns>Новый экземпляр класса <see cref="Drawing"/></returns>
        public Drawing Proccess(FileInfo file)
        {
            this.file = file;
            info = new Info();

            OpenImage();
            SetSize();
            SetFormat();
            CheckOnMark();
            CloseImage();

            return drawing;
        }
    }
}