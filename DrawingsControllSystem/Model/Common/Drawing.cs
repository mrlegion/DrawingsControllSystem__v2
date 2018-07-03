using System.IO;

namespace DrawingsControllSystem.Model.Common
{
    public class Drawing
    {
        /// <summary>
        /// Получение или установка наименование формата изображения
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Получение или установка полного пути до физического файла
        /// </summary>
        public FileInfo Path { get; set; }

        /// <summary>
        /// Получение или установка значение ширины изображения 
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Получение или установка значения вызоты изображения
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Получение или установка значения площади изображения
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Получение или установка тип <see cref="OrientationType"/> для изображения
        /// </summary>
        public OrientationType Orientation { get; set; }

        /// <summary>
        /// Получение или установка номера содкржащих в изображении стандартного формата а4
        /// </summary>
        public double Include { get; set; }

        /// <summary>
        /// Получение или установка статуса маркера для изображения; 
        /// если возвращенно FALSE - изображение не маркер; 
        /// если возвращенно TRUE  - изображение маркер;
        /// </summary>
        public bool IsMarker { get; set; }
    }
}