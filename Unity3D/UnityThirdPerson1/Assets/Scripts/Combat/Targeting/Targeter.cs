using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    private List<Target> targets = new List<Target>();
    public Target currentTarget { get; private set; }
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) return;
        target.OnDestroyed += RemoveTarget;
        targets.Add(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) return;
       RemoveTarget(target);
    }


    public bool SelectTarget()
    {
        if (targets.Count <= 0) return false;

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (var target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);
            if (viewPos.x is <= 0 or >= 1 || viewPos.y is <= 0 or >= 1) continue;
                                                    //Position of the center
            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if (toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null) return false;
        currentTarget = closestTarget;
        cinemachineTargetGroup.AddMember(currentTarget.transform,1,2f);
        return true;
    }

    public void Cancel()
    {
        if (currentTarget == null) return;
        cinemachineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        targets.Remove(target);
        target.OnDestroyed -= RemoveTarget;

        if (currentTarget == target)
        {
            Cancel();
        }

    }
}
