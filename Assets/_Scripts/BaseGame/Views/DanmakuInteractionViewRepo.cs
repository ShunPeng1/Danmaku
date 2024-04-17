using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views.Default;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public class DanmakuInteractionViewRepo : MonoBehaviour
    {
        [Header("Serialized Views")]
        [ShowInInspector, ReadOnly] public DanmakuSetupPlayerBaseView SetupPlayerView;
        [ShowInInspector, ReadOnly] public DanmakuTurnBaseView TurnView;
        [ShowInInspector, ReadOnly] public DanmakuBoardBaseView BoardView;
        
        private void Awake()
        {
            InitializeViews();
        }

        private void InitializeViews()
        {
            SetupPlayerView = gameObject.GetComponentInChildren<DanmakuSetupPlayerBaseView>();
       
            TurnView = gameObject.GetComponentInChildren<DanmakuTurnBaseView>();
       
            BoardView = gameObject.GetComponentInChildren<DanmakuBoardBaseView>();
        
        }
    }
}