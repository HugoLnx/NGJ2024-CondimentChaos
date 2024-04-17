using UnityEngine;
using System;
using LnxArch;

namespace SensenToolkit.Observables
{
    [LnxAutoAdd(Target = AutoAddTarget.Local)]
    public class UpdateObservable : MonoBehaviour, IParameterlessObservable
    {
        public event Action Callbacks;

        private void Update()
        {
            Callbacks?.Invoke();
        }
    }
}
