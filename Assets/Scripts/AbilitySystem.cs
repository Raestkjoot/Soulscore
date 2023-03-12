using UnityEngine;

public class AbilitySystem : Singleton<AbilitySystem>
{
    private Ability[] abilities;

    // Overloads for casting abilities
    public void Cast(int abilityID, Unit fromUnit) => 
        CastAbilitySelfcast(abilityID, fromUnit);
    public void Cast(int abilityID, Unit fromUnit, Unit targetUnit) => 
        CastAbilityByTarget(abilityID, fromUnit, targetUnit);
    public void Cast(int abilityID, Unit fromUnit, Vector3 targetPosition) =>
        CastAbilityByPosition(abilityID, fromUnit, targetPosition);

    public void CastAbilitySelfcast(int abilityID, Unit fromUnit) 
    { 
    }

    public void CastAbilityByTarget(int abilityID, Unit fromUnit, Unit targetUnit) 
    { 
    }

    public void CastAbilityByPosition(int abilityID, Unit fromUnit, Vector3 targetPosition) 
    { 
    }

    protected override void Awake()
    {
        base.Awake();

        InitializeAbilities();
    }

    private void InitializeAbilities()
    {
        abilities = new Ability[1];
    }
}