using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRSessionMenuHandler : DanmakuSessionMenuBaseHandler
    {
        [SerializeField] private VRSessionMenuUICoordinator _sessionMenuUICoordinator;
        [SerializeField] private VRSessionChoiceUICoordinator _sessionChoiceUICoordinator;
        
        public override void AddSessionMenu(DanmakuSessionMenu sessionMenu)
        {
            if (SessionMenus.Contains(sessionMenu))
            {
                return;
            }
            
            SessionMenus.Add(sessionMenu);
            foreach (var sessionChoice in sessionMenu.SessionChoices)
            {
                _sessionChoiceUICoordinator.CreateView(sessionChoice);
            }
            
            _sessionMenuUICoordinator.CreateView(sessionMenu);
        }
        
        public override void RemoveSessionMenu(DanmakuSessionMenu sessionMenu)
        {
            if (!SessionMenus.Contains(sessionMenu))
            {
                return;
            }
            
            SessionMenus.Remove(sessionMenu);
            
            foreach (var sessionChoice in sessionMenu.SessionChoices)
            {
                _sessionChoiceUICoordinator.RemoveView(sessionChoice);

            }
            
            _sessionMenuUICoordinator.RemoveView(sessionMenu);
            
        }

    }
}