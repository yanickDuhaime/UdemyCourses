using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [field:SerializeField] public int Cost { get; private set; }
    [SerializeField] private float buildTime = 0.5f;
    

    private void Start()
    {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null) return false;

        if (bank.CurrentBalance >= Cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(Cost);
            return true;
        }

        return false;
    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child.transform)
            {
                grandChild.gameObject.SetActive(false);
            }
        }
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildTime);
            foreach (Transform grandChild in child.transform)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }
}
