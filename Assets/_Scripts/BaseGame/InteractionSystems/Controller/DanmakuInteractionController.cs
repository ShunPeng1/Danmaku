using _Scripts.BaseGame.Views;
using _Scripts.CoreGame.Configurations;
using UnityEditor;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuInteractionController
    {
        public DanmakuSetupSubController DanmakuStepSubController { get; private set; }
        public DanmakuInteractionViewRepo InteractionViewRepo { get; private set; }

        public DanmakuBoardModel BoardModel { get; private set; }
        public DanmakuPlayerGroupModel PlayerGroupModel { get; private set; }
        
        public DanmakuInteractionController(DanmakuInteractionViewRepo danmakuInteractionViewRepo)
        {
            InteractionViewRepo = danmakuInteractionViewRepo;
            
        }
        
        public void SetupModels(DanmakuBoardModel boardModel, DanmakuPlayerGroupModel playerGroupModel)
        {
            BoardModel = boardModel;
            PlayerGroupModel = playerGroupModel;
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