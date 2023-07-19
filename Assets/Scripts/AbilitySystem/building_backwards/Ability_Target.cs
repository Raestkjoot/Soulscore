using AbilitySystem;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Target", menuName = "AbilitySystem/Abilities/Target_Ability")]
public class Ability_Target : Ability
{
    [SerializeField] private float _range;
    public float GetRange() { return _range; }

    public async void Execute(Unit source, Unit target)
    {
        foreach (Effect effect in _effects)
        {
            effect.Execute(source, target);
        }

        if (_hasSecondaryEffects)
        {
            await Task.Delay((int)(_waitUntilSecondaryEffects * 1000f));

            foreach (Effect effect in _secondaryEffects)
            {
                effect.Execute(source, target);
            }
        }
    }
}