using System;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Positions
{
    public class LocalOffsetPosition : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        private void Start()
        {
            transform.localPosition += _offset;
        }
    }
}