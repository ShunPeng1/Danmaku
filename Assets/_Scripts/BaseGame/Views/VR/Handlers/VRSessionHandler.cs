using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views.Abstracts;
using _Scripts.BaseGame.Views.Basics.BNGExtension;
using _Scripts.CoreGame.InteractionSystems;
using BNG;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRSessionHandler : DanmakuSessionBaseHandler
    {
        
        public override void SetCurrentSession(DanmakuSession session)
        {
            CurrentSession = session;
            CurrentSession.SubscribeOnMenuAdded(MenuHandler.AddSessionMenu);
            CurrentSession.SubscribeOnMenuRemoved(MenuHandler.RemoveSessionMenu);
            
            foreach (var sessionMenu in session.GetPlayerSessionMenus(PlayerView.PlayerModel))
            {
                MenuHandler.AddSessionMenu(sessionMenu);    
            }
        }
        
        
        public override void UnsetCurrentSession()
        {
            foreach (var sessionMenu in CurrentSession.GetPlayerSessionMenus(PlayerView.PlayerModel))
            {
                if (sessionMenu.Activator != null && sessionMenu.Activator != PlayerView.PlayerModel)
                {
                    continue;
                }
                MenuHandler.RemoveSessionMenu(sessionMenu);
                
            }

            CurrentSession = null;

        }
        
        
    }
}