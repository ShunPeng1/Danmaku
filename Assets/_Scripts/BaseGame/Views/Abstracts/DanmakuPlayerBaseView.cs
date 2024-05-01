using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views.Basics;
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
        [ShowInInspector, ReadOnly] public DanmakuSessionBaseHandler SessionHandler;
        
        private void Awake()
        {
            RoleView = transform.GetComponentInChildren<DanmakuRoleBaseView>();
            CardHandView = transform.GetComponentInChildren<DanmakuCardHandBaseView>();
            SessionHandler = transform.GetComponentInChildren<DanmakuSessionBaseHandler>();
        }

        public abstract void InitializeView();


        public abstract void SetupRole(IDanmakuRole playerRoleValue);

        public virtual void StartMainStep(Action finishExecuteCallback)
        {
            CardHandView.AllowCardPlay();
            CardHandView.DisallowCardPlay();
            finishExecuteCallback?.Invoke();
        }

       
        public abstract void AddSession(DanmakuSession session);
        public abstract void RemoveSession(DanmakuSession session);
    }
}