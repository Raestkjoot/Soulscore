using System.Collections.Generic;
using UnityEngine;

namespace AttributeSystem
{
    public class AttributesContainer : MonoBehaviour
    {
        [SerializeField]
        private Attribute[] _initAttributes;

        private Dictionary<EAttribute, Attribute> _attributes;
        private ILogr _logger = new ConsoleLogger();

        public void Initialize()
        {
            _attributes = new Dictionary<EAttribute, Attribute>();

            foreach (Attribute attribute in _initAttributes)
            {
                attribute.Initialize();
                _attributes.Add(attribute.GetName(), attribute);
            }
        }

        public float GetAttributeCurValue(EAttribute name)
        {
            if (_attributes.TryGetValue(name, out Attribute attr))
            {
                return attr.GetCurValue();
            }

            _logger.Warn(string.Format("Attribute ({0}) not found!", name));
            return 0;
        }

        public void ApplyModifierForDuration(EAttribute name, AttributeModifier modifier, float duration)
        {
            if (_attributes.TryGetValue(name, out Attribute attr))
            {
                attr.AddModifierForDuration(modifier, duration);
            }
        }

        public void ApplyModifier(EAttribute name, AttributeModifier modifier)
        {
            if (_attributes.TryGetValue(name, out Attribute attr))
            {
                attr.AddModifier(modifier);
            }
        }
    } 
}