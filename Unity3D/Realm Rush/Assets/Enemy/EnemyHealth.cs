using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    [SerializeField] int currentHitPoints = 0;
    
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)          
    {
        TakeDamage(1);
    }

    public void TakeDamage(int damage)
    {
        currentHitPoints -= damage;
        if(currentHitPoints <= 0) gameObject.SetActive(false);
    }
}
