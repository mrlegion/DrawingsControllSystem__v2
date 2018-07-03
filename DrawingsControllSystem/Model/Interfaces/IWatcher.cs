using System.IO;
using DrawingsControllSystem.Model.Common;

namespace DrawingsControllSystem.Model.Interfaces
{
    public interface IWatcher
    {
        void Run(DirectoryInfo directory, FileExtansion fileExtantion);
        void Stop();
    }
}