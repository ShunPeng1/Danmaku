using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics.UI
{
    public class VRSessionMenuHandler : DanmakuSessionMenuBaseHandler
    {
        
        
        public override void AddSessionMenu(DanmakuSessionMenu sessionMenu)
        {
            if (SessionMenus.Contains(sessionMenu))
            {
                return;
            }
            
            SessionMenus.Add(sessionMenu);
            foreach (var sessionChoice in sessionMenu.SessionChoices)
            {
                CreateView(sessionChoice);
            }
        
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
                var cardPlayView = CardPlayViews.FirstOrDefault(view => view.SessionChoice == sessionChoice);
                if (cardPlayView != null)
                {
                    RemoveCardPlayView(cardPlayView);
                }
            }
            
        }

        
        public void CreateView(DanmakuSessionChoice sessionChoice)
        {
            var cardPlayView = CreateCardPlayView(sessionChoice);
            cardPlayView.SetSessionChoice(sessionChoice);
                    
            switch (sessionChoice.TargetType)
            {
                case var type when type == typeof(DanmakuCharacterCardModel):
                    // Handle DanmakuCharacterCardModel case
                            
                    break;
                case var type when type == typeof(DanmakuPlayerModel):
                    // Handle DanmakuPlayerModel case
                            
                    break;
                // Add more cases as needed
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

    }
}