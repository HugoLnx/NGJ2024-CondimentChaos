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
        private bool _canShoot = true;
        [SerializeField] private float _shotCooldown = 0.5f;
        [SerializeField] private AudioClip[] _shootSfx;

        private Vector2 Forward
        {
            get => transform.up;
            set => transform.up = value;
        }
        public void Shoot()
        {
            if (!_canShoot) return;

            AudioPlayer.Instance.PlaySFX(_shootSfx);
            FoodSO food = FoodSORepository.Repo.GetRandom();
            FoodProjectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            projectile.Setup(
                food: food,
                direction: this.Forward,
                speed: _speed
            );
            _canShoot = false;
            StartCoroutine(ShotCooldown());
        }

        private IEnumerator ShotCooldown()
        {
            yield return new WaitForSeconds(_shotCooldown);
            _canShoot = true;
        }

        public void TurnTo(Vector3 targetPosition)
        {
            this.Forward = (targetPosition - transform.position).XY().normalized;
        }
    }
}
