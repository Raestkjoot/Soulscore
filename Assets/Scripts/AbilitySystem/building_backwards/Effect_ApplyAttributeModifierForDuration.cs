using AttributeSystem;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "Effect_AttrModifierForDuration", menuName = "AbilitySystem/Effects/AttrModifierForDuration")]
    public class Effect_ApplyAttributeModifierForDuration : Effect
    {
        [SerializeField] private EAttribute _attribute;
        [SerializeField] private float _magnitude;
        [SerializeField] private EModifierType _modifierType;
        [SerializeField] private float _duration;

        public override void Cancel()
        {}

        public override void End()
        {}

        public override void Execute(Unit source, Unit target)
        {
            AttributeModifier modifier = new AttributeModifier();
            modifier.Magnitude = _magnitude;
            modifier.ModifierType = _modifierType;
            source.GetAttributes().ApplyModifierForDuration(_attribute, modifier, _duration);
        }
    }
}