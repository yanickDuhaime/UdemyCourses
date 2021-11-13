using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int droppedGold = 25;

    [SerializeField] int goldPenalty = 25;

    Bank bank;
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null) return;
        bank.Deposit(droppedGold);
    }
    
    public void StealGold()
    {
        if (bank == null) return;
        bank.Withdraw(goldPenalty);
    }


}
