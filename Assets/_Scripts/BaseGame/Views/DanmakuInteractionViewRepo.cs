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
        [ShowInInspector] private DanmakuSetupPlayerBaseView _setupPlayerView;
        [ShowInInspector] private DanmakuTurnBaseView _turnView;
        [ShowInInspector] private DanmakuBoardBaseView _boardView;
        
        public DanmakuSetupPlayerBaseView SetupPlayerView => _setupPlayerView ? _setupPlayerView : (_setupPlayerView = gameObject.AddComponent<MockSetupPlayerView>());
        public DanmakuTurnBaseView TurnView => _turnView ? _turnView : (_turnView = gameObject.AddComponent<MockTurnView>());
        public DanmakuBoardBaseView BoardView => _boardView ? _boardView : (_boardView = gameObject.AddComponent<MockBoardView>());
        
        
        private void Awake()
        {
            InitializeViews();
        }

        private void OnValidate()
        {
            InitializeViews();
        }

        private void InitializeViews()
        {
            if (_setupPlayerView == null)
            {
                _setupPlayerView = gameObject.GetComponentInChildren<DanmakuSetupPlayerBaseView>();
            }
            if (_turnView == null)
            {
                _turnView = gameObject.GetComponentInChildren<DanmakuTurnBaseView>();
            }
            if (_boardView == null)
            {
                _boardView = gameObject.GetComponentInChildren<DanmakuBoardBaseView>();
            }
        }
    }
}