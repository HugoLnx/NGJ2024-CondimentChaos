using System;
using System.Collections;
using System.Collections.Generic;
using SensenToolkit;
using UnityEngine;

namespace Jam
{
    public class ShootingPlayerController : MonoBehaviour
    {
        private Camera _camera;
        private Camera MainCamera => _camera = _camera != null ? _camera : Camera.main;

        private void Update()
        {
            UpdatesPlayerRotation();
        }

        private void UpdatesPlayerRotation()
        {
            Vector3 mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.up = (mousePosition - transform.position).XY().normalized;
        }
    }
}
