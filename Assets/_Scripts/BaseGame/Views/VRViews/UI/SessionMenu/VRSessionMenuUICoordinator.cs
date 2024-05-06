using System;
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRSessionMenuUICoordinator : MonoBehaviour
    {
        [SerializeField] private VRSessionMenuUI _vrSessionMenuUIPrefab;
        [SerializeField] private Transform _startingTransform;
        [SerializeField] private Vector3 _offset;
        
        private Dictionary<DanmakuSessionMenu, VRSessionMenuUI> _vrMenuUIs = new ();
        

        public void CreateMenuUI(DanmakuSessionMenu sessionMenu, Action endAction = null)
        {
            var vrMenuUI = Instantiate(_vrSessionMenuUIPrefab, _startingTransform);
            
            var uiTransform = vrMenuUI.transform;
            
            uiTransform.position = _startingTransform.position;
            uiTransform.localPosition += _startingTransform.position * _vrMenuUIs.Count;
            uiTransform.rotation = _startingTransform.rotation;
            vrMenuUI.SetOneTimeButtonAction(sessionMenu.TryEndSession, endAction);
            
            _vrMenuUIs.Add(sessionMenu, vrMenuUI);
            
        }

        public void RemoveMenuUI(DanmakuSessionMenu sessionMenu)
        {
            if (!_vrMenuUIs.ContainsKey(sessionMenu))
            {
                return;
            }
            
            var vrMenuUI = _vrMenuUIs[sessionMenu];
            _vrMenuUIs.Remove(sessionMenu);
            Destroy(vrMenuUI.gameObject);
        }
        
        public void ClearMenuUI()
        {
            foreach (var vrMenuUI in _vrMenuUIs.Values)
            {
                Destroy(vrMenuUI.gameObject);
            }
            _vrMenuUIs.Clear();
        }
        
        public void SetMenuUIActive(DanmakuSessionMenu sessionMenu, bool active)
        {
            if (!_vrMenuUIs.ContainsKey(sessionMenu))
            {
                return;
            }
            
            var vrMenuUI = _vrMenuUIs[sessionMenu];
            vrMenuUI.gameObject.SetActive(active);
        }
        
        
    }
}