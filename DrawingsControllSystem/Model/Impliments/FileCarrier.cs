using System.Collections.Generic;
using System.IO;
using System.Linq;
using DrawingsControllSystem.Model.Common;
using DrawingsControllSystem.Model.Interfaces;

namespace DrawingsControllSystem.Model.Impliments
{
    public class FileCarrier : IFileCarrier
    {
        private readonly ILogger logger;

        public FileCarrier(ILogger logger)
        {
            this.logger = logger;
        }

        public bool Transfer(DirectoryInfo to, List<Drawing> drawings)
        {
            if (!to.Exists)
            {
                logger.Logging($"Директория не найдена [ {to.Name} ]");
                return false;
            }

            if (drawings == null || drawings.Count == 0)
            {
                logger.Logging($"Коллекция чертежей Null или пустая");
                return false;
            }

            // Todo: Сделать асинхронный метод
            drawings.AsParallel().ForAll(drawing => TransferHelper(to, drawing));

            logger.Logging($"Перемещение файлов в директорию [ {to.Name} ] успешно завершено");
            return true;
        }

        private bool TransferHelper(DirectoryInfo to, Drawing drawing)
        {
            string name = drawing.Path.Name;
            string newPath = Path.Combine(to.FullName, name);

            File.Move(drawing.Path.FullName, newPath);

            if (File.Exists(newPath))
            {
                drawing.Path = new FileInfo(newPath);
                logger.Logging($"Файл [ {name} ] был успешно перемещен");
            }
            else
            {
                logger.Logging($"Ошибка при меремещении файла [ {name} ]");
                return false;
            }

            return true;
        }
    }
}