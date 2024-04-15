using System.Collections.Generic;
using _Scripts.BaseGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.BaseGame.Views
{
    public abstract class DanmakuCardHandBaseView : MonoBehaviour
    {
        public abstract void DrawCard(IDanmakuCard card);
        public abstract void DrawCards(List<IDanmakuCard> cards);
    }
}