using AbilitySystem;
using AttributeSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_DashToLocation", menuName = "AbilitySystem/Effects/DashToLocation")]
public class Effect_DashToLocation : Effect
{
    [SerializeField] private float _dashDuration;

    // turn off movement for duration
    // move towards location for duration
    public override void Cancel()
    {}

    public override void End()
    {}

    public override void Execute(Unit source, Unit target)
    {
        float dist = Vector2.Distance(source.transform.position, target.transform.position);
        Vector2 dir = target.transform.position - source.transform.position;
        

        AttributeModifier nullMovementModifier = new AttributeModifier();
        nullMovementModifier.Magnitude = 0;
        nullMovementModifier.ModifierType = EModifierType.Override;
        source.GetAttributes().ApplyModifierForDuration(EAttribute.MovementSpeed, nullMovementModifier, _dashDuration);

        source.ApplyForce((dist / _dashDuration), dir, _dashDuration, 0f);
    }
}