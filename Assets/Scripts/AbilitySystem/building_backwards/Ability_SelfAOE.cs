using AbilitySystem;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_SelfAOE", menuName = "AbilitySystem/Abilities/Ability_SelfAOE")]
public class Ability_SelfAOE : Ability
{
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private float _radius;

    public ContactFilter2D GetContactFilter() { return _contactFilter; }
    public float GetRadius() { return _radius; }

    public async void Execute(Unit source, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            foreach (Effect effect in _effects)
            {
                effect.Execute(source, target);
            } 
        }

        if (_hasSecondaryEffects)
        {
            await Task.Delay((int)(_waitUntilSecondaryEffects * 1000f));

            foreach (Unit target in targets)
            {
                foreach (Effect effect in _effects)
                {
                    effect.Execute(source, target);
                }
            }
        }
    }
}