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
        [SerializeField] private RoleSetConfig _roleSetConfig;
        [SerializeField] private DeckSetConfig _deckSetConfig;
        [SerializeField] private StartupStatsConfig _startupStatsConfig;
        [SerializeField] private CharacterSetConfig _characterSetConfig;
        
        [Header("Controller")]
        public DanmakuInteractionController InteractionController;
        
        
        private void Start()
        {
            
            InteractionController =  new DanmakuInteractionController.Builder(_interactionViewRepo)
                .WithPlayerCount(_playerCount)
                .WithPlayerRoles(_roleSetConfig)
                .WithCardDeck(_deckSetConfig)
                .WithCharacterSet(_characterSetConfig)
                .Build();
            
            InteractionController.SetupStartingStats(_startupStatsConfig);
            
            InteractionController.StartupReveal();
            
            InteractionController.StartupDraw();
            
            InteractionController.StartGame();
            

        }
        
        
    }
}