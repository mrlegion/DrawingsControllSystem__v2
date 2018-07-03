using System.IO;
using DrawingsControllSystem.Model.Common;

namespace DrawingsControllSystem.Model.Interfaces
{
    public interface IStoragekeeper
    {
        int Count { get; }
        void Add(Drawing item);
        void Add(Drawing item, bool replace);
        void Clear();
        void SendToBank(DirectoryInfo source);
    }
}