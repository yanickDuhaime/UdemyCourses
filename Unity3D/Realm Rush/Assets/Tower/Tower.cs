using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [field:SerializeField] public int Cost { get; private set; }

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
}
