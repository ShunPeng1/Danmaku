using System.Collections.Generic;
using _Scripts.BaseGame.ScriptableData;
using UnityEngine;

namespace _Scripts.CoreGame.Configurations
{
    [CreateAssetMenu(fileName = "DeckSetConfig", menuName = "Configurations/DeckSetConfig")]
    public class DeckSetConfig : ScriptableObject
    {
        [SerializeField] private DeckCard[] _deckCardsData;
        
        public List<DeckCardScriptableData> DeckCardsData
        {
            get
            {
                var deckCardsData = new List<DeckCardScriptableData>();
                for (var i = 0; i < _deckCardsData.Length; i++)
                {
                    for (var j = 0; j < _deckCardsData[i].Amount; j++)
                    {
                        deckCardsData.Add(_deckCardsData[i].DeckCardData);
                    }
                }
                return deckCardsData;
            }
        }
        
        [System.Serializable]
        public class DeckCard
        {
            public DeckCardScriptableData DeckCardData;
            public int Amount;
        }
    }
}