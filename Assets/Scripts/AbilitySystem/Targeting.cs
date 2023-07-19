using AbilitySystem;
using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;

public class Targeting : MonoBehaviour
{
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private GameObject directionIndicator;
    [SerializeField] private GameObject coneIndicator;

    private TargetType curActiveTargetType = TargetType.Invalid;

    //public <T> GetTargetFromActiveTargeting<T>(float range) {}
    //    return (T)GetTargetFromTargetingType(curActiveTargetType, range);

    //=>
    //GetTargetFromTargetingType(curActiveTargetType, range);

    public T GetTargetFromActiveTargeting<T>(float range) =>
        GetTargetFromTargetingType<T>(curActiveTargetType, range);

    public T GetTargetFromTargetingType<T>(TargetType targetType, float range)
    {
        switch (targetType)
        {
            case TargetType.Target:
                if (GetTarget_Target(range) is T target)
                    return target;
                return default(T);
            default:
                return default(T);
        }
    }

    public void ActivateIndicatorsFromTargetingType(TargetType targetType, float range, float width)
    {
        switch (targetType)
        {
            case TargetType.Target:
                ActivateIndicators_Target(range);
                break;
            case TargetType.Direction:
                ActivateIndicators_Direction(range, width);
                break;
            case TargetType.Cone:
                ActivateIndicators_Cone(range, width);
                break;
            case TargetType.SelfAOE:
                ActivateIndicators_SelfAOE(range);
                break;
        }

        return;
    }

    public void HideTargetingIndicators()
    {
        rangeIndicator.SetActive(false);
        directionIndicator.SetActive(false);
        coneIndicator.SetActive(false);
    }

    #region ActivateIndicators

    public void ActivateIndicators_Target(float range) // TODO: Material vfxMaterial
    {
        curActiveTargetType = TargetType.Target;
        rangeIndicator.transform.localScale = new Vector3(range, range, 1);
        rangeIndicator.SetActive(true);
    }

    public void ActivateIndicators_Direction(float range, float width) // TODO: Material vfxMaterial
    {
        curActiveTargetType = TargetType.Direction;
        directionIndicator.transform.localScale = new Vector3(width, range, 1);
        directionIndicator.SetActive(true);
    }

    public void ActivateIndicators_Cone(float range, float width) // TODO: Material vfxMaterial
    {
        curActiveTargetType = TargetType.Cone;
        coneIndicator.transform.localScale = new Vector3(width, range, 1);
        coneIndicator.SetActive(true);
    }

    public void ActivateIndicators_SelfAOE(float range) // TODO: Material vfxMaterial
    {
        curActiveTargetType = TargetType.SelfAOE;
        rangeIndicator.transform.localScale = new Vector3(range, range, 1);
        rangeIndicator.SetActive(true);
    }

    #endregion

    #region GetTarget

    public Unit GetTarget_Target (float range)
    {
        RaycastHit2D rayHit;
        Unit target = null;

        if (rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition)))
        {
            if (rayHit.transform.TryGetComponent<Unit>(out target))
            {
                float dist = Vector3.Distance(target.transform.position, transform.position);
                if (dist > range)
                {
                    return null;
                }
            }
        }

        return target;
    }

    public Vector3 GetTarget_Direction ()
    {
        return transform.right;
    }

    public Unit GetTarget_Cone ()
    {
        // TODO: use cone hitbox / shape-casting
        return null;
    }

    public Unit GetTarget_SelfAOE ()
    {
        // TODO: use circle hitbox / shape-casting
        return null;
    }

    //PositionAOE (range, rangeOfAOE)

    #endregion

    private void Update()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}