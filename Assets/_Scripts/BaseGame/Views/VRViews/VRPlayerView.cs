using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views.Basics.UI;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRPlayerView : DanmakuPlayerBaseView
    {
        [SerializeField] private VRPlayerUI _vrPlayerInformationUI;
        public override void InitializeView()
        {
            
        }

        public override void SetupRole(IDanmakuRole playerRoleValue)
        {
            
        }
        
        public override void StartMainStep(Action finishExecuteCallback)
        {
            CardHandView.AllowCardPlay();
            _vrPlayerInformationUI.SetOneTimeButtonAction(() =>
            {
                CardHandView.DisallowCardPlay();
                finishExecuteCallback?.Invoke();
            });
        }
        
        public override void AddSession(DanmakuSession session)
        {
            SessionHandler.SetCurrentSession(session);
        }

        public override void RemoveSession(DanmakuSession session)
        {
            SessionHandler.UnsetCurrentSession();
        }
    }
}