using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views.Default;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public class DanmakuInteractionViewRepo : MonoBehaviour
    {
        [Header("Serialized Views")]
        [SerializeField] private DanmakuSetupPlayerBaseView _setupPlayerView;
        [SerializeField] private DanmakuTurnBaseView _turnView;
        [SerializeField] private DanmakuBoardBaseView _boardView;
        
        public DanmakuSetupPlayerBaseView SetupPlayerView => _setupPlayerView ? _setupPlayerView : (_setupPlayerView = gameObject.AddComponent<MockSetupPlayerView>());
        public DanmakuTurnBaseView TurnView => _turnView ? _turnView : (_turnView = gameObject.AddComponent<MockTurnView>());
        public DanmakuBoardBaseView BoardView => _boardView ? _boardView : (_boardView = gameObject.AddComponent<MockBoardView>());
        
        
        private void Awake()
        {
            
        }

    }
}