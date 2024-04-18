
using System;
using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Shun_Drag_Item_System
{
    public class BaseDragItem : MonoBehaviour, IMouseDraggable, IMouseHoverable
    {
        public Action<BaseDragItem> OnDestroy { get; set; }
        protected BaseDragItemInput Input;
        public bool IsDestroyed { get; protected set; }
        public bool IsDraggable = true; 
        public bool IsDragging { get; private set; }

        public bool IsHoverable = true;
        public bool IsHovering { get; private set; }
        
        [SerializeField] protected bool ActivateOnValidate = false;

        protected virtual void Awake()
        {
            if (GetComponent<Collider>() == null && GetComponent<Collider2D>() == null)
            {
                Debug.LogWarning("BaseDragItem requires either a Collider or a Collider2D component", this);
            }
        }
        
        public virtual bool StartDrag()
        {
            if (!IsDraggable) return false;
            IsDragging = true;
            return true;
        }

        public virtual bool EndDrag()
        {
            if (!IsDraggable) return false;
            IsDragging = false;
            return true;
        }
        
        private void OnValidate()
        {
            if (ActivateOnValidate) ValidateInformation();
        }
        
        protected virtual void ValidateInformation()
        {
            
        }
        
        public virtual void StartHover()
        {
            IsHovering = true;
        }

        public virtual void EndHover()
        {
            IsHovering = false;
        }
        
        public virtual void DisableDrag()
        {
            if (!IsDraggable) return;
            IsDraggable = false;
            if (IsHovering) EndHover();
        }
        
        public virtual void EnableDrag()
        {
            if (IsDraggable) return;
            IsDraggable = true;
        }

        public void SetMouseInput(BaseDragItemInput input)
        {
            Input = input;
        }
        
        public void RemoveMouseInput(BaseDragItemInput input)
        {
            Input = null;    
        }

        public void Destroy()
        {
            IsDestroyed = true;
            OnDestroy?.Invoke(this);
        }
    }
}
