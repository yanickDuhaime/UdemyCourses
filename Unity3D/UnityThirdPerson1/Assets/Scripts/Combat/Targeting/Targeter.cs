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
        currentTarget = targets[0];
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
