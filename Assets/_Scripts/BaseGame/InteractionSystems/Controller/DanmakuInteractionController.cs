using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.Configurations;
using UnityEditor;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuInteractionController
    {
        public DanmakuSetupSubController DanmakuStepSubController { get; private set; }
        private DanmakuInteractionViewRepo _interactionViewRepo;

        public DanmakuInteractionController(DanmakuInteractionViewRepo danmakuInteractionViewRepo)
        {
            _interactionViewRepo = danmakuInteractionViewRepo;
            
        }

        public void SetSubController(DanmakuSetupSubController danmakuStepSubController)
        {
            DanmakuStepSubController = danmakuStepSubController;
            
        }
        
        
        public void StartGame()
        {
            
        }
        
    }
}