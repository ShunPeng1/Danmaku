using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Shun_Drag_Item_System
{
    [RequireComponent(typeof(Collider2D))]
    public class BaseDragItemRegion : MonoBehaviour, IMouseHoverable
    {
        public enum MiddleInsertionStyle
        {
            AlwaysBack,
            InsertInMiddle,
            Cannot,
            Swap,
        }
        
        
        [SerializeField]
        private bool _interactable = true;
        [SerializeField] protected BaseDragItemHolder DragItemHolderPrefab;
        [SerializeField] protected Transform SpawnPlace;
        [SerializeField] protected Vector3 ItemOffset = new Vector3(5f, 0 ,0);

        
        [SerializeField] protected List<BaseDragItemHolder> _itemPlaceHolders = new();
        [SerializeField] protected int MaxItemHold;
        public MiddleInsertionStyle ItemMiddleInsertionStyle = MiddleInsertionStyle.InsertInMiddle;
        protected BaseDragItemHolder TemporaryBaseDragItemHolder;
        public int ItemHoldingCount { get; private set; } 
        
        public bool IsHoverable { get => _interactable; protected set => _interactable = value;}
        public bool IsHovering { get; protected set; }

        #region INITIALIZE

        protected virtual void Awake()
        {
            InitializeItemPlaceHolder();
        }

        protected virtual void InitializeItemPlaceHolder()
        {
            if (_itemPlaceHolders.Count != 0)
            {
                MaxItemHold = _itemPlaceHolders.Count;
                for (int i = 0; i < MaxItemHold; i++)
                {
                    _itemPlaceHolders[i].InitializeRegion(this, i);
                }
            }
            else
            {
                for (int i = 0; i < MaxItemHold; i++)
                {
                    var itemPlaceHolder = Instantiate(DragItemHolderPrefab, SpawnPlace.position + ((float)i -  MaxItemHold/2f) * ItemOffset,
                        Quaternion.identity, SpawnPlace);
                    _itemPlaceHolders.Add(itemPlaceHolder);
                    itemPlaceHolder.InitializeRegion(this, i);
                }    
            }
            
        }
        
        #endregion

        #region OPERATION

        
        public List<BaseDragItem> GetAllItemGameObjects(bool getNull = false)
        {
            List<BaseDragItem> result = new();
            for (int i = 0; i < ItemHoldingCount; i++)
            {
                if ((!getNull && _itemPlaceHolders[i].DragItem != null) || getNull) result.Add(_itemPlaceHolders[i].DragItem);
            }

            return result;
        }

        public void DestroyAllItemGameObject()
        {
            foreach (var itemHolder in _itemPlaceHolders)
            {
                if (itemHolder.DragItem == null) continue;
                Destroy(itemHolder.DragItem.gameObject);
                itemHolder.RemoveItem();
            }
            
            ItemHoldingCount = 0;
        }

        protected BaseDragItemHolder FindEmptyItemPlaceHolder()
        {
            if (ItemHoldingCount >= MaxItemHold) return null;
            return _itemPlaceHolders[ItemHoldingCount];
        }
        
        public BaseDragItemHolder FindItemPlaceHolder(BaseDragItem baseDragItem)
        {
            foreach (var itemPlaceHolder in _itemPlaceHolders)
            {
                if (itemPlaceHolder.DragItem == baseDragItem) return itemPlaceHolder;
            }

            return null;
        }

        public bool AddItem(BaseDragItem dragItem, BaseDragItemHolder dragItemHolder = null, bool isReAdd = false)
        {
            if ( dragItemHolder == null || dragItemHolder.IndexInRegion >= ItemHoldingCount)
            {
                return AddItemAtBack(dragItem, isReAdd);
            }

            return ItemMiddleInsertionStyle switch
            {
                MiddleInsertionStyle.AlwaysBack => AddItemAtBack(dragItem, isReAdd),
                MiddleInsertionStyle.InsertInMiddle => AddItemAtMiddle(dragItem, dragItemHolder.IndexInRegion, isReAdd),
                MiddleInsertionStyle.Cannot => false,
                _ => false
            };
        }

        private bool AddItemAtBack(BaseDragItem dragItem, bool isReAdd = false)
        {
            if (ItemHoldingCount >= MaxItemHold || !CheckCompatibleObject(dragItem))
            {
                return false;
            }

            var index = ItemHoldingCount;
            var itemPlaceHolder = _itemPlaceHolders[index];
            itemPlaceHolder.AttachDragItem(dragItem);
            
            ItemHoldingCount ++;
            
            OnSuccessfullyAddItem(dragItem, itemPlaceHolder, index, isReAdd);
                
            return true;
        }
        
        private  bool AddItemAtMiddle(BaseDragItem dragItem, int index, bool isReAdd = false)
        {
            if (ItemHoldingCount >= MaxItemHold || !CheckCompatibleObject(dragItem))
            {
                return false;
            }
            
            ShiftRight(index);

            var itemPlaceHolder = _itemPlaceHolders[index];
            itemPlaceHolder.AttachDragItem(dragItem);
            
            ItemHoldingCount++;
            
            OnSuccessfullyAddItem(dragItem, itemPlaceHolder, index, isReAdd);
            
            return true;
        }
        
        
        protected virtual void ShiftRight(int startIndex)
        {
            for (int i = _itemPlaceHolders.Count - 1; i > startIndex; i--)
            {
                var item = _itemPlaceHolders[i - 1].DetachDragItem();
                
                if (item == null) continue;
                _itemPlaceHolders[i].AttachDragItem(item);
                
                //SmoothMove(item.transform, _itemPlaceHolders[i].transform.position);

            }
        }
        
        
        protected virtual void ShiftLeft(int startIndex)
        {
            for (int i = startIndex; i < _itemPlaceHolders.Count - 1; i++)
            {
                var item = _itemPlaceHolders[i + 1].DetachDragItem();
                
                if (item == null) continue;
                
                _itemPlaceHolders[i].AttachDragItem(item);
                
                
                //SmoothMove(item.transform, _itemPlaceHolders[i].transform.position);

            }
        }
        
        public virtual bool RemoveItem(BaseDragItem dragItem)
        {

            for (int i = 0; i < _itemPlaceHolders.Count; i++)
            {
                if (_itemPlaceHolders[i].DragItem != dragItem) continue;
                _itemPlaceHolders[i].DetachDragItem();
                
                ShiftLeft(i);
                ItemHoldingCount--;
                
                OnSuccessfullyRemoveItem(dragItem, _itemPlaceHolders[i], i);
                return true;
            }
            return false;
        }
        
        public virtual bool RemoveItem(BaseDragItem dragItem,BaseDragItemHolder dragItemHolder, bool isTakeOutTemporary = false)
        {
            if (dragItemHolder == null || dragItemHolder.DragItem != dragItem) return false;

            dragItemHolder.DetachDragItem();

            var index = _itemPlaceHolders.IndexOf(dragItemHolder);
            ShiftLeft(index);
            ItemHoldingCount--;

            OnSuccessfullyRemoveItem(dragItem, dragItemHolder, index, isTakeOutTemporary);
            return true;
        }
        
        
        #endregion

        #region MOUSE_INPUT
        
        public virtual bool TryAddItem(BaseDragItem dragItem, BaseDragItemHolder dragItemHolder = null)
        {
            if (!_interactable) return false;
            return AddItem(dragItem, dragItemHolder);
        }
        
        public virtual bool TakeOutTemporary(BaseDragItem dragItem,BaseDragItemHolder dragItemHolder)
        {
            if (!_interactable) return false;
            
            if (!RemoveItem(dragItem, dragItemHolder, true)) return false;
            
            TemporaryBaseDragItemHolder = dragItemHolder;
            return true;
        }
        
        public virtual void ReAddTemporary(BaseDragItem baseDragItem)
        {
            if (baseDragItem == null) return;
            AddItem(baseDragItem, TemporaryBaseDragItemHolder, true);
            
            TemporaryBaseDragItemHolder = null;
        }

        public virtual void RemoveTemporary(BaseDragItem baseDragItem)
        {
            if (baseDragItem == null) return;
            
            TemporaryBaseDragItemHolder = null;
        }
        
        
        #endregion

        protected virtual void SmoothMove(Transform movingObject, Vector3 toPosition)
        {
            movingObject.position = toPosition;
        }

        public virtual bool CheckCompatibleObject(BaseDragItem baseDragItem)
        {
            return true;
        }
        
        private void RemoveDestroyedItem(BaseDragItem item)
        {
            RemoveItem(item);
        }
        
        protected virtual void OnSuccessfullyAddItem(BaseDragItem baseDragItem, BaseDragItemHolder baseDragItemHolder, int index, bool isReAdd = false)
        {
            if(!isReAdd) baseDragItem.OnDestroy += RemoveDestroyedItem;
        }

        protected virtual void OnSuccessfullyRemoveItem(BaseDragItem baseDragItem, BaseDragItemHolder baseDragItemHolder, int index, bool isTakeOutTemporary = false)
        {
            if(!isTakeOutTemporary) baseDragItem.OnDestroy -= RemoveDestroyedItem;
        }

        public void StartHover()
        {
            IsHovering = true;
            
        }

        public void EndHover()
        {
            IsHovering = false;
            
        }

        public virtual void DisableInteractable()
        {
            
            if (!_interactable) return;
            _interactable = false;
            if(IsHovering) EndHover();
        }
        
        public virtual void EnableInteractable()
        {
            if (_interactable) return;
            _interactable = true;
        }
    }
}