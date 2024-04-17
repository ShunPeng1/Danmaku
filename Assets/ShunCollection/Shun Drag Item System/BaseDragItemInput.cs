using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Shun_Drag_Item_System
{
    public class BaseDragItemInput : MonoBehaviour, ShunDragItemInputActionAsset.IDragItemControlsActions
    {
        [SerializeField] private bool _is2D = true;
        
        protected Vector3 MouseWorldPosition;
        protected RaycastHit2D[] RayCastHit2Ds;
        protected RaycastHit [] RayCastHits3Ds;
    
        [Header("Hover Objects")]
        protected List<IMouseHoverable> LastHoverMouseInteractableGameObjects = new();
        public bool IsHoveringItem => LastHoverMouseInteractableGameObjects.Count != 0;

        [Header("Drag Objects")]
        protected Vector3 ItemOffset;
        protected BaseDragItem DraggingItem;
        protected BaseDragItemRegion LastDragItemRegion;
        protected BaseDragItemHolder LastDragItemHolder;
        protected BaseDragItemOverlayButton LastItemButton;

        private ShunDragItemInputActionAsset _dragInputActionAsset;
        
        

        private void Awake()
        {
            
        }

        public void OnEnable()
        {
            if (_dragInputActionAsset == null)
            {
                _dragInputActionAsset = new ShunDragItemInputActionAsset();
                _dragInputActionAsset.DragItemControls.SetCallbacks(this);
            }
            
            _dragInputActionAsset.Enable();
            
        }
        
        public void OnDisable()
        {
            _dragInputActionAsset.Disable();
        }

        public bool IsDraggingItem
        {
            get;
            private set;
        }
        

        #region CAST
        

        protected virtual void CastMouse()
        {
            RayCastHit2Ds = Physics2D.RaycastAll(MouseWorldPosition, Vector2.zero);
        }

        #endregion
    

        #region HOVER

        protected virtual void UpdateHoverObject()
        {
            var hoveringMouseInteractableGameObject = FindAllIMouseInteractableInMouseCast();

            var endHoverInteractableGameObjects = SetDifference(LastHoverMouseInteractableGameObjects, hoveringMouseInteractableGameObject);
            var startHoverInteractableGameObjects =  SetDifference(hoveringMouseInteractableGameObject, LastHoverMouseInteractableGameObjects);

            foreach (var interactable in endHoverInteractableGameObjects)
            {
                if (interactable.IsHovering) interactable.EndHover();
            }
            foreach (var interactable in startHoverInteractableGameObjects)
            {
                if (!interactable.IsHovering) interactable.StartHover();
            }

            LastHoverMouseInteractableGameObjects = hoveringMouseInteractableGameObject;
        }

        private List<IMouseHoverable> SetDifference(List<IMouseHoverable> list1, List<IMouseHoverable> list2)
        {
            return list1.Except(list2).ToList();
        }


        protected virtual IMouseHoverable FindFirstIMouseInteractableInMouseCast()
        {
            foreach (var hit in RayCastHit2Ds)
            {
                var characterItemButton = hit.transform.gameObject.GetComponent<BaseDragItemOverlayButton>();
                if (characterItemButton != null && characterItemButton.IsHoverable)
                {
                    //Debug.Log("Mouse find "+ gameObject.name);
                    return characterItemButton;
                }
            
                var characterItemGameObject = hit.transform.gameObject.GetComponent<BaseDragItem>();
                if (characterItemGameObject != null && characterItemGameObject.IsDraggable)
                {
                    //Debug.Log("Mouse find "+ gameObject.name);
                    return characterItemGameObject;
                }
            }

            return null;
        }
    
        protected virtual List<IMouseHoverable> FindAllIMouseInteractableInMouseCast()
        {
            List<IMouseHoverable> mouseInteractableGameObjects = new(); 
            foreach (var hit in RayCastHit2Ds)
            {
                var characterItemButton = hit.transform.gameObject.GetComponent<BaseDragItemOverlayButton>();
                if (characterItemButton != null && characterItemButton.IsHoverable)
                {
                    mouseInteractableGameObjects.Add(characterItemButton);
                }
            
                var characterItemGameObject = hit.transform.gameObject.GetComponent<BaseDragItem>();
                if (characterItemGameObject != null && characterItemGameObject.IsDraggable)
                {
                    //Debug.Log("Mouse find "+ gameObject.name);
                    mouseInteractableGameObjects.Add(characterItemGameObject);
                }
            }

            return mouseInteractableGameObjects;
        }
    
        #endregion
    
    
        protected virtual TResult FindFirstInMouseCast<TResult>()
        {
            foreach (var hit in RayCastHit2Ds)
            {
                var result = hit.transform.gameObject.GetComponent<TResult>();
                if (result != null)
                {
                    //Debug.Log("Mouse find "+ gameObject.name);
                    return result;
                }
            }

            //Debug.Log("Mouse cannot find "+ typeof(TResult));
            return default;
        }

    
        protected bool StartDragItem()
        {
            // Check for button first
            LastItemButton = FindFirstInMouseCast<BaseDragItemOverlayButton>();

            if (LastItemButton != null && LastItemButton.IsHoverable)
            {
                LastItemButton.Select();
                return true;
            } 

            // Check for item game object second
            DraggingItem = FindFirstInMouseCast<BaseDragItem>();

            if (DraggingItem == null || !DraggingItem.IsDraggable || !DetachItemToHolder())
            {
                DraggingItem = null;
                return false;
            }
            
            // Successfully detach item
            ItemOffset = DraggingItem.transform.position - MouseWorldPosition;
            IsDraggingItem = true;

            DraggingItem.StartDrag();
            DraggingItem.SetMouseInput(this);
            return true;
        
        }

        protected void DragItem()
        {
            if (!IsDraggingItem) return; 
        
            DraggingItem.transform.position = MouseWorldPosition + ItemOffset;
        
        }

        private void EndDragItem()
        {
            if (!IsDraggingItem) return;

            
            DraggingItem.EndDrag();
            if (DraggingItem != null) DraggingItem.RemoveMouseInput(this);
            AttachItemToHolder();

            DraggingItem = null;
            LastDragItemHolder = null;
            LastDragItemRegion = null;
            IsDraggingItem = false;

        }

        public void ForceEndDragAndDetachTemporary()
        {
            if (LastDragItemRegion != null) // remove the temporary in last region
            {
                LastDragItemRegion.RemoveTemporary(DraggingItem);
                
            }
            
            DraggingItem = null;
            LastDragItemHolder = null;
            LastDragItemRegion = null;
            IsDraggingItem = false;
        }
    
        protected virtual bool DetachItemToHolder()
        {
            if (DraggingItem.IsDestroyed) return false;
            
            // Check the item region base on item game object or item holder, to TakeOutTemporary
            LastDragItemRegion = FindFirstInMouseCast<BaseDragItemRegion>();
            if (LastDragItemRegion == null)
            {
                LastDragItemHolder = FindFirstInMouseCast<BaseDragItemHolder>();
                if (LastDragItemHolder == null)
                {
                    return true;
                }

                LastDragItemRegion = LastDragItemHolder.DragItemRegion;
            }
            else
            {
                LastDragItemHolder = LastDragItemRegion.FindItemPlaceHolder(DraggingItem);
            }

            // Having got the region and holder, take the item out temporary
            if ((!LastDragItemRegion.CheckCompatibleObject(DraggingItem) )||
            (LastDragItemRegion.CheckCompatibleObject(DraggingItem) 
             && LastDragItemRegion.TakeOutTemporary(DraggingItem, LastDragItemHolder))) return true;
        
            LastDragItemHolder = null;
            LastDragItemRegion = null;

            return false;

        }

        protected void AttachItemToHolder()
        {
            if (DraggingItem == null || DraggingItem.IsDestroyed) return;
            var dropRegion = FindFirstInMouseCast<BaseDragItemRegion>();
            var dropHolder = FindFirstInMouseCast<BaseDragItemHolder>();
        
            if (dropHolder == null)
            {
                if (dropRegion != null && dropRegion != LastDragItemRegion &&
                    dropRegion.TryAddItem(DraggingItem, dropHolder)) // Successfully add to the drop region
                {
                    if (LastDragItemHolder != null) // remove the temporary in last region
                    {
                        LastDragItemRegion.RemoveTemporary(DraggingItem);
                        return;
                    }
                }
            
                if (LastDragItemRegion != null) // Unsuccessfully add to drop region or it is the same region
                    LastDragItemRegion.ReAddTemporary(DraggingItem);
            }
            else
            {
                if (dropRegion == null) 
                    dropRegion = dropHolder.DragItemRegion;
                
                if (dropRegion == null) // No region to drop anyway
                {
                    if(LastDragItemRegion != null) LastDragItemRegion.ReAddTemporary(DraggingItem);
                }

                if (dropRegion.ItemMiddleInsertionStyle == BaseDragItemRegion.MiddleInsertionStyle.Swap)
                {
                    var targetItem = dropHolder.DragItem;
                    if (targetItem != null && LastDragItemRegion != null && dropRegion.TakeOutTemporary(targetItem, dropHolder))
                    {
                        LastDragItemRegion.ReAddTemporary(targetItem);
                        dropRegion.ReAddTemporary(DraggingItem);
                        
                        return;
                    }
                    
                }
                
                if (!dropRegion.TryAddItem(DraggingItem, dropHolder))
                {
                    if(LastDragItemRegion != null) LastDragItemRegion.ReAddTemporary(DraggingItem);
                }
                
                if (LastDragItemHolder != null)
                {
                    LastDragItemRegion.RemoveTemporary(DraggingItem);
                }

            }

        }


        public void OnDrag(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                EndDragItem();
            }
            if (context.performed)
            {
                DragItem();
            }
            if (context.started)
            {
                StartDragItem();
            }
            
            
        }

        public void OnScreenPosition(InputAction.CallbackContext context)
        {
            CastMouse();
            if(!IsDraggingItem) UpdateHoverObject();

            
            var value = context.ReadValue<Vector2>();
            MouseWorldPosition = Camera.main.ScreenToWorldPoint(value);
     
            
            if (IsDraggingItem) DragItem();
        }
    }
}
