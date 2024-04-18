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
        
        [Header("Controller")]
        public DanmakuInteractionController InteractionController;
        
        
        private void Start()
        {
            
            InteractionController =  new DanmakuInteractionController.Builder(_interactionViewRepo)
                .WithPlayerCount(_playerCount)
                .WithPlayerRoles(_roleSetConfig)
                .WithCardDeck(_deckSetConfig)
                .Build();
            
            InteractionController.SetupStartingStats(_startupStatsConfig);
            
            
            var boardController = new DanmakuBoardController(InteractionController);
            InteractionController.SetBoardController(boardController);
            
            var playerSubController = new DanmakuPlayerController(InteractionController);
            InteractionController.SetPlayerSubController(playerSubController);
            
            playerSubController.StartupReveal();
            boardController.StartupDraw();
            playerSubController.StartGame();


        }
        
        
    }
}