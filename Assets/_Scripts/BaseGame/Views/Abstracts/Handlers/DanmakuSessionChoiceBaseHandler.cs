using System;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class DanmakuSessionChoiceBaseHandler : MonoBehaviour
    {
        protected DanmakuSessionBaseHandler SessionHandler;
        protected DanmakuInteractionSystem InteractionSystem;
        public DanmakuSessionChoice SessionChoice { get; protected set; }
        
        public Action<DanmakuSessionChoice> OnSessionChoiceSet;
        public Action<DanmakuSessionChoice> OnSessionChoiceUnset;

        protected virtual void Awake()
        {
            SessionHandler = transform.GetComponentInParent<DanmakuSessionBaseHandler>();
            InteractionSystem = transform.GetComponentInParent<DanmakuInteractionSystem>();
        }

        public void SetSessionChoice(DanmakuSessionChoice sessionChoice)
        {
            SessionChoice = sessionChoice;
            OnSessionChoiceSet?.Invoke(SessionChoice);
        }
        
        public void UnsetSessionChoice()
        {
            SessionChoice = null;
            OnSessionChoiceUnset?.Invoke(SessionChoice);
        }
        
    }
}