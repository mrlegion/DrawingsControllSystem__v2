using System.IO;
using DrawingsControllSystem.Model.Common;

namespace DrawingsControllSystem.Model.Interfaces
{
    public interface ICreator
    {
        CreateStrategy Strategy { get; set; }
        void Create(FileInfo file);
        void Create(FileInfo file, bool replace);
    }
}