using System;
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
    [SerializeField] private TextMeshProUGUI _timerText;
    private GameTime _gameTime;

    private void Start()
    {
        RefreshText();
        _gameTime = GameTime.Instance;
    }

    private void Update()
    {
        SetTimeText(_gameTime.CurrentTime);
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

    private void SetTimeText(float currentTime)
    {
        int timeInt = (int)currentTime;
        _timerText.text = $"Time: {timeInt}";
    }
}
