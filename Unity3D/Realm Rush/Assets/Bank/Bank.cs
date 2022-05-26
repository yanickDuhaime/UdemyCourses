using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

    [field: SerializeField]public int CurrentBalance { get; private set; }

    [SerializeField] private TextMeshProUGUI balanceText;
    

    

    void Awake()
    {
        CurrentBalance = startingBalance;
        UpdateBalanceText();
    }

    public void Deposit(int amount)
    {
        CurrentBalance += Mathf.Abs(amount);
        UpdateBalanceText();
    }

    public void Withdraw(int amount)
    {
        CurrentBalance -= Mathf.Abs(amount);
        if (CurrentBalance < 0)
        {
            ReloadScene();
        }
        UpdateBalanceText();
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void UpdateBalanceText()
    {
        balanceText.text = $"Gold:{CurrentBalance}";
    }
}
