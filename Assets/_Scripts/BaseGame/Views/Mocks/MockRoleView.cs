using System;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Default
{
    public class MockRoleView : DanmakuRoleBaseView
    {
        public override void RevealRole(IDanmakuRole role)
        {
            Debug.Log("RevealRole :" + role.GetType());
        }
    }
}