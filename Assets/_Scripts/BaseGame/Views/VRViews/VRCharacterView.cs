using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCharacterView : DanmakuCharacterBaseView
    {
        [SerializeField] private Transform _characterTransform;
        
        private GameObject _characterObject;
        public override void SetupCharacter(DanmakuCharacterCardModel chosenCard)
        {
            _characterObject = Instantiate(chosenCard.CharacterCardData.ModelData, _characterTransform);
        }
    }
}