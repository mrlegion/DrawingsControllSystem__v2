using System.IO;

namespace DrawingsControllSystem.Model.Interfaces
{
    public interface ILogger
    {
        string Log { get; }

        /// <summary>
        /// Вывод информации о событиях в программе
        /// </summary>
        /// <param name="message">Сообщение, которое следует вывести пользователю</param>
        void Logging(string message);

        /// <summary>
        /// Сохранить файл журнала событий в файл
        /// </summary>
        /// <param name="directory">Директория, в которую следует сохранить файл журнала событий</param>
        void SaveToFile(DirectoryInfo directory);
    }
}