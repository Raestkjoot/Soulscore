using AbilitySystem;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_Cone", menuName = "AbilitySystem/Abilities/Cone")]
public class Ability_Cone : Ability
{
    [SerializeField] private float _radius;
    [SerializeField] private float _maxNormalAngle;
    public float GetRadius() { return _radius; }
    public float GetMaxNormalAngle() { return _maxNormalAngle; }

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