using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AttributeSystem
{
    [Serializable]
    public class Attribute
    {
        [SerializeField]
        private EAttribute _name;
        [SerializeField]
        private float _baseValue;

        private float _curValue;
        private List<AttributeModifier> _modifiers = new List<AttributeModifier>();

        public EAttribute GetName()
        {
            return _name;
        }

        public void Initialize() =>
            SetCurValueToBaseValue();

        public float GetCurValue()
        {
            CalculateModifiers();
            return _curValue;
        }

        public async void AddModifierForDuration(AttributeModifier modifier, float duration)
        {
            _modifiers.Add(modifier);

            await Task.Delay((int)(duration * 1000));

            RemoveModifier(modifier);
        }

        private void RemoveModifier(AttributeModifier modifier)
        {
            _modifiers.Remove(modifier);
            CalculateModifiers();
        }

        private void SetCurValueToBaseValue()
        {
            _curValue = _baseValue;
        }

        private void CalculateModifiers()
        {
            float newValue = _baseValue;
            foreach (AttributeModifier modifier in _modifiers)
            {
                modifier.Apply(ref newValue);
            }

            _curValue = newValue;
        }
    }
}