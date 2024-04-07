using _Scripts.CoreGame.InteractionSystems.Interfaces;
using UnityEngine;

namespace _Scripts.BaseGame.ScriptableData
{
    
    [CreateAssetMenu(fileName = "DeckCardScriptableData", menuName = "ScriptableData/DeckCardScriptableData")]
    public class DeckCardScriptableData : ScriptableObject
    {
        public string CardName;
        public string CardTitle;
        public CardDeckEnum CardDeck;
        public CardExpansionEnum CardExpansion;
        public int PointValue;
        public CardRuleScriptableData[] CardRulesScriptableData;
        
        public Sprite CardIllustration;
        
        
    }
}