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
        public DanmakuCardDeckModel CharacterDeckModel { get; protected set;}

        protected DanmakuBoardModel() { }

        public class Builder
        {
            private readonly DanmakuCardDeckModel _mainDeck;
            private readonly DanmakuCardDeckModel _discardDeck = new DanmakuCardDeckModel(new List<IDanmakuCard>());
            private DanmakuCardDeckModel _incidentDeck;
            private readonly DanmakuCardDeckModel _discardIncidentDeck = new DanmakuCardDeckModel(new List<IDanmakuCard>());
            private DanmakuCardDeckModel _characterDeck;
            
            public Builder(DanmakuCardDeckModel mainDeck)
            {
                _mainDeck = mainDeck;
            }
            
            public Builder SetIncidentDeck(DanmakuCardDeckModel incidentDeck)
            {
                _incidentDeck = incidentDeck;
                return this;
            }
            
            public Builder SetCharacterDeck(DanmakuCardDeckModel characterDeck)
            {
                _characterDeck = characterDeck;
                return this;
            }

            public DanmakuBoardModel Build()
            {
                return new DanmakuBoardModel
                {
                    MainDeckModel = _mainDeck,
                    DiscardDeckModel = _discardDeck,
                    IncidentDeckModel = _incidentDeck,
                    DiscardIncidentDeckModel = _discardIncidentDeck,
                    CharacterDeckModel = _characterDeck
                };
            }
        }
    }
}