using System.Collections.Generic;
using DrawingsControllSystem.Model.Common;

namespace DrawingsControllSystem.Model.Interfaces
{
    public interface IStatistics
    {
        void ShowInformation(Dictionary<string, List<Drawing>> drawings);
    }
}