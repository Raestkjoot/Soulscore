using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_ApplyTag", menuName = "AbilitySystem/Effects/ApplyTag")]
public class Effect_ApplyTag : Effect
{
    [SerializeField] private EStatusEffect tag;

    public override void Cancel()
    {
    }

    public override void End()
    {
    }

    public override void Execute(Unit source, Unit target)
    {
        target.statusEffect |= tag;
    }
}