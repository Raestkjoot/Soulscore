using System.Collections.Generic;
using UnityEngine;

namespace AttributeSystem
{
    [CreateAssetMenu(fileName = "NewAttributesContainer", menuName = "AttributesSystem/AttributesContainer")]
    public class AttributesContainer : ScriptableObject
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
                attribute.SetCurValueToBaseValue();
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
    } 
}