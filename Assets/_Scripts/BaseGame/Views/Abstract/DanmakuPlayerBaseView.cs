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
        
        public DanmakuRoleBaseView RoleView { get; set; }
        public DanmakuCardHandBaseView CardHandView { get; set; }
        public DanmakuCardDeckBaseView CardDeckView { get; set; }        
        
        public abstract void InitializeView();
        
        
    }
}