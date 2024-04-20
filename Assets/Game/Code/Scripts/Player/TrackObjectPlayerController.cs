using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam
{
    public class TrackObjectPlayerController : MonoBehaviour
    {
        [SerializeField] float _speed = 3f;
        private TrackObject _trackObject;

        private void Awake()
        {
            this._trackObject = this.GetComponent<TrackObject>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                this._trackObject.SetPreferredDirection(Direction.Up);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                this._trackObject.SetPreferredDirection(Direction.Down);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                this._trackObject.SetPreferredDirection(Direction.Right);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                this._trackObject.SetPreferredDirection(Direction.Left);
            }
            this._trackObject.MoveForward(this._speed * Time.deltaTime);
        }
    }
}
