using _Scripts.BaseGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Abstracts
{
    public abstract class DanmakuCardBaseView : MonoBehaviour
    {
        public IDanmakuCard CardModel { get; protected set; }

        public abstract void SetCardModel(IDanmakuCard card);
        
    }
}