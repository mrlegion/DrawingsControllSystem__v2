using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DrawingsControllSystem.Model.Common;
using DrawingsControllSystem.Model.Interfaces;

namespace DrawingsControllSystem.Model.Impliments
{
    public class Exploder : IExploder
    {
        private readonly ILogger logger;

        public Exploder(ILogger logger)
        {
            this.logger = logger;
        }

        public void Explode(DirectoryInfo source, Dictionary<string, List<Drawing>> drawings)
        {
            if (drawings == null || drawings.Count == 0)
            {
                logger.Logging("Список чертежей для деления не может быть пустым или иметь значение Null");
                return;
            }

            string name = "Папка деления";
            DirectoryInfo folder = new DirectoryInfo(Path.Combine(source.FullName, name));

            while (folder.Exists)
            {
                string path = Path.Combine(source.FullName, name.GetRandomPrefix());
                folder = new DirectoryInfo(path);
            }

            if (!folder.Exists)
                folder.Create();

            // Сортировка по формату
            Dictionary<string, List<Drawing>> groups = new Dictionary<string, List<Drawing>>();

            drawings.AsParallel().ForAll(pair =>
            {
                foreach (Drawing drawing in pair.Value)
                {
                    if (groups.ContainsKey(drawing.Format))
                        groups[drawing.Format].Add(drawing);
                    else
                        groups.Add(drawing.Format, new List<Drawing> { drawing });
                }
            });

            groups.AsParallel().ForAll(pair =>
            {
                string subfolder = Path.Combine(folder.FullName, pair.Key);
                folder.CreateSubdirectory(pair.Key);

                pair.Value.ForEach(d => CopyDrawing(subfolder, d));

                logger.Logging($"Формат чертежей [ {pair.Key} ] успешно копирован в директорию [ {subfolder} ]");
            });
        }

        public async void ExplodeAsync(DirectoryInfo source, Dictionary<string, List<Drawing>> drawings)
        {
            await Task.Factory.StartNew(() => Explode(source, drawings));
            logger.Logging("Деление по форматам успешно завершенно");
        }

        private void CopyDrawing(string to, Drawing drawing)
        {
            string filename = drawing.Path.Name;
            File.Copy(drawing.Path.FullName, Path.Combine(to, filename));
        }

    }
}