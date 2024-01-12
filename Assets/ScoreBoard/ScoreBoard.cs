using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    TMP_Text goldText;

    private void Awake()
    {
        goldText = GetComponent<TMP_Text>();
    }

    public void SetGoldText(int currentBalance)
    {
        if (goldText == null)
        {
            goldText = GetComponent<TMP_Text>();
        }
        goldText.text = $"Gold: {currentBalance}";
    }
}
