using _Scripts.BaseGame.Views.Basics.UI;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRPlayerView : DanmakuPlayerBaseView
    {
        [SerializeField] private PlayerUI _playerInformationUI;
        public override void InitializeView()
        {
            
        }

        public override void SetupRole(IDanmakuRole playerRoleValue)
        {
            
        }
        
        public override void StartMainStep(System.Action finishExecuteCallback)
        {
            CardHandView.AllowCardPlay();
            _playerInformationUI.SetOneTimeButtonAction(() =>
            {
                CardHandView.DisallowCardPlay();
                finishExecuteCallback?.Invoke();
            });
        }
    }
}