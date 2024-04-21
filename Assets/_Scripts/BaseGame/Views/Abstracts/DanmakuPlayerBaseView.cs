using System;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuPlayerBaseView : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] public DanmakuRoleBaseView RoleView;
        [ShowInInspector, ReadOnly] public DanmakuCardHandBaseView CardHandView;
        
        private void Awake()
        {
            RoleView = transform.GetComponentInChildren<DanmakuRoleBaseView>();
            CardHandView = transform.GetComponentInChildren<DanmakuCardHandBaseView>();
        }

        public abstract void InitializeView();


        public abstract void SetupRole(IDanmakuRole playerRoleValue);

        public virtual void StartMainStep(Action finishExecuteCallback)
        {
            CardHandView.AllowCardPlay();
            CardHandView.DisallowCardPlay();
            finishExecuteCallback?.Invoke();
        }
    }
}