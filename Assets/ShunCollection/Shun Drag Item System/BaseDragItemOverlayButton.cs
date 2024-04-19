using System;
using UnityEngine;

namespace Shun_Drag_Item_System
{
    
    public class BaseDragItemOverlayButton : MonoBehaviour, IMouseHoverable
    {
        
        [SerializeField]
        private bool _interactable;
        public bool IsHoverable { get => _interactable; protected set => _interactable = value; }
        public bool IsHovering { get; protected set; }

        protected virtual void Awake()
        {
            if (GetComponent<Collider>() == null && GetComponent<Collider2D>() == null)
            {
                Debug.LogWarning("BaseDragItem requires either a Collider or a Collider2D component", this);
            }
        }
        
        public virtual void Select()
        {
            
        }

        public virtual void Deselect()
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

        public virtual void DisableInteractable()
        {
            if (!IsHoverable) return;
            IsHoverable = false;
            if (IsHovering) EndHover();
        }
        
        public virtual void EnableInteractable()
        {
            if (IsHoverable) return;
            IsHoverable = true;
        }

        
    }
}