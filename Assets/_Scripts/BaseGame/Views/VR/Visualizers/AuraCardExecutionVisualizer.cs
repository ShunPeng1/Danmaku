using System.Collections.Generic;
using _Scripts.BaseGame.Views.Abstracts.Visualizers;
using UnityEngine;

namespace _Scripts.BaseGame.Views.VR.Visualizers
{
    public class AuraCardExecutionVisualizer : DanmakuCardExecutionBaseVisualizer
    {
        [SerializeField] private float _auraRadius;
        [SerializeField] private float _duration;
        
        
        public override void Visualize(GameObject activatorView, List<GameObject> targetViews)
        {
            transform.localScale = Vector3.one * _auraRadius;
            
            Invoke(nameof(DestroyAura), _duration);
        }


        private void DestroyAura()
        {
            Destroy(gameObject);
        }


    }
}