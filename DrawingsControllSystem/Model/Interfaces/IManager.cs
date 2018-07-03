using System.Collections.Generic;
using System.IO;
using DrawingsControllSystem.Model.Common;

namespace DrawingsControllSystem.Model.Interfaces
{
    public interface IManager
    {
        Dictionary<string, List<Drawing>> Data { get; }

        void Add(string name, List<Drawing> drawings);

        void Explode(DirectoryInfo source);

        void ExplodeAsync(DirectoryInfo source);

        void ShowInformation();
    }
}