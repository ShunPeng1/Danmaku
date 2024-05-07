﻿using System;
using System.Collections.Generic;
using _Scripts.BaseGame.Views.Default;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public class DanmakuInteractionViewRepo : MonoBehaviour
    {
        [Header("Serialized Views")]
        [ShowInInspector, ReadOnly] public DanmakuTurnBaseView TurnView;
        [ShowInInspector, ReadOnly] public DanmakuBoardBaseView BoardView;
        [ShowInInspector, ReadOnly] protected Dictionary<DanmakuPlayerModel, DanmakuPlayerBaseView> PlayerModelToViews;

        
        private void Awake()
        {
            InitializeViews();
        }

        private void InitializeViews()
        {
            TurnView = gameObject.GetComponentInChildren<DanmakuTurnBaseView>();
            BoardView = gameObject.GetComponentInChildren<DanmakuBoardBaseView>();
        }

        public virtual void CreatePlayerViews(List<DanmakuPlayerModel> playerModels)
        {
            PlayerModelToViews = BoardView.CreatePlayerViews(playerModels);
        }
        
        public virtual DanmakuPlayerBaseView GetPlayerView(DanmakuPlayerModel boardModel)
        {
            return PlayerModelToViews[boardModel];
        }

        public void SetupPlayerRoleView(Dictionary<DanmakuPlayerModel, IDanmakuRole> playerToRole)
        {
            foreach (var playerRole in playerToRole)
            {
                var playerView = GetPlayerView(playerRole.Key);
                playerView.RoleView.SetupRole(playerRole.Value);
            }
        }

        public void SetupMainDeck(DanmakuCardDeckModel drawMainDeckModel)
        {
            BoardView.SetupMainDeck(drawMainDeckModel);
        }

        public void SetupIncidentDeck(DanmakuCardDeckModel incidentDeckModel)
        {
            BoardView.SetupIncidentDeck(incidentDeckModel);
        }

        public void SetupCharacterDeck(DanmakuCardDeckModel characterDeckModel)
        {
            BoardView.SetupCharacterDeck(characterDeckModel);
        }
        
        
    }
}