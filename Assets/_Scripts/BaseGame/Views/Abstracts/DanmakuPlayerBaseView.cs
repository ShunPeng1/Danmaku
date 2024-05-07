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
        [ShowInInspector, ReadOnly] public DanmakuCharacterBaseView CharacterView;
        
        public DanmakuPlayerModel PlayerModel { get; protected set;}

        private void Awake()
        {
            RoleView = transform.GetComponentInChildren<DanmakuRoleBaseView>();
            CardHandView = transform.GetComponentInChildren<DanmakuCardHandBaseView>();
            SessionHandler = transform.GetComponentInChildren<DanmakuSessionBaseHandler>();
            CharacterView = transform.GetComponentInChildren<DanmakuCharacterBaseView>();
        }

        public virtual void InitializeView(DanmakuPlayerModel playerModel)
        {
            PlayerModel = playerModel;
        }
        
        public virtual void AllowCardPlay()
        {
            CardHandView.AllowCardPlay();
        }
        
        public virtual void DisallowCardPlay()
        {
            CardHandView.DisallowCardPlay();
        }

        
    }
}