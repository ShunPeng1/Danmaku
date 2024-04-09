using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuPlayerBaseView : MonoBehaviour
    {
        [SerializeField]
        private DanmakuRoleBaseView _roleView;
        [SerializeField]
        private DanmakuCardHandBaseView _cardHandView;
        
        
        public DanmakuRoleBaseView RoleView => _roleView;
        public DanmakuCardHandBaseView CardHandView => _cardHandView;
        
        
        public abstract void InitializeView();


    }
}