using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EasyButtons;
using Mono.Cecil.Cil;
using Unity.Collections;
using UnityEngine;

namespace Jam
{
    public class FoodProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _lifetime = 20f;
        [SerializeField] private FoodSO _food;
        [SerializeField] private FlavorSO _flavor;
        [SerializeField] private Vector2 _direction = new Vector2(0.5f, 0.5f).normalized;
        [SerializeField] private float _rotateSpeed = 180f;
        [SerializeField] private SpriteRenderer _srenderer;
        [SerializeField] private SpriteRenderer _flavorRenderer;
        [SerializeField] private bool _autoSetup;
        [SerializeField] private AudioClip[] _bounceSfx;
        private Rigidbody2D _rbody;
        private RaycastHit2D[] _rayhits = new RaycastHit2D[1];

        public FoodSO Food => _food;
        public FlavorSO Flavor => _flavor;

        public bool Launched { get; private set; }

        private void Awake()
        {
            _srenderer = GetComponentInChildren<SpriteRenderer>();
            _rbody = GetComponent<Rigidbody2D>();
            _srenderer.enabled = false;
            _flavorRenderer.enabled = false;
            _rbody.simulated = false;
        }

        private void Start()
        {
            if (_autoSetup)
            {
                Setup(FoodSORepository.Repo.GetRandom(), _direction, _speed);
                Flavorize(FlavorSORepository.Repo.GetRandom());
            }
        }

        [Button]
        public void Setup(FoodSO food, Vector2 direction, float speed)
        {
            SetupView(food);
            Launch(direction, speed);
        }

        public void SetupView(FoodSO food)
        {
            this._food = food;
            _srenderer.sprite = food.Texture;
            _srenderer.enabled = true;
        }

        public void Launch(Vector2 direction, float speed, Vector2? position = null)
        {
            this.transform.SetParent(null);
            Destroy(gameObject, _lifetime);
            this._direction = direction;
            this._speed = speed;
            if (position.HasValue) _rbody.position = position.Value;
            _rbody.velocity = _direction * _speed;
            Launched = true;
            _rbody.simulated = true;
        }

        public void Flavorize(FlavorSO flavor)
        {
            for (int i = 0; i < _food.FlavorTextures.Count; i++)
            {
                if (_food.FlavorTextures[i].Flavor == flavor)
                {
                    _flavorRenderer.sprite = _food.FlavorTextures[i].Sprite;
                    break;
                }
            }
            _flavorRenderer.enabled = true;
        }

        private void Update()
        {
            if (!Launched) return;
            transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!Launched) return;
            bool hasHitFountain = other.collider.CompareTag(Fountain.Tag);
            if (!hasHitFountain) return;

            Fountain fountain = other.collider.GetComponent<Fountain>();
            Vector2 normal = other.GetContact(0).normal;
            Vector2 currentDirection = _rbody.velocity.normalized;
            var newDirection = Vector2.Reflect(currentDirection, normal);
            this._rbody.velocity = newDirection * _speed;

            AudioPlayer.Instance.PlaySFX(_bounceSfx);

            Flavorize(fountain.Flavor);
        }
    }
}
