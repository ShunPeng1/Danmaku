using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.CoreGame.Configurations;

namespace _Scripts.CoreGame.InteractionSystems.Setups
{
    public class DanmakuCardSetupDirector
    {
        private DeckSetConfig _deckSetConfig;
        
        public DanmakuCardSetupDirector(DeckSetConfig deckSetConfig)
        {
            _deckSetConfig = deckSetConfig;
        }
        
        
        public List<IDanmakuCard> SetupMainDeckCards()
        {
            List<IDanmakuCard> mainDeckCards = new List<IDanmakuCard>();
            foreach (var deckCardData in _deckSetConfig.DeckCardsData)
            {
                foreach (var cardRuleData in deckCardData.CardRulesScriptableData)
                {
                    
                    
                }
                
                
            }

            return mainDeckCards;
            
        }
    }
}