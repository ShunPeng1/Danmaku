using System;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class DanmakuCardPlayBaseView : MonoBehaviour
    {
        protected DanmakuSessionBaseHandler SessionHandler;
        public DanmakuSessionChoice SessionChoice { get; protected set; }

        protected virtual void Awake()
        {
            SessionHandler = transform.GetComponentInParent<DanmakuSessionBaseHandler>();
        }

        public virtual void SetSessionChoice(DanmakuSessionChoice sessionChoice)
        {
            SessionChoice = sessionChoice;
        }
        
        public virtual void UnsetSessionChoice()
        {
            SessionChoice = null;
        }
        
    }
}