using System.IO;
using DrawingsControllSystem.Common;
using DrawingsControllSystem.Model.Common;
using DrawingsControllSystem.Model.Impliments;
using DrawingsControllSystem.Model.Interfaces;

namespace DrawingsControllSystem.Model
{
    public class WatcherController : BindableBase
    {
        private readonly ILogger logger;
        private readonly IWatcher watcher;
        private readonly IStoragekeeper storagekeeper;
        private readonly IManager manager;

        private DirectoryInfo directory;
        private FileExtansion extansion;

        public DirectoryInfo Directory
        {
            get => directory;
            set => SetProperty(ref directory, value);
        }

        public FileExtansion Extansion
        {
            get => extansion;
            set
            {
                SetProperty(ref extansion, value);
                logger.Logging($"Задание нового типа отслеживания файлов: [ {value.ToString()} ]");
            }
        }

        public string Log => logger.Log;

        public WatcherController(IWatcher watcher, ILogger logger, IStoragekeeper storagekeeper, IManager manager)
        {
            this.watcher = watcher;
            this.logger = logger;
            this.storagekeeper = storagekeeper;
            this.manager = manager;

            ((Logger)logger).PropertyChanged += (sender, args) => RaisePropertyChanged(args.PropertyName);
        }

        public void Run()
        {
            if (Extansion == FileExtansion.None)
                Extansion = FileExtansion.Tiff;
            watcher.Run(Directory, Extansion);
        }

        public void Stop()
        {
            watcher.Stop();
        }

        public void SafeLogToFile()
        {
            logger.SaveToFile(Directory);
        }

        public void ChangeFolder()
        {
            logger.Logging("Принудительная смена папки вызванная пользователем");

            if (storagekeeper.Count != 0)
            {
                storagekeeper.SendToBank(Directory);
                logger.Logging("Принудительная смена папки завершена успешна");
                return;
            }
            else if (storagekeeper.Count == 0)
            {
                logger.Logging("Принудительная смена папки прервана! В коллекции нет ни одного файла");
                return;
            }

            logger.Logging("Принудительная смена папки прервана! Неизвестная ошибка!");
        }

        public void Explode()
        {
            logger.Logging("Запуск опции деления по форматам");
            manager.ExplodeAsync(Directory);
            //logger.Logging("Деление по форматам успешно завершенно");
        }

        public void ShowInformation()
        {
            manager.ShowInformation();
        }

        public void Logging(string message)
        {
            logger.Logging(message);
        }

    }
}