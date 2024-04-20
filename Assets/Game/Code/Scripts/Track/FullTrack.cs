using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

namespace Jam
{
    public class FullTrack : MonoBehaviour
    {
        [SerializeField] private Transform _objectsContainer;

        public bool IsReady { get; private set; }

        public event Action OnTrackReady;
        private void Start()
        {
            CompleteTrack();
            this.IsReady = true;
            OnTrackReady?.Invoke();
        }

        public void Add(
            TrackObject obj,
            TrackWaypoint originWaypoint,
            Direction movementDirection,
            float startOffsetRate = 0f
        )
        {
            if (!IsReady) throw new Exception("Track is not ready yet");
            TrackWaypoint targetWaypoint = originWaypoint.GetWaypointInDirection(movementDirection);
            if (targetWaypoint == null)
            {
                throw new Exception($"No waypoint in that direction. Origin: {originWaypoint}, Direction: {movementDirection}");
            }
            obj.transform.SetParent(_objectsContainer);
            Vector2 positionBetweenWaypoints
                = originWaypoint.Position + (targetWaypoint.Position - originWaypoint.Position) * startOffsetRate;
            obj.Position = positionBetweenWaypoints;
            obj.Setup(targetWaypoint, movementDirection);
        }

        [Button]
        private void CompleteTrack()
        {
            TrackWaypoint[] waypoints = GetComponentsInChildren<TrackWaypoint>();

            foreach (TrackWaypoint waypoint in waypoints)
            {
                waypoint.EnsureBackReferenceConnections();
            }
        }
    }
}
