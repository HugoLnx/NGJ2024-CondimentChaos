using System;
using System.Collections;
using System.Collections.Generic;
using Jam;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : ASingleton<UI>
{
    const string SCORETEXTBASE = "Score: ";
    private int _currentScore = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _gameoverScoreText;
    [SerializeField] private GameObject _blackscreen;
    [SerializeField] private TextMeshProUGUI _timerText;
    private GameTime _gameTime;

    private void Start()
    {
        _blackscreen.SetActive(false);
        RefreshText();
        _gameTime = GameTime.Instance;
        GameTime.Instance.OnEnded += () => _blackscreen.SetActive(true);
    }

    private void Update()
    {
        SetTimeText(_gameTime.RemainingTime);
    }

    public void IncreaseScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        RefreshText();
    }

    private void RefreshText()
    {
        _scoreText.text = SCORETEXTBASE + _currentScore;
        _gameoverScoreText.text = _scoreText.text;
    }

    private void SetTimeText(float currentTime)
    {
        int timeInt = (int)currentTime;
        _timerText.text = $"Time: {timeInt}";
    }

    public void ReloadScene()
    {
        // _blackscreen.SetActive(false);
        // _currentScore = 0;
        // RefreshText();
        // SetTimeText(GameTime.Instance.TotalTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
