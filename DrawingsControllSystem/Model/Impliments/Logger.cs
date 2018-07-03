using System;
using System.IO;
using System.Text;
using DrawingsControllSystem.Common;
using DrawingsControllSystem.Model.Interfaces;

namespace DrawingsControllSystem.Model.Impliments
{
    public class Logger : BindableBase, ILogger
    {
        private string log;

        public string Log
        {
            get => log;
            private set => SetProperty(ref log, value);
        }

        /// <summary>
        /// Вывод информации о событиях в программе
        /// </summary>
        /// <param name="message">Сообщение, которое следует вывести пользователю</param>
        public void Logging(string message)
        {
            StringBuilder sb = new StringBuilder(Log);
            sb.AppendFormat("[{0:T}]: {1}", DateTime.Now, message);
            sb.Append(Environment.NewLine);
            Log = sb.ToString();
        }

        /// <summary>
        /// Сохранить файл журнала событий в файл
        /// </summary>
        /// <param name="directory">Директория, в которую следует сохранить файл журнала событий</param>
        public void SaveToFile(DirectoryInfo directory)
        {
            if (!directory.Exists)
                throw new DirectoryNotFoundException(directory.FullName);

            string file = Path.Combine(directory.FullName, "log.txt");

            if (File.Exists(file))
                File.Delete(file);

            Stream stream = File.Create(file);
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(Log);
            writer.Close();
            stream.Close();

            Logging($"Файл журнала событий успешно сохранен: [ {file} ]");
        }
    }
}