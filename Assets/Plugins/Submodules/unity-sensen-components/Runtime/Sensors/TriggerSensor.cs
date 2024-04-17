using UnityEngine;

namespace SensenToolkit.Sensors
{
    public class TriggerSensor : SensorBase
    {
        private void OnTriggerEnter(Collider other)
        {
            OnEnter(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit(other);
        }
    }
}
