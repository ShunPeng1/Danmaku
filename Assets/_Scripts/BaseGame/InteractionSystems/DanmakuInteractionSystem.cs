using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuInteractionSystem : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private DanmakuInteractionViewRepo _interactionViewRepo;
        
        [Header("Configurations")]
        [SerializeField] private int _playerCount;
        [SerializeField] private int _eachPlayerCharacterChoiceCount = 2;
        [SerializeField] private RoleSetConfig _roleSetConfig;
        [SerializeField] private DeckSetConfig _deckSetConfig;
        [SerializeField] private StartupStatsConfig _startupStatsConfig;
        [SerializeField] private CharacterSetConfig _characterSetConfig;
        
        [Header("Controller")]
        public DanmakuInteractionController InteractionController;
        
        
        public Action OnFinishDrawCharacter;
        public Action OnFinishSetupStats;
        public Action OnFinishReveal;
        public Action OnFinishDraw;
        public Action OnFinishStartGame;
        
        
        private IEnumerator Start()
        {
            
            InteractionController =  new DanmakuInteractionController.Builder(_interactionViewRepo)
                .WithPlayerCount(_playerCount)
                .WithPlayerRoles(_roleSetConfig)
                .WithCardDeck(_deckSetConfig)
                .WithCharacterSet(_characterSetConfig)
                .WithStartGameSequence(new List<Action>()
                {
                    StartDrawCharacter,
                    StartSetupStats,
                    StartReveal,
                    StartDraw,
                    StartGame
                })
                .Build();
            
            
            yield return null;
            
            InteractionController.StartNextSequence();
            
        }
        
        private void StartDrawCharacter()
        {
            InteractionController.StartDrawCharacter(_eachPlayerCharacterChoiceCount);
            OnFinishDrawCharacter?.Invoke();
        }
        
        private void StartSetupStats()
        {
            InteractionController.SetupStartingStats(_startupStatsConfig);
            OnFinishSetupStats?.Invoke();
        }
        
        private void StartReveal()
        {
            InteractionController.StartupReveal();
            OnFinishReveal?.Invoke();
        }
        
        private void StartDraw()
        {
            InteractionController.StartupDraw();
            OnFinishDraw?.Invoke();
        }
        
        private void StartGame()
        {
            InteractionController.StartGame();
            OnFinishStartGame?.Invoke();
        }
        
    }
}