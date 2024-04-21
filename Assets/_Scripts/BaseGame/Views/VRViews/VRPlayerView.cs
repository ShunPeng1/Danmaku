using _Scripts.BaseGame.Views.Basics.UI;
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
        
        public override void StartMainStep(System.Action finishExecuteCallback)
        {
            CardHandView.AllowCardPlay();
            _vrPlayerInformationUI.SetOneTimeButtonAction(() =>
            {
                CardHandView.DisallowCardPlay();
                finishExecuteCallback?.Invoke();
            });
        }
    }
}