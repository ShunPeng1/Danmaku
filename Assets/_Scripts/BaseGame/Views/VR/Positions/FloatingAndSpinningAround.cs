using System;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Positions
{
    public class FloatingAndSpinningAround : MonoBehaviour
    {
        [Header("Spinning Settings")]
        [SerializeField] private float _spinSpeed = 10f;
        [SerializeField] private Vector3 _axis = Vector3.right;

        [Header("Floating Settings")]
        [SerializeField] private Vector3 _floatAxis = Vector3.up;
        [SerializeField] private float _floatSpeed = 0.5f;
        [SerializeField] private float _floatRange = 0.5f;
        
        private Vector3 _startPos;

        private void Start()
        {
            _startPos = transform.position;
        }

        private void Update()
        {
            transform.Rotate(_axis, _spinSpeed * Time.deltaTime);
            
            
            var multiplier = Mathf.Sin(Time.time * _floatSpeed) * _floatRange;
            transform.position = _startPos + _floatAxis * multiplier;
            
            
            
        }
        
    }
}