using System.Collections.Generic;
using _Scripts.BaseGame.ScriptableData;
using UnityEngine;

namespace _Scripts.CoreGame.Configurations
{
    [CreateAssetMenu(fileName = "CharacterSetConfig", menuName = "Configurations/CharacterSetConfig")]
    public class CharacterSetConfig : ScriptableObject
    {
        public List<CharacterCardScriptableData> CharacterCardsData;
    }
}