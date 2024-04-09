using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;

namespace _Scripts.CoreGame.InteractionSystems
{
    public class DanmakuBoardModel
    {
        public DanmakuCardDeckModel MainDeckModel { get; protected set;}
        public DanmakuCardDeckModel DiscardDeckModel { get; protected set;}
        public DanmakuCardDeckModel IncidentDeckModel { get; protected set;}
        public DanmakuCardDeckModel DiscardIncidentDeckModel { get; protected set;}
        
        
        public DanmakuBoardModel(List<IDanmakuCard> mainDeck, List<IDanmakuCard> discardDeck, List<IDanmakuCard> incidentDeck)
        {
            MainDeckModel = new DanmakuCardDeckModel(mainDeck);
            DiscardDeckModel = new DanmakuCardDeckModel(discardDeck);
            IncidentDeckModel = new DanmakuCardDeckModel(incidentDeck);
        }
        
    }
}