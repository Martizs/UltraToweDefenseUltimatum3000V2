using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField]
    int startingBalance = 150;

    [SerializeField]
    int currentBalance;
    public int CurrentBalance
    {
        get { return currentBalance; }
    }

    ScoreBoard scoreBoard;

    private void Awake()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        currentBalance = startingBalance;
        scoreBoard.SetGoldText(currentBalance);
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        scoreBoard.SetGoldText(currentBalance);
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        if (currentBalance < 0)
        {
            SceneManager.LoadScene(0);
        }
        scoreBoard.SetGoldText(currentBalance);
    }
}
