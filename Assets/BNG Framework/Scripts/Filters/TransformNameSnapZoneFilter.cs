using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BNG
{
    public class TransformNameSnapZoneFilter : SnapZoneFilter
    {
        [Header("Transform Name Filters")]
        /// <summary>
        /// If not empty, can only snap objects if transform name contains one of these strings
        /// </summary>
        [Tooltip("If not empty, can only snap objects if transform name contains one of these strings")]
        public List<string> MustIncludeNames;
        
        /// <summary>
        /// Do not allow snapping if transform contains one of these names
        /// </summary>
        [Tooltip("Do not allow snapping if transform contains one of these names")]
        public List<string> MustExcludeNames;

        private void Awake()
        {
            AddMustIncludeFilter(CheckIncludeNames);
            AddMustExcludeFilter(CheckExcludeNames);
        }
        public override bool CheckSnappable(Grabbable grabbable)
        {
            // Must contain transform name
            foreach (var filter in MustIncludeFilters)
            {
                if (!filter(grabbable))
                {
                    return false;
                }
            }
            
            // Must not contain transform name
            foreach (var filter in MustExcludeFilters)
            {
                if (!filter(grabbable))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        private bool CheckIncludeNames(Grabbable grabbable)
        {
            if (MustIncludeNames == null || MustIncludeNames.Count == 0)
            {
                return true;
            }
            
            string transformName = grabbable.transform.name;
            return MustIncludeNames.Any(name => transformName.Contains(name));
        }

        private bool CheckExcludeNames(Grabbable grabbable)
        {
            if (MustExcludeNames == null) return true;
            
            string transformName = grabbable.transform.name;
            return !MustExcludeNames.Any(name => transformName.Contains(name));
        }
    }
}