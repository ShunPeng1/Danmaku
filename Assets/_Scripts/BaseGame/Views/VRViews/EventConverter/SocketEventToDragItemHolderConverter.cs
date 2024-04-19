using System;
using Shun_Drag_Item_System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Scripts.BaseGame.Views.Basics.EventConverter
{
    [RequireComponent(typeof(XRSocketInteractor), typeof(BaseDragItemHolder))]
    public class SocketEventToDragItemHolderConverter : MonoBehaviour
    {
        private XRSocketInteractor _socketInteractor;
        private BaseDragItemHolder _dragItemHolder;
        
        private void Awake()
        {
            _socketInteractor = GetComponent<XRSocketInteractor>();
            _dragItemHolder = GetComponent<BaseDragItemHolder>();
            
            _socketInteractor.selectEntered.AddListener(OnSelectEntered);
            _socketInteractor.selectExited.AddListener(OnSelectExited);
            
           
            
        }

        private void OnSelectExited(SelectExitEventArgs arg0)
        {
            _dragItemHolder.RemoveItemFromRegion();
        }

        private void OnSelectEntered(SelectEnterEventArgs arg0)
        {
            var dragItem= arg0.interactableObject.transform.GetComponent<BaseDragItem>();
            _dragItemHolder.AddItemToRegion(dragItem);
        }

        
    }
}