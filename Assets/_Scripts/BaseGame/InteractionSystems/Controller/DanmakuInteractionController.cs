using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.Configurations;
using UnityEditor;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuInteractionController
    {
        public DanmakuStepSubController DanmakuStepSubController { get; private set; }
        private DanmakuInteractionViewRepo _interactionViewRepo;

        public DanmakuInteractionController(DanmakuInteractionViewRepo danmakuInteractionViewRepo)
        {
            _interactionViewRepo = danmakuInteractionViewRepo;
            
        }

        public void SetSubController(DanmakuStepSubController danmakuStepSubController)
        {
            DanmakuStepSubController = danmakuStepSubController;
            
        }

        public void Setup(RoleSetConfig roleSetConfig)
        {
            DanmakuStepSubController.SetupPlayerRole(roleSetConfig);
            
        }
        
        public void StartGame()
        {
            DanmakuStepSubController.StartGame();
        }
        
    }
}