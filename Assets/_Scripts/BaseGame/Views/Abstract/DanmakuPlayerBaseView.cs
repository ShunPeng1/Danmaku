using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuPlayerBaseView : MonoBehaviour
    {
        [SerializeField]
        private DanmakuRoleBaseView _roleView;
        [SerializeField]
        private DanmakuCardHandBaseView _cardHandView;
        [SerializeField]
        private DanmakuCardDeckBaseView _cardDeckView;
        
        public DanmakuRoleBaseView RoleView => _roleView;
        public DanmakuCardHandBaseView CardHandView => _cardHandView;
        public DanmakuCardDeckBaseView CardDeckView => _cardDeckView;   
        
        public abstract void InitializeView();
        
        
    }
}