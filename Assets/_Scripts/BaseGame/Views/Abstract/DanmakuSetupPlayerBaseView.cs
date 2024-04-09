using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuSetupPlayerBaseView : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private DanmakuPlayerBaseView _playerViewPrefab;
        
        protected readonly Dictionary<DanmakuPlayerModel,DanmakuPlayerBaseView> PlayerModelToViews = new();
        
        public virtual void CreatePlayerViews(List<DanmakuPlayerModel> playerModels)
        {
            foreach (var playerModel in playerModels)
            {
                var playerView = Instantiate(_playerViewPrefab, transform);
                playerView.name = $"PlayerView_{playerModel.PlayerId}";
                PlayerModelToViews.Add(playerModel, playerView);
                
                playerView.InitializeView();
            }
        }
        
        public virtual DanmakuPlayerBaseView GetPlayerView(DanmakuPlayerModel boardModel)
        {
            return PlayerModelToViews[boardModel];
        }

        public abstract void SetupPlayerRoleView(Dictionary<DanmakuPlayerModel, IDanmakuRole> playerToRole);
        public abstract void SetupCardDeckRoleView(DanmakuCardDeckModel cardDeckModel);
        public abstract void SetupPlayerHandView(DanmakuPlayerModel playerModel, DanmakuCardHandModel cardHandModel);


        public abstract void SetupCardDeck(DanmakuCardDeckModel mainDeckModel, DanmakuCardDeckModel discardDeckModel, DanmakuCardDeckModel incidentDeckModel);

        
    }
}