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
        
        [Header("Controller")]
        public DanmakuInteractionController InteractionController;
        public DanmakuSessionSubController SessionSubController;
        public DanmakuStepSubController StepSubController;
        
        private void Awake()
        {
            InteractionController = new DanmakuInteractionController(_setupPlayerView);
            
            DanmakuStepSubController.Builder builder = new DanmakuStepSubController.Builder();
            StepSubController = builder.WithPlayerGroupModel(_playerCount, _roleSetConfig)
                .Build(InteractionController, _setupPlayerView.SetupPlayerView);
            
            
            SessionSubController = new DanmakuSessionSubController();
            
            InteractionController.SetSubController(StepSubController);
            
        }
        
        private void Start()
        {
            StepSubController.SetupPlayerRole(_roleSetConfig);
            StepSubController.StartGame();
        }
        
    }
}