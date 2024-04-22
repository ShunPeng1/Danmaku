using System;
using BNG;

namespace BNG
{
    public interface ISnapZoneFilter
    {
        void AddMustIncludeFilter(Func<Grabbable, bool> filter);
        void AddMustExcludeFilter(Func<Grabbable, bool> filter);
        void RemoveMustIncludeFilter(Func<Grabbable, bool> filter);
        void RemoveMustExcludeFilter(Func<Grabbable, bool> filter);
        void ClearMustIncludeFilters();
        void ClearMustExcludeFilters();
        
        bool CheckSnappable(Grabbable grabbable);

    }
}