//using AbilitySystem;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;
//using UnityEngine;

//enum AbilityState
//{
//    NotActive,
//    Waiting,
//    Execute,
//   // Cancel
//}

//// is targeting the correct name when it also applies the ability?
//public class Targeting_NormalCast : MonoBehaviour, ITargeting
//{
//    private List<int> activeAbilityIDs = new List<int>();

//    public void AbilityReleased(int abilityID)
//    {
//        activeAbilityIDs.Remove(abilityID);
//    }

//    public async Task<bool> ActivateAbility(Unit source, int abilityID, TargetType targetType, float range, Action<Unit, Unit> abilityExecuteFunction)
//    {
//        if (activeAbilityIDs.Any())
//        {
//            // check if is LMB to execute or RMB to cancel.
//        }

//        switch (targetType)
//        {
//            case TargetType.Self:
//                abilityExecuteFunction(source, source);
//                return true;

//            case TargetType.Target:
//                // activate target targeting
//                return await Target(source, abilityID, abilityExecuteFunction);

//            case TargetType.Projectile:

//                break;

//            case TargetType.Cone:

//                break;

//            default:
//                Debug.LogWarning("Target type not found");
//                break;
//        }

//        return false;
//    }

//    //public bool Self(Unit source, Action<Unit, Unit> abilityExecuteFunction)
//    //{
//    //    abilityExecuteFunction(source, source);
//    //    return true;
//    //}

//    public async Task<bool> Target(Unit source, int abilityID, Action<Unit, Unit> abilityExecuteFunction)
//    {
//        // create mouse targeting cursor that
//        // turns red when hovering over enemy and
//        // turns green when hovering over ally


//        activeAbilityIDs.Add(abilityID);

//        Debug.Log("targetingType target started");

//        while (true)
//        {
//            switch (abilityStates)
//            {
//                case AbilityState.Waiting:
//                    await Task.Yield();
//                    continue;
//                case AbilityState.Execute:
//                    // target = mouse find target
//                    // if found target
//                    //   abilityFunction(target)
//                    //   return true
//                    // else
//                    Debug.Log("execute ability");
//                    abilityStates = AbilityState.NotActive;
//                    return false;
//                case AbilityState.Cancel:
//                    Debug.Log("cancel ability");
//                    abilityStates = AbilityState.NotActive;
//                    return false;
//                default:
//                    Debug.Log("something went wrong");
//                    abilityStates = AbilityState.NotActive;
//                    //something  went wrong
//                    return false;
//            }

//        }
//    }

//    public Unit Projectile(GameObject projectilePrefab, float speed, float range)
//    {
//        // instantiate projectile prefab
//        // wait for it to find a target or reach end of range

//        return null;
//    }

//    public Unit Cone(Unit source, float range, float width, float delay)
//    {
//        // wait for delay
//        // create cone shape from source (or a point on source) and check for intersection
//        // activate ability effects on targets
//        // yeah we're using delegate instead of returning target I think
//        return null;
//    }

//    // === self AOE ====
//    // or just instantiate an aoe prefab, but then the prefab itself would have to apply effects
//    // activate a circle collider or make a circle cast from source with radius = range
//    // if applyonstart, check immediately for targets and applyeffect
//    // every period tick, find targets and apply effect
//    // stop when duration is reached
//}