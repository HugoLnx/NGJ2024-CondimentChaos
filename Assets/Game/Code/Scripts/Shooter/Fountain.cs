using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Jam
{
    public class Fountain : MonoBehaviour
    {
        public const string Tag = "Fountain";
        [SerializeField] private FlavorSO _flavor;
        private SpriteRenderer _srenderer;

        public FlavorSO Flavor => _flavor;

        private void Awake()
        {
            _srenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            Setup(_flavor);
        }

        public void Setup(FlavorSO flavor)
        {
            _flavor = flavor;
            _srenderer.color = flavor.Color;
        }
    }
}
