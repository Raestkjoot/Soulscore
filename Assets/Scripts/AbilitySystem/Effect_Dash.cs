using UnityEngine;

[CreateAssetMenu(fileName = "DashEffect", menuName = "AbilitySystem/Effects/DashEffect")]
public class Effect_Dash : Effect
{
    public override void Cancel()
    {
    }

    public override void End()
    {
    }

    public override void Execute(Unit source)
    {
        source.ApplyForceInMoveDirection(15f, .2f, 15f);
    }
}