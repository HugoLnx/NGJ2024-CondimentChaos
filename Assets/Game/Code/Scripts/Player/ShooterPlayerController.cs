using System;
using System.Collections;
using System.Collections.Generic;
using SensenToolkit;
using UnityEngine;

namespace Jam
{
    public class ShooterPlayerController : MonoBehaviour
    {
        private Camera _camera;
        private Shooter _player;

        private Camera MainCamera => _camera = _camera != null ? _camera : Camera.main;

        private void Awake()
        {
            this._player = GetComponent<Shooter>();
        }

        private void Update()
        {
            UpdatesPlayerRotation();
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _player.Shoot();
            }
        }

        private void UpdatesPlayerRotation()
        {
            Vector3 mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            _player.TurnTo(mousePosition);
        }
    }
}
