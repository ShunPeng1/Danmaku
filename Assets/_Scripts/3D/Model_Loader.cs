using _Scripts.BaseGame.ScriptableData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Loader : MonoBehaviour
{
    [SerializeField]
    RuntimeAnimatorController controller;

    [SerializeField] //serialized for testing only
    CharacterCardScriptableData characterData;

    private void Start()
    {
        //testing only
        LoadModel(characterData);
    }

    // Update is called once per frame
    public void LoadModel(CharacterCardScriptableData characterCardScriptableData)
    {
        characterData = characterCardScriptableData;
        GameObject modelPrefab = characterData.ModelData;
        if (modelPrefab != null )
        {
            GameObject loadedprefab = Instantiate(modelPrefab);
            Animator animator = loadedprefab.GetComponent<Animator>();
            if (animator != null )
            {
                animator.runtimeAnimatorController = controller;
                loadedprefab.AddComponent<Model_Anim_Controller>();
            }
        }
    }
}
