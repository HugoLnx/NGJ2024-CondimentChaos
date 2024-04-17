using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SensenToolkit.Sensors
{
    public abstract class SensorBase : MonoBehaviour
    {
        #region Properties
        [SerializeField] private string[] _onlyTags;
        private Transform _oneSensed;
        public readonly HashSet<Transform> Sensed = new();
        public bool IsSensing => Sensed.Count > 0;
        public Transform OneSensed => IsSensing ? GetOneSensed() : null;
        #endregion

        #region Lifecycle
        protected void OnEnter(Collider other)
        {
            if (IsValid(other))
            {
                Sensed.Add(other.transform);
            }
        }

        protected void OnExit(Collider other)
        {
            if (IsValid(other))
            {
                Sensed.Remove(other.transform);
                if (!Sensed.Contains(_oneSensed))
                {
                    _oneSensed = null;
                }
            }
        }

        private void OnDisable()
        {
            Sensed.Clear();
            _oneSensed = null;
        }
        #endregion

        #region Private
        private bool IsValid(Collider other)
        {
            if (_onlyTags.Length == 0)
            {
                return true;
            }

            foreach (string tag in _onlyTags)
            {
                if (other.CompareTag(tag))
                {
                    return true;
                }
            }

            return false;
        }

        private Transform GetOneSensed()
        {
            if (_oneSensed == null)
            {
                _oneSensed = Sensed.FirstOrDefault();
            }
            return _oneSensed;
        }
        #endregion
    }
}
