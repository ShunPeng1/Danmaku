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
        public DanmakuPlayerSubController PlayerSubController;
        public DanmakuSetupSubController SetupSubController;
        
        private void Awake()
        {
            InteractionController = new DanmakuInteractionController(_setupPlayerView);
            
            DanmakuSetupSubController.Builder builder = new DanmakuSetupSubController.Builder();
            SetupSubController = builder.WithPlayerGroupModel(_playerCount, _roleSetConfig)
                .Build(InteractionController, _setupPlayerView.SetupPlayerView);
            
            
            PlayerSubController = new DanmakuPlayerSubController();
            
            InteractionController.SetSubController(SetupSubController);
            
        }
        
        private void Start()
        {
            SetupSubController.SetupPlayerRole(_roleSetConfig);
           
        }
        
    }
}