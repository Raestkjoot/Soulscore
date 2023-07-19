using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AbilitySystem
{
    // TODO: use input manager so we don't hardcode the hardware.
    [RequireComponent(typeof(Unit))]
    public class AbilityCaster : MonoBehaviour
    {
        // four abilities: (EAbilityType) 0:basicAttack, 1:dash, 2:ability1, 3:ability2
        [SerializeField] private Ability[] _abilities = new Ability[4];

        private AbilityType _curActiveAbility = AbilityType.None;
        private Targeting _targeting;
        private Unit _sourceUnit;
        private ILogr _logger;

        // Attempts to activate the ability
        public void TryActivateAbility(int abilityID)
        {
            if (abilityID == (int)AbilityType.Interact ||
                _curActiveAbility != AbilityType.None)
            {
               CancelCurAbility(abilityID);
            }
            
            if (abilityID != (int)AbilityType.Interact &&
                CanActivateAbility(abilityID))
            {
                if (_abilities[abilityID].IsImmediate())
                    ExecuteAbilityImmediate(abilityID);
                else
                    ActivateAbility(abilityID);
            }
        }

        public void ReleaseAbility(int abilityID)
        {
            // Execute ability
            if (abilityID == (int)_curActiveAbility)
            {
                ExecuteAbility(abilityID);
                _curActiveAbility = AbilityType.None;
            }
        }

        public bool CanActivateAbility(int abilityID)
        {
            if (_abilities[abilityID] == null)
            {
                _logger.Warn($"No ability at ability ID {abilityID}");
                return false;
            }

            // TODO: check cooldownHistory
            // TODO: check attributes (resources)

            return true;
        }

        public void TryInteruptAbility(Unit source)
        {
            throw new NotImplementedException();
        }

        private void ActivateAbility(int abilityID)
        {
            _curActiveAbility = (AbilityType)abilityID;

            if (_abilities[abilityID] is Ability_Target abilityTarget)
            {
                _targeting.ActivateIndicators_Target(abilityTarget.GetRange());
            }

            // TODO: activate targeting for abilities where relevant.
        }

        // Commits reources/cooldowns etc. ActivateAbility() must call this!
        private void CommitAbility(int abilityID)
        {
            // TODO: add timestamp to cooldownHistory
            // TODO: subtract resources from attributes (might just let this be an effect)
        }

        private void ExecuteAbility(int abilityID)
        {
            // TODO:
            // - check target, if target is null for a target ability -> end ability
            // - if target is null for aoe type ability -> execute anyway
            CommitAbility(abilityID);

            if (_abilities[abilityID] is Ability_SelfTarget abilitySelfTarget)
            {
                abilitySelfTarget.Execute(_sourceUnit);
            }
            else if (_abilities[abilityID] is Ability_Target abilityTarget)
            {
                _targeting.HideTargetingIndicators();
                Unit target = _targeting.GetTargetFromActiveTargeting<Unit>(abilityTarget.GetRange());
                if (target != null)
                    abilityTarget.Execute(_sourceUnit, target);
            }

            // TODO: implement execute call for each type of ability
            // TODO: turn off targeting | _targeting.CancelActiveTargeting();
        }

        private void ExecuteAbilityImmediate(int abilityID)
        {
            // TODO: 
            // - find target
            // - call ExecuteAbility with the target
            ExecuteAbility(abilityID);
        }

        private void CancelCurAbility(int abilityID)
        {
            _curActiveAbility = AbilityType.None;
            _targeting.HideTargetingIndicators();
        }

        private void Awake()
        {
            _logger = new ConsoleLogger();
            _targeting = gameObject.GetComponentInChildren<Targeting>();
            _sourceUnit = gameObject.GetComponent<Unit>();
        }
    } 
}