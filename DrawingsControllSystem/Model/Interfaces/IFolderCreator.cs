using System.IO;

namespace DrawingsControllSystem.Model.Interfaces
{
    public interface IFolderCreator
    {
        DirectoryInfo Directory { get; }
        void Create(DirectoryInfo source);
    }
}