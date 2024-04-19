using System;
using TMPro;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRMainDeckCardView : DanmakuMainDeckCardBaseView
    {
        [SerializeField] private TMP_Text _cardNameText;

        private void Start()
        {
            _cardNameText.text = CardModel.DeckCardScriptableData.CardName;
        }
    }
}