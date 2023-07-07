using UnityEngine;

public class AbilitySystemComponent : MonoBehaviour
{
    [SerializeField] private Ability[] abilities; // four abilities: basic attack, dash, ability1, ability2


    // Attempts to activate the ability
    private void TryActivateAbility(int abilityID)
    {
        if (CanActivateAbility(abilityID))
        {
            ActivateAbility(abilityID);
        }
    }

    private void ActivateAbility(int abilityID)
    {
        // switch (abilities[abilityID].targetType)
        // returns target
        // yield wait until target is found

        CommitAbility(abilityID);

        // abilities[abilityID].Execute(source, target, other);
    }

    // const function to see if ability is activatable
    private bool CanActivateAbility(int abilityID)
    {
        // check cooldownHistory
        // check attributes (resources)

        return true;
    }

    // Commits reources/cooldowns etc. ActivateAbility() must call this!
    private void CommitAbility(int abilityID)
    {
        // add timestamp to cooldownHistory
        // subtract resources from attributes
    }

    // Interrupts the ability (from an outside source).
    private void CancelAbility(int abilityID)
    { }

    //  The ability has ended. This is intended to be called by the ability to end itself.
    private void EndAbility(int abilityID)
    { }
}