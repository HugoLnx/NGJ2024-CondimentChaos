using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

namespace MyGame
{
    // Test class to evaluate that IntellieSense and Analyzers are working
    public class BasicMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector3(horizontal, 0, vertical);
            var movement = direction * _speed * Time.deltaTime;
            transform.Translate(movement, Space.Self);
            var isPlayer = gameObject.tag == "Player";
            Debug.Log(isPlayer);
        }

        private void FixedUpdate()
        {
        }
    }
}
