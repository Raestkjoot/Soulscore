using UnityEngine;

public class AbilitySystem : Singleton<AbilitySystem>
{
    private Ability[] abilities;

    // Overloads for casting abilities
    public void Cast(int abilityID, Unit sourceUnit) => 
        CastAbilitySelfcast(abilityID, sourceUnit);
    public void Cast(int abilityID, Unit sourceUnit, Unit targetUnit) => 
        CastAbilityByTarget(abilityID, sourceUnit, targetUnit);
    public void Cast(int abilityID, Unit sourceUnit, Vector3 targetPosition) =>
        CastAbilityByPosition(abilityID, sourceUnit, targetPosition);

    public void CastAbilitySelfcast(int abilityID, Unit sourceUnit) 
    { 
    }

    public void CastAbilityByTarget(int abilityID, Unit sourceUnit, Unit targetUnit) 
    { 
    }

    public void CastAbilityByPosition(int abilityID, Unit sourceUnit, Vector3 targetPosition) 
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