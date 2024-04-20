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
    }
}
