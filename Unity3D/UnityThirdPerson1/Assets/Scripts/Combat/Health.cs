using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(int damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage,0);

        Debug.Log(currentHealth);
        
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
