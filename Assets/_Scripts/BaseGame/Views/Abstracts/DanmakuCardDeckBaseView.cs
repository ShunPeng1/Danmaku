using _Scripts.BaseGame.Views.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuCardDeckBaseView : MonoBehaviour
    {
        [SerializeField] private ViewHolderTypeEnum _holderTypeEnum;
        public ViewHolderTypeEnum HolderTypeEnum { get; set; }
    }
}