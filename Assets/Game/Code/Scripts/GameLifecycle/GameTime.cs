using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class GameTime : ASingleton<GameTime>
    {
        [SerializeField] private int _totalTime = 60;
        private float _currentTime = 0f;
        private bool _hasStarted;

        public float CurrentTime => _currentTime;
        public event Action OnEnded;

        private void Start()
        {
            DificultyCurve.Instance.GameDuration = _totalTime;
        }

        public void StartTimer()
        {
            this._hasStarted = true;
        }

        private void Update()
        {
            if (!_hasStarted) return;
            this._currentTime += Time.deltaTime;
            if (this._currentTime >= _totalTime)
            {
                OnEnded?.Invoke();
                this._hasStarted = false;
            }
        }
    }
}
