using AbilitySystem;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Self", menuName = "AbilitySystem/Abilities/Self_Ability")]
public class Ability_SelfTarget : Ability
{
    public async void Execute(Unit source)
    {
        foreach(Effect effect in _effects)
        {
            effect.Execute(source, source);
        }

        if (_hasSecondaryEffects)
        {
            await Task.Delay((int)(_waitUntilSecondaryEffects * 1000f));

            foreach (Effect effect in _secondaryEffects) 
            {
                effect.Execute(source, source);
            }
        }
    }
}