using System;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockPlayerView : DanmakuPlayerBaseView
    {
        public override void InitializeView()
        {
            
        }

        public override void SetupRole(IDanmakuRole playerRoleValue)
        {
            
        }
        
        public override void StartMainStep(Action finishExecuteCallback)
        {
            finishExecuteCallback?.Invoke();
        }
    }
}