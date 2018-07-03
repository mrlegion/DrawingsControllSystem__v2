using System.IO;
using System.Text.RegularExpressions;
using DrawingsControllSystem.Model.Common;
using DrawingsControllSystem.Model.Interfaces;

namespace DrawingsControllSystem.Model.Impliments
{
    public class Watcher : IWatcher
    {
        private FileSystemWatcher _fileSystemWatcher;
        private FileExtansion _extension;

        private readonly ICreator _creator;
        private readonly ILogger _logger;

        private string _name;

        /// <summary>
        ///   Инициализирует новый экземпляр класса <see cref="Watcher" />.
        /// </summary>
        public Watcher(ICreator creator, ILogger logger)
        {
            _creator = creator;
            _logger = logger;
        }

        public void Run(DirectoryInfo directory, FileExtansion fileExtension)
        {
            if (directory == null || !directory.Exists)
            {
                _logger.Logging("Выбранная директория не найдена!");
                return;
            }

            if (fileExtension == FileExtansion.None)
            {
                _logger.Logging($"Для запуска отслеживания директории необходимо указать расширение отслеживаемых файлов");
                return;
            }

            if (_fileSystemWatcher == null)
                InitWatcher();

            this._extension = fileExtension;

            _fileSystemWatcher.Path = directory.FullName;
            _fileSystemWatcher.EnableRaisingEvents = true;

            _logger.Logging($"Отслеживание директории [ {directory.FullName} ] началось!");
        }

        private void InitWatcher()
        {
            _fileSystemWatcher = new FileSystemWatcher()
            {
                Filter = "*.*",
                IncludeSubdirectories = false,
                NotifyFilter = NotifyFilters.FileName |
                               NotifyFilters.DirectoryName |
                               NotifyFilters.CreationTime,
            };

            _fileSystemWatcher.Created += OnCreated;
        }

        private void OnCreated(object sender, FileSystemEventArgs args)
        {
            if (!CheckExtension(Path.GetExtension(args.FullPath))) { return; }

            _logger.Logging($"Найден новый файл: [ {args.Name} ]");

            FileInfo file = new FileInfo(args.FullPath);

            // Todo: Сделать не так колхозно
            while (IsLocked(file))
            {
                // logger.Logging($"Файл заблокирован");
            }

            _creator.Create(file, _name == file.Name);

            _name = file.Name;
        }

        private bool IsLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }

            return false;
        }

        private bool CheckExtension(string ext)
        {
            bool result = false;

            switch (_extension)
            {
                case FileExtansion.Tiff:
                    result = Regex.IsMatch(ext, "tiff$|tif$", RegexOptions.IgnoreCase);
                    break;
                case FileExtansion.Pdf:
                    result = Regex.IsMatch(ext, "pdf$", RegexOptions.IgnoreCase);
                    break;
                case FileExtansion.Jpeg:
                    result = Regex.IsMatch(ext, "jpeg$|jpg$", RegexOptions.IgnoreCase);
                    break;
            }

            return result;
        }

        public void Stop()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            _logger.Logging($"Отслеживание директории завершено!");
        }
    }
}
