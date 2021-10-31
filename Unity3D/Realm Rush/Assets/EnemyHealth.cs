using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;

    [SerializeField] int currentHitPoints = 0;
    
    void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        takeDamage(1);
    }

    public void takeDamage(int damage)
    {
        currentHitPoints -= damage;
        if(currentHitPoints <= 0) Destroy(gameObject);
    }
}
