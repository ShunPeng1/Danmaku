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
        [SerializeField] private TargetSelectionCanvas _targetSelectionCanvas;
        
        public override void SetCurrentSession(DanmakuSession session)
        {
            CurrentSession = session;
            
            foreach (var sessionMenu in session.PlayingSessionMenus)
            {
                MenuHandler.AddSessionMenu(sessionMenu);    
            }
        }
        
        
        public override void UnsetCurrentSession()
        {
            CurrentSession = null;
        }
        
        public override void AddCardsToSelection(List<DanmakuCardBaseView> cardViews)
        {
            CardShowcaseView.Show();
            CardShowcaseView.AddCardsToShowcase(cardViews);
        }
        
    }
}