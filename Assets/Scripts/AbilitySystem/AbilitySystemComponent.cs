using UnityEngine;

public class AbilitySystemComponent : MonoBehaviour
{
    [SerializeField] public Ability[] abilities = new Ability[4]; // four abilities: basic attack, dash, ability1, ability2

    // Attempts to activate the ability
    public void TryActivateAbility(int abilityID, Unit source)
    {
        if (CanActivateAbility(abilityID))
        {
            ActivateAbility(abilityID, source);
        }
    }

    // const function to see if ability is activatable
    public bool CanActivateAbility(int abilityID)
    {
        // check cooldownHistory
        // check attributes (resources)

        return true;
    }

    // Interrupts the ability (from an outside source).
    public void CancelAbility(int abilityID)
    { }

    private void ActivateAbility(int abilityID, Unit source)
    {
        // switch (abilities[abilityID].targetType)
        // returns target
        // yield wait until target is found

        CommitAbility(abilityID);

        abilities[abilityID].Execute(source);
        //abilities[abilityID].Execute(source);
    }

    // Commits reources/cooldowns etc. ActivateAbility() must call this!
    private void CommitAbility(int abilityID)
    {
        // add timestamp to cooldownHistory
        // subtract resources from attributes
    }

    //  The ability has ended. This is intended to be called by the ability to end itself.
    private void EndAbility(int abilityID)
    { }
}