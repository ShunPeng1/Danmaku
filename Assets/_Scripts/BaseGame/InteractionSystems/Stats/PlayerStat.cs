using System;
using _Scripts.CoreGame.InteractionSystems.Interfaces;
using Shun_Utilities;
using UnityEngine;

namespace _Scripts.CoreGame.InteractionSystems.Stats
{
    public class PlayerStat : IStat<int>
    {
        private ObservableData<int> _value;
        private int _minValue;
        private int _maxValue;

        public PlayerStat(int value, int minValue = 0, int maxValue = Int32.MinValue)
        {
            _value = new ObservableData<int>(value);
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public int Get()
        {
            return _value.Value;
        }

        public void Set(int value)
        {
            if (value < _minValue)
            {
                value = _minValue;
            }
            else if (value > _maxValue)
            {
                value = _maxValue;
            }
            _value.Value = value;
        }

        public void Increase(int value)
        {
            if (_value.Value + value > _maxValue)
            {
                value = _maxValue - _value.Value;
            }
            Set(_value.Value + value);
        }

        public void Decrease(int value)
        {
            if (_value.Value - value < _minValue)
            {
                value = _value.Value - _minValue;
            }
            Set(_value.Value - value);
        }

        public void Subscribe(Action<int, int> action)
        {
            _value.OnValueChanged += action;
        }

        public void Unsubscribe(Action<int, int> action)
        {
            _value.OnValueChanged -= action;
        }
    }
}