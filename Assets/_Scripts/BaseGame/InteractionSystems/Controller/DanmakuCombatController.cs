using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.InteractionSystems.Stats;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuCombatController
    {
        private readonly DanmakuInteractionController _danmakuInteractionController;
        private DanmakuBoardModel BoardModel => _danmakuInteractionController.BoardModel;
        
        private readonly Stack<DanmakuCardExecutionParameter> _playedCards = new (); 
        
        public DanmakuCombatController(DanmakuInteractionController danmakuInteractionController)
        {
            _danmakuInteractionController = danmakuInteractionController;
        }
        

        public void AddCardExecution(DanmakuCardExecutionParameter danmakuCardExecutionParameter)
        {
            // Remove from hand if is player
            if (danmakuCardExecutionParameter.Activator is DanmakuPlayerModel danmakuPlayerModel)
            {
                danmakuPlayerModel.DeckCardHandModel.RemoveCard(danmakuCardExecutionParameter.Card);
            }
           
            
            // Add to played cards
            _playedCards.Push(danmakuCardExecutionParameter);
            
            danmakuCardExecutionParameter.Card.RevealCard();
            
        }
        
        
        
        public void ResolveCombat()
        {
            while (_playedCards.TryPop(out var cardExecution))
            {
                cardExecution.Card.ExecuteCard(cardExecution.DanmakuCardRule, cardExecution.Activator, cardExecution.Targetable);
                
                BoardModel.DiscardDeckModel.AddCard(cardExecution.Card);
            }
        }
        
        
    }
}