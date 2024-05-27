using System;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;


namespace _Scripts.BaseGame.ScriptableData
{
    [CreateAssetMenu(fileName = "CharacterCardScriptableData", menuName = "ScriptableData/CharacterCardScriptableData")]
    public class CharacterCardScriptableData : ScriptableObject
    {
        public string CardName;
        public string CardTitle;
        public CardDeckEnum CardDeck;
        public CardExpansionEnum CardExpansion;
        public CardSeasonEnum CardSeason;

        public Sprite CardIllustration;

        public CardRuleScriptableData[] AbilityCardRules;
        public CardRuleScriptableData[] SpellCardRules;

        public GameObject ModelData;
        
        private void OnEnable()
        {
            if (CardIllustration == null)
            {
                CardIllustration = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
            }
        }
    }
}
