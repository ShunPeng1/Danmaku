using _Scripts.BaseGame.ScriptableData;
using _Scripts.CoreGame.InteractionSystems;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Basics
{
    public class VRCharacterView : DanmakuCharacterBaseView
    {
        [SerializeField] private Transform _characterTransform;
        [SerializeField] RuntimeAnimatorController _controller;

        private CharacterCardScriptableData _characterData;
        private GameObject _characterObject;
        
        
        public override void SetupCharacter(DanmakuCharacterCardModel chosenCard)
        {
            //_characterObject = Instantiate(chosenCard.CharacterCardData.ModelData, _characterTransform);
            
            _characterData = chosenCard.CharacterCardData;
            LoadModel(chosenCard.CharacterCardData);
        }
        
        // Update is called once per frame
        public void LoadModel(CharacterCardScriptableData characterCardScriptableData)
        {
            GameObject modelPrefab = _characterData.ModelData;
            if (modelPrefab == null) return;
            
            
            GameObject loadedPrefab = Instantiate(modelPrefab, _characterTransform);
            Animator animator = loadedPrefab.GetComponent<Animator>();
            if (animator != null )
            {
                animator.runtimeAnimatorController = _controller;
                loadedPrefab.AddComponent<ModelAnimController>();
            }
                
            _characterObject = loadedPrefab;
        }
    }
}