using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrawingsControllSystem.Model.Common;
using DrawingsControllSystem.Model.Interfaces;

namespace DrawingsControllSystem.Model.Impliments
{
    public class Statistics : IStatistics
    {
        private readonly ILogger logger;

        public Statistics(ILogger logger)
        {
            this.logger = logger;
        }

        public void ShowInformation(Dictionary<string, List<Drawing>> drawings)
        {
            StringBuilder builder = new StringBuilder();
            double count = 0;
            foreach (var folder in drawings)
            {
                builder.Append('-', 30);
                builder.Append($"{Environment.NewLine, -14}Наименование папки: {folder.Key}");
                builder.Append($"{Environment.NewLine, -14}В папке находятся {folder.Value.Count} чертежей:");
                builder.Append($"{Environment.NewLine, -14}Список форматов:");
                builder.Append(Environment.NewLine);

                var formats = folder.Value.GroupBy(d => d.Format);

                foreach (IGrouping<string, Drawing> format in formats)
                {
                    builder.AppendFormat("            {0, -10} : {1, -10}", format.Key, format.Count());
                    builder.Append(Environment.NewLine);
                }

                count += folder.Value.Sum(d => d.Include);

                builder.Append(Environment.NewLine);
                builder.AppendFormat("            Общее количество в а4: {0, -5}", folder.Value.Sum(d => d.Include));
                builder.Append(Environment.NewLine);
                builder.Append('-', 42);
            }

            builder.AppendFormat($"{Environment.NewLine, -14}ОБЩЕЕ КОЛ-ВО А4: {count,-5}");
            builder.Append(Environment.NewLine);

            logger.Logging(builder.ToString());
        }
    }
}