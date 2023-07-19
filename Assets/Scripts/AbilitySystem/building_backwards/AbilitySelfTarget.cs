using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_SelfTarget", menuName = "AbilitySystem/Self_Ability")]
public class AbilitySelfTarget : Ability
{
    public void Execute(Unit source)
    {
        foreach(Effect effect in _effects)
        {
            effect.Execute(source, source);
        }
    }
}