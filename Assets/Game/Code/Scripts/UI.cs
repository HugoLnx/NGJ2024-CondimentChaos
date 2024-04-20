using System.Collections;
using System.Collections.Generic;
using Jam;
using TMPro;
using UnityEngine;

public class UI : ASingleton<UI>
{
    const string SCORETEXTBASE = "Score: ";
    private int _currentScore = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        RefreshText();
    }
    public void IncreaseScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        RefreshText();
    }

    private void RefreshText()
    {
        _scoreText.text = SCORETEXTBASE + _currentScore;
    }
}
