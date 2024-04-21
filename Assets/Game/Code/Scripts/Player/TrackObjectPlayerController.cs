using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam
{
    public class TrackObjectPlayerController : MonoBehaviour
    {
        private readonly int ParamHashIsVertical = Animator.StringToHash("isVertical");
        [SerializeField] float _speed = 3f;
        [SerializeField] private AudioClip[] _movementSfx;
        private TrackObject _trackObject;
        private Animator _animator;

        private void Awake()
        {
            this._trackObject = this.GetComponent<TrackObject>();
            this._trackObject.OnChangedDirection += OnChangedDirection;
            _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            GameTime.Instance.OnEnded += () => SetSpeed(0);
        }

        private void OnChangedDirection()
        {
            AudioPlayer.Instance.PlaySFX(_movementSfx);
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
            _animator.SetBool(ParamHashIsVertical, _trackObject.Direction?.IsVertical == true);
            this._trackObject.MoveForward(this._speed * Time.deltaTime);
        }

        public void SetSpeed(int speed)
        {
            _speed = speed;
        }
    }
}
