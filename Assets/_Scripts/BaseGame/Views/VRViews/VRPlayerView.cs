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
        
        public override void InitializeView(DanmakuPlayerModel playerModel)
        {
            PlayerModel = playerModel;
        }

        public override void SetupRole(IDanmakuRole playerRoleValue)
        {
            
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