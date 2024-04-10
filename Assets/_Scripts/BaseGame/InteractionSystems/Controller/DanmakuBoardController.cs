using _Scripts.BaseGame.Views;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuBoardController
    {
        private DanmakuInteractionController _danmakuInteractionController;
        private DanmakuPlayerGroupModel PlayerGroupModel => _danmakuInteractionController.PlayerGroupModel;
        private DanmakuBoardModel BoardModel => _danmakuInteractionController.BoardModel;
        
        private DanmakuSetupPlayerBaseView SetupPlayerView => _danmakuInteractionController.InteractionViewRepo.SetupPlayerView;
        
        public DanmakuBoardController(DanmakuInteractionController danmakuInteractionController)
        {
            _danmakuInteractionController = danmakuInteractionController;
        }
        
        
        public void StartupDraw()
        {
            foreach (var player in PlayerGroupModel.Players)
            {
                for (int i = 0; i < player.HandSize.Get(); i++)
                {
                    DrawCard(player);
                }
                
            }
        }
        
        
        private void DrawCard(DanmakuPlayerModel player)
        {
            var card = BoardModel.MainDeckModel.PopCardFront();
            player.CardHandModel.AddCard(card);
            
            var playerHandView = SetupPlayerView.GetPlayerView(player).CardHandView;
            playerHandView.DrawCard(card);
        }
        
    }
}