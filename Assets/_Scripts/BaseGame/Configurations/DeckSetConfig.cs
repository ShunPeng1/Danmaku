using System;
using System.Collections.Generic;
using _Scripts.BaseGame.ScriptableData;
using UnityEngine;

namespace _Scripts.CoreGame.Configurations
{
    [CreateAssetMenu(fileName = "DeckSetConfig", menuName = "DeckSetConfig")]
    public class DeckSetConfig : ScriptableObject
    {
        [SerializeField] private List<DeckCard> _deckCardsData;

        public List<DeckCardScriptableData> DeckCardsData
        {
            get
            {
                List<DeckCardScriptableData> deckCardScriptableData = new ();
                foreach (var deckCard in _deckCardsData)
                {
                    for (var i = 0; i < deckCard.Amount; i++)
                    {
                        deckCardScriptableData.Add(deckCard.DeckCardData);
                    }
                }
                
                return deckCardScriptableData; 
            }
    
        }
        
        
        [Serializable]
        public class DeckCard
        {
            public DeckCardScriptableData DeckCardData;
            public int Amount;
        }
        
    }
}