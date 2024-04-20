using System;
using System.Collections;
using System.Collections.Generic;
using SensenToolkit;
using UnityEngine;

namespace Jam
{
    public class Shooter : MonoBehaviour
    {
        public void Shoot()
        {
            Debug.Log("Player Shoots!");
        }

        public void TurnTo(Vector3 targetPosition)
        {
            transform.up = (targetPosition - transform.position).XY().normalized;
        }
    }
}
