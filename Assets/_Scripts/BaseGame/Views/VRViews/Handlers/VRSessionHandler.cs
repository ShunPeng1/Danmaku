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
            
            foreach (var sessionMenu in session.PlayingSessionMenus)
            {
                if (sessionMenu.Activator != null && sessionMenu.Activator != PlayerView.PlayerModel)
                {
                    continue;
                }
                
                MenuHandler.AddSessionMenu(sessionMenu);    
            }
        }
        
        
        public override void UnsetCurrentSession()
        {
            foreach (var sessionMenu in CurrentSession.PlayingSessionMenus)
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