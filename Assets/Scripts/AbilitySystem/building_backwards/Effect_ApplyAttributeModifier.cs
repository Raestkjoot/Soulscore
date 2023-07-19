using AttributeSystem;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "Effect_AttrModifier", menuName = "AbilitySystem/Effects/AttrModifier")]
    public class Effect_ApplyAttributeModifier : Effect
    {
        [SerializeField] private EAttribute _attribute;
        [SerializeField] private float _magnitude;
        [SerializeField] private EModifierType _modifierType;

        public override void Cancel()
        {}

        public override void End()
        {}

        public override void Execute(Unit source, Unit target)
        {
            AttributeModifier modifier = new AttributeModifier();
            modifier.Magnitude = _magnitude;
            modifier.ModifierType = _modifierType;
            source.GetAttributes().ApplyModifier(_attribute, modifier);
        }
    }
}