using System;
using System.IO;
using DrawingsControllSystem.Model.Common;
using DrawingsControllSystem.Model.Interfaces;

namespace DrawingsControllSystem.Model.Impliments
{
    public class Creator : ICreator
    {
        private readonly IStoragekeeper storagekeeper;
        private readonly ILogger logger; 

        /// <summary>
        ///   Инициализирует новый экземпляр класса <see cref="Creator" />.
        /// </summary>
        public Creator(IStoragekeeper storagekeeper, ILogger logger, CreateStrategy strategy = null)
        {
            this.storagekeeper = storagekeeper;
            Strategy = strategy ?? new TiffCreatorStrategy();
            this.logger = logger;
        }

        public CreateStrategy Strategy { get; set; }

        public void Create(FileInfo file)
        {
            Create(file, false);
        }

        public void Create(FileInfo file, bool replace)
        {
            if (Strategy == null)
            {
                logger.Logging($"Стратегия создания файлов не определенна!");
                logger.Logging($"Проверьте, выбран ли тип отслеживаемых файлов.");
                return;
            }

            try
            {
                logger.Logging($"Начало создание объекта чертежа");
                Drawing drawing = Strategy.Proccess(file: file);

                if (drawing.IsMarker)
                {
                    storagekeeper.SendToBank(file.Directory);
                    return;
                }

                storagekeeper.Add(drawing, replace);
            }
            catch (Exception e)
            {
                logger.Logging($"Ошибка на этапе создания чертежа!");
                logger.Logging(e.Message);
                return;
            }
        }
    }
}