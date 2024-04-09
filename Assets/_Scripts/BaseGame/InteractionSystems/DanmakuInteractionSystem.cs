using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.Configurations;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuInteractionSystem : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private DanmakuInteractionViewRepo _setupPlayerView;
        
        [Header("Configurations")]
        [SerializeField] private int _playerCount;
        [SerializeField] private RoleSetConfig _roleSetConfig;
        [SerializeField] private DeckSetConfig _deckSetConfig;
        
        [Header("Controller")]
        public DanmakuInteractionController InteractionController;
        public DanmakuSessionSubController SessionSubController;
        public DanmakuSetupSubController SetupSubController;
        
        private void Awake()
        {
            InteractionController = new DanmakuInteractionController(_setupPlayerView);
     
            
            SetupSubController =  new DanmakuSetupSubController.Builder(InteractionController, _setupPlayerView.SetupPlayerView)
                .WithPlayerGroup(_playerCount, _roleSetConfig)
                .WithCardDeck(_deckSetConfig)
                .Build();

            
            SessionSubController = new DanmakuSessionSubController();
            
            InteractionController.SetSubController(SetupSubController);
            
        }
        
        private void Start()
        {

        }
        
    }
}