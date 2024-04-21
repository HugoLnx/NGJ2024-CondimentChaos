using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    // Test class to evaluate that IntellieSense and Analyzers are working
    public class BasicMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var direction = new Vector3(horizontal, 0, vertical);
            var movement = direction * speed * Time.deltaTime;
            transform.Translate(movement, Space.Self);
            var isPlayer = gameObject.tag == "Player";
            Debug.Log(isPlayer);
        }

        private void FixedUpdate()
        {
        }
    }
}
