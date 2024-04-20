using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
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
        private Rigidbody2D _rbody;
        private RaycastHit2D[] _rayhits = new RaycastHit2D[1];

        public FoodSO Food => _food;
        public FlavorSO Flavor => _flavor;

        private void Awake()
        {
            _srenderer = GetComponentInChildren<SpriteRenderer>();
            _rbody = GetComponent<Rigidbody2D>();
            _srenderer.enabled = false;
            _flavorRenderer.enabled = false;
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
            Destroy(gameObject, _lifetime);
            this._food = food;
            this._direction = direction;
            this._speed = speed;
            _rbody.velocity = _direction * _speed;
            _srenderer.sprite = food.Texture;
            _flavorRenderer.sprite = food.FlavorTexture;
            _srenderer.enabled = true;
        }

        public void Flavorize(FlavorSO flavor)
        {
            _flavorRenderer.color = flavor.Color;
            _flavorRenderer.enabled = true;
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            bool hasHitFountain = other.collider.CompareTag(Fountain.Tag);
            if (!hasHitFountain) return;

            Fountain fountain = other.collider.GetComponent<Fountain>();
            Vector2 normal = other.GetContact(0).normal;
            Vector2 currentDirection = _rbody.velocity.normalized;
            var newDirection = Vector2.Reflect(currentDirection, normal);
            this._rbody.velocity = newDirection * _speed;

            Flavorize(fountain.Flavor);
        }
    }
}
