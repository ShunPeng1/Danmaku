using System.Collections.Generic;
using BNG;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Positions
{
    public class SnapZoneCoordinator : MonoBehaviour
    {
        [SerializeField] private SnapZone _snapZonePrefab;
        [SerializeField] private Transform _snapZoneStartingTransform;
        [SerializeField] private Vector3 _snapZoneOffset;
        
        [Header("Debug")]
        [SerializeField] private int _debugSnapZoneCount;
        
        public List<SnapZone> SnapZones = new();
        
        
        public SnapZone GetEmptySnapZone(int startFrom = 0, Grabbable startingItem = null )
        {
            for (var index = startFrom; index < SnapZones.Count; index++)
            {
                var snapZone = SnapZones[index];
                if (snapZone.HeldItem == null)
                {
                    snapZone.HeldItem = startingItem;
                    return snapZone;
                }
            }

            var newSnapZone = CreateSnapZone(startingItem);
            return newSnapZone;
        }
        
        
        public SnapZone CreateSnapZone(Grabbable startingItem = null, int positionIndex = 0)
        {
            
            var snapZone = Instantiate(_snapZonePrefab, _snapZoneStartingTransform);
            snapZone.transform.position = _snapZoneStartingTransform.position;
            snapZone.transform.localPosition += _snapZoneOffset * SnapZones.Count;
            snapZone.transform.rotation = _snapZoneStartingTransform.rotation;
            
            snapZone.CanDropItem = true;
            snapZone.CanRemoveItem = true;

            snapZone.StartingItem = startingItem;
            
            SnapZones.Add(snapZone);
            
            return snapZone;
        }
        
        
        public List<SnapZone> CreateSnapZones(List<Grabbable> grabbables,int count)
        {
            var snapZones = new List<SnapZone>();
            
            for (var index = 0; index < count; index++)
            {
                Grabbable startingItem = null;
                if (index < grabbables.Count)
                {
                    startingItem = grabbables[index];
                }
                
                var snapZone = CreateSnapZone(startingItem,index);
                snapZones.Add(snapZone);
            }

            return snapZones;
        }
        
        

        public List<SnapZone> CreateSnapZones(int count)
        {
            var snapZones = new List<SnapZone>();

            for (var index = 0; index < count; index++)
            {
                var snapZone = CreateSnapZone(null,index);
                
                snapZones.Add(snapZone);
            }
            
            return snapZones;
        }

        public List<Grabbable> GetGrabbables()
        {
            var grabbables = new List<Grabbable>();
            foreach (var snapZone in SnapZones)
            {
                if (snapZone.HeldItem != null)
                {
                    grabbables.Add(snapZone.HeldItem);
                }
            }

            return grabbables;
            
        }

        public void DestroySnapZones()
        {
            foreach (var snapZone in SnapZones)
            {
                Destroy(snapZone.gameObject);
            }

            SnapZones.Clear();
        }
        
        public void SetAllowDrop(bool allow)
        {
            foreach (var snapZone in SnapZones)
            {
                snapZone.CanDropItem = allow;
            }
        }
        
        public void SetAllowRemove(bool allow)
        {
            foreach (var snapZone in SnapZones)
            {
                snapZone.CanRemoveItem = allow;
            }
        }
        
        void OnDrawGizmos()
        {

            for (int i = 0; i < _debugSnapZoneCount; i++)
            {
                Vector3 position = _snapZoneStartingTransform.position + _snapZoneOffset * i;
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(position, 0.1f);

                // Draw a line in the direction of the SnapZone's local up axis
                Vector3 direction = _snapZoneStartingTransform.rotation * Vector3.up;
                Gizmos.color = Color.green;
                Gizmos.DrawLine(position, position + direction * 0.2f);
            }
            
        }

    }
}