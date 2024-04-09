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
        
        
        public DanmakuBoardModel(DanmakuCardDeckModel mainDeck, DanmakuCardDeckModel discardDeck, DanmakuCardDeckModel incidentDeck)
        {
            MainDeckModel = mainDeck;
            DiscardDeckModel = discardDeck;
            IncidentDeckModel = incidentDeck;
        }
        
    }
}