using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class DificultyCurve : ASingleton<DificultyCurve>
    {
        [SerializeField] private float _cyclesCount = 6;
        [SerializeField] private float _gameDuration = 60;
        [SerializeField] private float _waveAmplitude = 0.2f;

        public float GameDuration
        {
            set => _gameDuration = value;
        }

        public float DificultyOnTime(float seconds)
        {
            float cycleTime = _gameDuration / _cyclesCount;
            float cycleRawWave = CalculateCycleWave(seconds, cycleTime);
            float cycleWave = cycleRawWave * _waveAmplitude;
            float linearGrowth = seconds * (1f - (_waveAmplitude / 2f)) / _gameDuration;
            return cycleWave + linearGrowth;
        }

        // Output from 0 to 1
        private float CosWithNormalizedOutput(float x)
        {
            return (Mathf.Cos(x) + 1f) / 2f;
        }

        // Also input 0.5 maps to 1 and 1 maps back to 0
        private float CalculateCycleWave(float x, float waveLength)
        {
            return CosWithNormalizedOutput(Mathf.PI + 2f * Mathf.PI * x / waveLength);
        }
    }
}
