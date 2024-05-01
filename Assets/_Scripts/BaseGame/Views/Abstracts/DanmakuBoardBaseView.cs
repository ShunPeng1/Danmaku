using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using _Scripts.BaseGame.Views.Enum;
using _Scripts.CoreGame.InteractionSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuBoardBaseView : MonoBehaviour
    {
        
        [ShowInInspector, ReadOnly] public DanmakuInteractionViewRepo InteractionViewRepo;
        [ShowInInspector, ReadOnly] public DanmakuCardDeckBaseView MainDeckView;
        [ShowInInspector, ReadOnly] public DanmakuCardDeckBaseView DiscardDeckView;
        [ShowInInspector, ReadOnly] public DanmakuCardDeckBaseView IncidentDeckView;
        private void Awake()
        {
            InitializeViews();
            InitializeInherit();
        }

        protected abstract void InitializeInherit();

        private void InitializeViews()
        {
            InteractionViewRepo = transform.parent.GetComponent<DanmakuInteractionViewRepo>();
            
            var deckViews = transform.GetComponentsInChildren<DanmakuCardDeckBaseView>();
            
            foreach (var deckView in deckViews)
            {
                if (deckView.HolderTypeEnum == ViewHolderTypeEnum.DrawMainDeck)
                {
                    MainDeckView = deckView;
                }
                else if (deckView.HolderTypeEnum == ViewHolderTypeEnum.DiscardMainDeck)
                {
                    DiscardDeckView = deckView;
                }
                
            }
            
        }

        public abstract Dictionary<DanmakuPlayerModel, DanmakuPlayerBaseView> CreatePlayerViews(List<DanmakuPlayerModel> playerModels);
        
        public abstract void DrawCardFromMainDeck(DanmakuPlayerModel playerModel, DanmakuMainDeckCardModel card);
        public abstract void DrawCardFromMainDeck(DanmakuPlayerModel playerModel, List<DanmakuMainDeckCardModel> cards);
        
        public abstract void DiscardCardToDiscardDeck(DanmakuPlayerModel playerModel, DanmakuMainDeckCardModel card);
        public abstract void DiscardCardsToDiscardDeck(DanmakuPlayerModel playerModel, List<DanmakuMainDeckCardModel> cards);
        public abstract void SetupMainDeck(DanmakuCardDeckModel mainDeckModel);
        public abstract void SetupIncidentDeck(DanmakuCardDeckModel incidentDeckModel);
        public abstract void SetupCharacterDeck(DanmakuCardDeckModel characterDeckModel);
        public abstract void DrawCharacterCardsForSelection(DanmakuPlayerModel player, List<DanmakuCharacterCardModel> characterCards);
        public abstract void AddSessionToPlayer(DanmakuPlayerModel player, DanmakuSession session);
        public abstract void RemoveSessionFromPlayer(DanmakuPlayerModel player, DanmakuSession session);
    }
}