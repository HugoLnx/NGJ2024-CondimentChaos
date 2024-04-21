using System;
using System.Collections;
using System.Collections.Generic;
using SensenToolkit;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UIElements;

namespace Jam
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private FoodProjectile _projectilePrefab;
        [SerializeField] private float _shotCooldown = 0.5f;
        [SerializeField] private Transform _aimObj;
        [SerializeField] private Transform _projectileContainer;
        [SerializeField] private AudioClip[] _shootSfx;
        private FoodProjectile _preparedProjectile;

        private Vector2 Forward
        {
            get => _aimObj.transform.up;
            set => _aimObj.up = value;
        }

        private void Awake()
        {
            _projectileContainer.GetComponent<SpriteRenderer>().enabled = false;
        }

        private void Start()
        {
            StartCoroutine(PrepareLoop());
        }
        public void Shoot()
        {
            if (_preparedProjectile == null) return;

            AudioPlayer.Instance.PlaySFX(_shootSfx);
            _preparedProjectile.Launch(
                position: _aimObj.position,
                direction: this.Forward,
                speed: _speed
            );
            _preparedProjectile = null;
        }

        public void TurnTo(Vector3 targetPosition)
        {
            this.Forward = (targetPosition - transform.position).XY().normalized;
        }

        private IEnumerator PrepareLoop()
        {
            while (true)
            {
                PrepareNext();
                yield return new WaitUntil(() => _preparedProjectile == null);
                yield return new WaitForSeconds(_shotCooldown);
            }
        }

        private void PrepareNext()
        {
            FoodSO food = FoodSORepository.Repo.GetRandom();
            FoodProjectile projectile = Instantiate(
                original: _projectilePrefab,
                position: _projectileContainer.position,
                rotation: Quaternion.identity,
                parent: _projectileContainer
            );
            projectile.SetupView(food);
            _preparedProjectile = projectile;
        }
    }
}
