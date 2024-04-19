
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shun_Drag_Item_System
{
    /// <summary>
    /// This class is the place holder of a drag item in item place region.
    /// This can be used to move, animations,...
    /// </summary>
    public class BaseDragItemHolder : MonoBehaviour
    {
        [HideInInspector] public BaseDragItemRegion DragItemRegion;
        [HideInInspector] public int IndexInRegion;
        
        [SerializeField] private BaseDragItem _dragItem;

        public BaseDragItem DragItem
        {
            get { return _dragItem; }
        }
        
        protected virtual void Awake()
        {
            if (GetComponent<Collider>() == null && GetComponent<Collider2D>() == null)
            {
                Debug.LogWarning("BaseDragItem requires either a Collider or a Collider2D component", this);
            }
        }
        
        public void InitializeRegion(BaseDragItemRegion dragItemRegion, int indexInRegion)
        {
            DragItemRegion = dragItemRegion;
            IndexInRegion = indexInRegion;
        }
        
        public void AttachDragItem(BaseDragItem dragItem)
        {
            if (dragItem == null) return;
            
            _dragItem = dragItem;
            _dragItem.transform.SetParent(transform, true);
            
            _dragItem.DisableDrag();
            VisualizeAttachment();
            _dragItem.EnableDrag();
        }

        public BaseDragItem DetachDragItem()
        {
            if (_dragItem == null) return null;
            
            BaseDragItem detachedDrag = _dragItem;
            
            detachedDrag.transform.SetParent(DragItemRegion.transform.parent, true);

            VisualizeDetachment();
            _dragItem = null;
            
            return detachedDrag;
        }

        protected virtual void VisualizeDetachment()
        {
            
        }

        protected virtual void VisualizeAttachment()
        {
            _dragItem.transform.localPosition = Vector3.zero;
        }

        public void RemoveItem()
        {
            _dragItem = null;
        }

        public void AddItemToRegion(BaseDragItem dragItem)
        {
            DragItemRegion.AddItem(dragItem,this);
        }
        
        public void RemoveItemFromRegion()
        {
            if (_dragItem == null) return;
            
            DragItemRegion.RemoveItem(_dragItem);
        }
    }
}