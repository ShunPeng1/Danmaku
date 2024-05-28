using System.Collections.Generic;
using _Scripts.BaseGame.Views.Abstracts.Visualizers;
using UnityEngine;

namespace _Scripts.BaseGame.Views.VR.Visualizers
{
    public class ShootCardExecutionVisualizer : DanmakuCardExecutionBaseVisualizer
    {
        [SerializeField] ProjectileMover _projectileMover;
        public override void Visualize(GameObject activatorView, List<GameObject> targetablesView)
        {
            var target = targetablesView[0];
            
            
            _projectileMover.transform.forward = target.transform.position - activatorView.transform.position;
            
            
        }
    }
}