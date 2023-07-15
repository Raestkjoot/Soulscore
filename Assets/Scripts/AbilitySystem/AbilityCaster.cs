using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AbilitySystem
{
    [RequireComponent(typeof(Targeting))]
    [RequireComponent(typeof(Unit))]
    public class AbilityCaster : MonoBehaviour
    {
        // four abilities: (EAbilityType) 0:basicAttack, 1:dash, 2:ability1, 3:ability2
        [SerializeField] private Ability[] abilities = new Ability[4];

        private AbilityType curActiveAbility = AbilityType.None;

        private Targeting _targeting;
        private Unit _sourceUnit;
        private ILogr _logger;

        // Attempts to activate the ability
        public void TryActivateAbility(int abilityID)
        {
            if (abilityID == (int)AbilityType.Interact)
            {
               CancelCurAbility(abilityID);
            }
            else if (CanActivateAbility(abilityID))
            {
                if (abilities[abilityID].isImmediate)
                    ExecuteAbilityImmediate(abilityID);
                else
                    ActivateAbility(abilityID);
            }
        }

        public void ReleaseAbility(int abilityID)
        {
            // Execute ability
            if (abilityID == (int)curActiveAbility)
            {
                ExecuteAbility(abilityID);
            }
            curActiveAbility = AbilityType.None;
        }

        // const function to see if ability is activatable
        public bool CanActivateAbility(int abilityID)
        {
            if (abilities[abilityID] == null)
            {
                _logger.Warn($"No ability at ability ID {abilityID}");
                return false;
            }

            // check cooldownHistory
            // check attributes (resources)

            return true;
        }

        public void TryInteruptAbility(Unit source)
        {
            throw new NotImplementedException();
        }

        private void ActivateAbility(int abilityID)
        {
            switch (abilities[abilityID].GetTargetType())
            {
                case TargetType.Self:
                    ExecuteAbility(abilityID);
                    break;
                case TargetType.Target:
                    // activate target targeting
                   
                    break;
                default:
                    // not yet implemented / something went wrong
                    break;
            }
        }

        // Commits reources/cooldowns etc. ActivateAbility() must call this!
        private void CommitAbility(int abilityID)
        {
            // add timestamp to cooldownHistory
            // subtract resources from attributes (might just let this be an effect)
        }

        private void ExecuteAbility(int abilityID)
        {
            // check target, if target is null for a target ability -> end ability
            // if target is null for aoe type ability -> execute anyway
            CommitAbility(abilityID);
            abilities[abilityID].Execute(_sourceUnit, _sourceUnit);
        }

        private void ExecuteAbilityImmediate(int abilityID)
        {
            // find target
            // call ExecuteAbility with the target
            throw new NotImplementedException();
        }

        private void CancelCurAbility(int abilityID)
        {
            curActiveAbility = AbilityType.None;
            // TODO: turn off targeting
        }

        private void Awake()
        {
            _logger = new ConsoleLogger();
            _targeting = gameObject.GetComponent<Targeting>();
            _sourceUnit = gameObject.GetComponent<Unit>();
        }
    } 
}