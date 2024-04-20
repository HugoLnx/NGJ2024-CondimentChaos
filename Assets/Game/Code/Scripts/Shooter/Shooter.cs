using System;
using System.Collections;
using System.Collections.Generic;
using SensenToolkit;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

namespace Jam
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private FoodProjectile _projectilePrefab;
        private Vector2 Forward
        {
            get => transform.up;
            set => transform.up = value;
        }
        public void Shoot()
        {
            FoodSO food = FoodSORepository.Repo.GetRandom();
            FoodProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.Setup(
                food: food,
                direction: this.Forward,
                speed: _speed
            );
        }

        public void TurnTo(Vector3 targetPosition)
        {
            this.Forward = (targetPosition - transform.position).XY().normalized;
        }
    }
}
