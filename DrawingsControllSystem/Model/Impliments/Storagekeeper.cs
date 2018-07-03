using System.Collections.Generic;
using System.IO;
using System.Linq;
using DrawingsControllSystem.Model.Common;
using DrawingsControllSystem.Model.Interfaces;

namespace DrawingsControllSystem.Model.Impliments
{
    public class Storagekeeper : IStoragekeeper
    {
        private readonly List<Drawing> drawings;
        private readonly IFolderCreator folderCreator;
        private readonly IFileCarrier carrier;
        private readonly IManager manager;
        private readonly ILogger logger;

        public int Count => drawings.Count;

        /// <summary>
        ///   Инициализирует новый экземпляр класса <see cref="Storagekeeper" />.
        /// </summary>
        public Storagekeeper(ILogger logger, IFolderCreator folderCreator, IFileCarrier carrier, IManager manager)
        {
            this.logger = logger;
            this.folderCreator = folderCreator;
            this.carrier = carrier;
            this.manager = manager;
            drawings = new List<Drawing>();
        }

        public void Add(Drawing item)
        {
            Add(item, false);
        }

        public void Add(Drawing item, bool replace)
        {
            if (replace)
                drawings.RemoveAt(Count - 1);

            drawings.Add(item);

            logger.Logging($"Файл [ {item.Path.Name} ] добавлен в коллекцию");
            logger.Logging($"Формат файла: {item.Format}");
            logger.Logging($"Общее количество файлов в коллекции: {Count}");
        }

        public void Clear()
        {
            drawings.Clear();
            logger.Logging("Коллекция чертежей очищенна.");
        }

        public void SendToBank(DirectoryInfo source)
        {
            folderCreator.Create(source);

            if (carrier.Transfer(folderCreator.Directory, drawings))
            {
                manager.Add(folderCreator.Directory.Name, drawings);
                Clear();
            }
        }
    }
}