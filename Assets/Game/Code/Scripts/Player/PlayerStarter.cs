using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class PlayerStarter : MonoBehaviour
    {
        [SerializeField] private TrackObject _player;
        [SerializeField] private TrackWaypoint _startWaypoint;
        [SerializeField] private DirectionEnum _startDirection;
        private FullTrack _track;

        private void Awake()
        {
            _track = this.GetComponent<FullTrack>();
            _track.OnTrackReady += OnTrackReady;
        }

        private void OnTrackReady()
        {
            _track.Add(_player, _startWaypoint, Direction.FromEnum(_startDirection));
        }
    }
}
