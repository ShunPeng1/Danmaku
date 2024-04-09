using System.Collections.Generic;
using _Scripts.CoreGame.InteractionSystems;
using _Scripts.CoreGame.InteractionSystems.Roles;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public class DanmakuInteractionViewRepo : MonoBehaviour
    {
        [SerializeField] private DanmakuSetupPlayerBaseView _setupPlayerView;
        [SerializeField] private DanmakuPlayerBaseView _playerView;
        [SerializeField] private DanmakuRoleBaseView _roleView;
        [SerializeField] private DanmakuCardBaseView _cardView;
        [SerializeField] private DanmakuCardHandBaseView _cardHandView;
        [SerializeField] private DanmakuCardDeckBaseView _cardDeckView;
        [SerializeField] private DanmakuCardBaseView _cardDiscardView;
        
        public DanmakuSetupPlayerBaseView SetupPlayerView => _setupPlayerView ? _setupPlayerView : (_setupPlayerView = GetComponent<DanmakuSetupPlayerBaseView>());
        public DanmakuPlayerBaseView PlayerView => _playerView ? _playerView : (_playerView = GetComponent<DanmakuPlayerBaseView>());
        public DanmakuRoleBaseView RoleView => _roleView ? _roleView : (_roleView = GetComponent<DanmakuRoleBaseView>());
        public DanmakuCardBaseView CardView => _cardView ? _cardView : (_cardView = GetComponent<DanmakuCardBaseView>());
        public DanmakuCardHandBaseView CardHandView => _cardHandView ? _cardHandView : (_cardHandView = GetComponent<DanmakuCardHandBaseView>());
        public DanmakuCardDeckBaseView CardDeckView => _cardDeckView ? _cardDeckView : (_cardDeckView = GetComponent<DanmakuCardDeckBaseView>());
        public DanmakuCardBaseView CardDiscardView => _cardDiscardView ? _cardDiscardView : (_cardDiscardView = GetComponent<DanmakuCardBaseView>());
        
        
    }
}