using System.Collections;
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
        
        
        private IEnumerator Start()
        {
            
            InteractionController =  new DanmakuInteractionController.Builder(_interactionViewRepo)
                .WithPlayerCount(_playerCount)
                .WithPlayerRoles(_roleSetConfig)
                .WithCardDeck(_deckSetConfig)
                .WithCharacterSet(_characterSetConfig)
                .Build();
            
            yield return null;
            
            InteractionController.StartDrawCharacter(_eachPlayerCharacterChoiceCount);
            
            
            yield return null;
            
            InteractionController.SetupStartingStats(_startupStatsConfig);
            
            yield return null;
            
            InteractionController.StartupReveal();
            
            yield return null;
            
            InteractionController.StartupDraw();
            
            yield return null;
            
            InteractionController.StartGame();
            

        }
        
        
    }
}