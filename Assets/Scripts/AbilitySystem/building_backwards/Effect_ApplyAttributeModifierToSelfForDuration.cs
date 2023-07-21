using AttributeSystem;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "Effect_AttrModifierToSelfForDuration", menuName = "AbilitySystem/Effects/AttrModifierToSelfForDuration")]
    public class Effect_ApplyAttributeModifierToSelfForDuration : Effect
    {
        [SerializeField] private EAttribute _attribute;
        [SerializeField] private float _magnitude;
        [SerializeField] private EModifierType _modifierType;
        [SerializeField] private float _duration;
        // TODO: tags on target that activate bonus
        [SerializeField] private float _bonus;

        public override void Cancel()
        { }

        public override void End()
        { }

        public override void Execute(Unit source, Unit target)
        {
            AttributeModifier modifier = new AttributeModifier();
            modifier.ModifierType = _modifierType;

            if (target != null) // TODO: check tags
            {
                modifier.Magnitude = _magnitude + _bonus;
            }
            else
            {
                modifier.Magnitude = _magnitude;
            }

            source.GetAttributes().ApplyModifierForDuration(_attribute, modifier, _duration);
        }
    }
}