using System.Collections.Generic;
using System.IO;
using DrawingsControllSystem.Model.Common;

namespace DrawingsControllSystem.Model.Interfaces
{
    public interface IExploder
    {
        void Explode(DirectoryInfo source, Dictionary<string, List<Drawing>> drawings);
        void ExplodeAsync(DirectoryInfo source, Dictionary<string, List<Drawing>> drawings);
    }
}