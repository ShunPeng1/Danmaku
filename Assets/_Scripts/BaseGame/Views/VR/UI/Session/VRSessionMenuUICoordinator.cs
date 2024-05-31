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
        
        
        private readonly Dictionary<DanmakuSessionMenu, VRSessionMenuUI> _vrMenuUIs = new ();
        private readonly List<DanmakuSessionMenu> _sessionMenus = new ();
        private VRSessionMenuUI _currentMenuUI;

        public void CreateView(DanmakuSessionMenu sessionMenu)
        {
            if (_currentMenuUI != null)
            {
                _currentMenuUI.gameObject.SetActive(false);
            }
            
            var vrMenuUI = Instantiate(_vrSessionMenuUIPrefab, _startingTransform);
            
            var uiTransform = vrMenuUI.transform;
            
            uiTransform.position = _startingTransform.position;
            //uiTransform.localPosition += _startingTransform.position * _vrMenuUIs.Count;
            uiTransform.rotation = _startingTransform.rotation;
            
            vrMenuUI.SetMenu(sessionMenu);
            
            
            _vrMenuUIs.Add(sessionMenu, vrMenuUI);
            _sessionMenus.Add(sessionMenu);
            _currentMenuUI = vrMenuUI;
            
        }

        public void RemoveView(DanmakuSessionMenu sessionMenu)
        {
            if (!_vrMenuUIs.ContainsKey(sessionMenu))
            {
                return;
            }
            
            var vrMenuUI = _vrMenuUIs[sessionMenu];
            _vrMenuUIs.Remove(sessionMenu);
            Destroy(vrMenuUI.gameObject);
            
            _sessionMenus.Remove(sessionMenu);
            
            if (_sessionMenus.Count == 0)
            {
                _currentMenuUI = null;
            }
            else
            {
                _currentMenuUI = _vrMenuUIs[_sessionMenus[^1]];
                _currentMenuUI.gameObject.SetActive(true);
            }
            
        }
        
        public void ClearMenuUI()
        {
            foreach (var vrMenuUI in _vrMenuUIs.Values)
            {
                Destroy(vrMenuUI.gameObject);
            }
            _vrMenuUIs.Clear();
            _sessionMenus.Clear();
            _currentMenuUI = null;
            
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