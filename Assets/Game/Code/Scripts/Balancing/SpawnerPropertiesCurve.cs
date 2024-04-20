using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using SensenToolkit.DataStructures;
using SensenToolkit.Lerp;
using Unity.VisualScripting;
using UnityEngine;

namespace Jam
{
    public struct DifficultyProperties
    {
        public float Cooldown;
        public float ChanceCustomerA;
        public float ChanceCustomerB;
        public float ChanceCustomerC;
    }

    public class SpawnerPropertiesCurve : ASingleton<SpawnerPropertiesCurve>
    {
        [SerializeField] private SimpleRange _cooldownRange;
        [SerializeField] private float[] _customerAChances;
        [SerializeField] private float[] _customerBChances;
        [SerializeField] private float[] _customerCChances;
        private LayeredLerp<float> _lerperA;
        private LayeredLerp<float> _lerperB;
        private LayeredLerp<float> _lerperC;

        private new void Awake()
        {
            base.Awake();
            _lerperA = BuildCustomerLerper(_customerAChances[0], _customerAChances[1], _customerAChances[2], _customerAChances[3]);
            _lerperB = BuildCustomerLerper(_customerBChances[0], _customerBChances[1], _customerBChances[2], _customerBChances[3]);
            _lerperC = BuildCustomerLerper(_customerCChances[0], _customerCChances[1], _customerCChances[2], _customerCChances[3]);
        }

        private void Start()
        {
            for (float i = 0f; i < 60f; i += 2.5f)
            {
                LogProperties(i);
            }
        }

        [Button]
        private void LogProperties(float seconds)
        {
            DifficultyProperties properties = GetProperties(seconds);
            // Debug.Log($"Cooldown: {properties.Cooldown}");
            // Debug.Log($"Chance A: {properties.ChanceCustomerA}");
            // Debug.Log($"Chance B: {properties.ChanceCustomerB}");
            // Debug.Log($"Chance C: {properties.ChanceCustomerC}");
        }

        public DifficultyProperties GetProperties(float seconds)
        {
            DificultyCurve curve = DificultyCurve.Instance;
            float difficulty = curve.DificultyOnTime(seconds);
            Debug.Log($"Difficulty: {seconds} -> {difficulty}");

            return new DifficultyProperties
            {
                Cooldown = _cooldownRange.Lerp(difficulty),
                ChanceCustomerA = _lerperA.Lerp(difficulty),
                ChanceCustomerB = _lerperB.Lerp(difficulty),
                ChanceCustomerC = _lerperC.Lerp(difficulty)
            };
        }

        private LayeredLerp<float> BuildCustomerLerper(float v0, float v1, float v2, float v3)
        {
            LayeredLerp<float> lerper = new();
            lerper.SetLerper(new FloatLerper());
            lerper.SetInitialWeightTargetReference(v0);
            lerper.AddLayer(v0, t: 0);
            lerper.AddLayer(v0, t: 0.25f);
            lerper.AddLayer(v1, t: 0.5f);
            lerper.AddLayer(v2, t: 0.75f);
            lerper.AddLayer(v3, t: 1.0f);
            return lerper;
        }
    }
}
