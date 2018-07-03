using System.IO;
using DrawingsControllSystem.Model.Interfaces;

namespace DrawingsControllSystem.Model.Impliments
{
    public class FolderCreator : IFolderCreator
    {
        private readonly ILogger logger;

        public FolderCreator(ILogger logger)
        {
            this.logger = logger;
        }

        public DirectoryInfo Directory { get; private set; }

        public void Create(DirectoryInfo source)
        {
            if (!source.Exists)
            {
                logger.Logging($"Выбранная директория [ {source.Name} ] не найдена");
                return;
            }

            int count = 0;
            string newDirctory;

            do
                newDirctory = Path.Combine(source.FullName, (++count).ToString());
            while (System.IO.Directory.Exists(newDirctory));

            Directory = new DirectoryInfo(newDirctory);
            Directory.Create();

            logger.Logging($"Новая директория [ {Directory.Name} ] была создана");
        }
    }
}