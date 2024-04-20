using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Jam
{

    [Serializable]
    public struct WaypointDirectionConfig
    {
        [SerializeField] private DirectionEnum _direction;
        [SerializeField] private TrackWaypoint _waypoint;
        public readonly DirectionEnum Direction => _direction;
        public readonly TrackWaypoint Waypoint => _waypoint;

        public WaypointDirectionConfig(DirectionEnum direction, TrackWaypoint waypoint)
        {
            this._direction = direction;
            this._waypoint = waypoint;
        }
    }
}
