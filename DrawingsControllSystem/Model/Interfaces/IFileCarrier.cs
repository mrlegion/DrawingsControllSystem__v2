using System.Collections.Generic;
using System.IO;
using DrawingsControllSystem.Model.Common;

namespace DrawingsControllSystem.Model.Interfaces
{
    public interface IFileCarrier
    {
        bool Transfer(DirectoryInfo to, List<Drawing> drawings);
    }
}