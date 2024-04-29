using _Scripts.BaseGame.ScriptableData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoader : MonoBehaviour
{
    [SerializeField]
    RuntimeAnimatorController _controller;

    [SerializeField] //serialized for testing only
    CharacterCardScriptableData _characterData;

    private void Start()
    {
        //testing only
        LoadModel(_characterData);
    }

    // Update is called once per frame
    public void LoadModel(CharacterCardScriptableData characterCardScriptableData)
    {
        _characterData = characterCardScriptableData;
        GameObject modelPrefab = _characterData.ModelData;
        if (modelPrefab != null )
        {
            GameObject loadedprefab = Instantiate(modelPrefab);
            Animator animator = loadedprefab.GetComponent<Animator>();
            if (animator != null )
            {
                animator.runtimeAnimatorController = _controller;
                loadedprefab.AddComponent<ModelAnimController>();
            }
        }
    }
}
