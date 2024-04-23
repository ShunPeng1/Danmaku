using System;
using System.Collections.Generic;
using BNG;
using UnityEngine;

namespace BNG
{
    public abstract class SnapZoneFilter : MonoBehaviour, ISnapZoneFilter
    {
        public readonly List<Func<Grabbable, bool>> MustIncludeFilters = new List<Func<Grabbable, bool>>();
        public readonly List<Func<Grabbable, bool>> MustExcludeFilters = new List<Func<Grabbable, bool>>();
        
        public void AddMustIncludeFilter(Func<Grabbable, bool> filter)
        {
            MustIncludeFilters.Add(filter);
        }

        public void AddMustExcludeFilter(Func<Grabbable, bool> filter)
        {
            MustExcludeFilters.Add(filter);
        }

        public void RemoveMustIncludeFilter(Func<Grabbable, bool> filter)
        {
            MustIncludeFilters.Remove(filter);
        }

        public void RemoveMustExcludeFilter(Func<Grabbable, bool> filter)
        {
            MustExcludeFilters.Remove(filter);
        }

        public void ClearMustIncludeFilters()
        {
            MustIncludeFilters.Clear();
        }

        public void ClearMustExcludeFilters()
        {
            MustExcludeFilters.Clear();
        }

        public abstract bool CheckSnappable(Grabbable grabbable);
    }
}