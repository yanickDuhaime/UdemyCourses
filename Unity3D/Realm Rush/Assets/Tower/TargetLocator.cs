using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] float attackRange = 15f;
    

    Transform closestTarget = null;

    void Update()
    {
        if (closestTarget == null)
        {
            FindClosestTarget();
        }
        else
        {
            bool targetInRange = Vector3.Distance(transform.position, closestTarget.position) < attackRange;
            if (!targetInRange) 
                closestTarget = null;
            AimWeapon(targetInRange);
        }
        
        
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        
    }

    void AimWeapon(bool targetInRange)
    {
        if(targetInRange)
            weapon.LookAt(closestTarget.position);

        Attack(targetInRange);
       
        
    }

    void Attack(bool isActive)
    {
        var emissionModule = particleSystem.emission;
        emissionModule.enabled = isActive;
    }
}
