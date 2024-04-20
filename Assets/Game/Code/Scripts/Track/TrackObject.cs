using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jam
{
    public class TrackObject : MonoBehaviour
    {
        [SerializeField] private bool _canTurnBack = true;
        [SerializeField] private bool _resetPreferredOnReach = true;
        private Direction _preferredDirection;
        private Direction _movementDirection;
        private TrackWaypoint _targetWaypoint;
        private TrackWaypoint OriginWaypoint
            => this._targetWaypoint.GetWaypointInDirection(this._movementDirection.Opposite);
        private bool IsReady => this._targetWaypoint != null;
        private Rigidbody2D _rbody;
        public Vector2 Position
        {
            get => this._rbody.position;
            set => this._rbody.position = value;
        }

        private void Awake()
        {
            this._rbody = this.GetComponent<Rigidbody2D>();
        }

        public void Setup(TrackWaypoint waypoint, Direction movementDirection)
        {
            this._targetWaypoint = waypoint;
            this._movementDirection = movementDirection;
        }

        public void MoveForward(float distance)
        {
            if (!IsReady) return;
            float distanceToReach = Vector2.Distance(this.Position, this._targetWaypoint.Position);
            float initialMoveDistance = Mathf.Min(distance, distanceToReach);
            this.Position += this._movementDirection.Vector * initialMoveDistance;
            if (distance >= distanceToReach)
            {
                float remainingDistance = distance - distanceToReach;
                Vector2 reachedPosition = this._targetWaypoint.Position;
                TargetNextWaypoint();
                this.Position = reachedPosition + this._movementDirection.Vector * remainingDistance;
            }
        }

        public void SetPreferredDirection(Direction direction)
        {
            this._preferredDirection = direction;
        }

        private void TargetNextWaypoint()
        {
            Direction newDirection = ResolveNewDirection();
            TrackWaypoint newTarget = this._targetWaypoint.GetWaypointInDirection(newDirection);
            Setup(newTarget, newDirection);
            if (_resetPreferredOnReach)
            {
                this._preferredDirection = null;
            }
        }

        private Direction ResolveNewDirection()
        {
            HashSet<Direction> directionOptions = new(this._targetWaypoint.Connections.Keys);
            if (!this._canTurnBack) directionOptions.Remove(this._movementDirection.Opposite);
            if (directionOptions.Count == 1) return directionOptions.First();

            if (_preferredDirection != null && directionOptions.Contains(_preferredDirection))
            {
                // If the preferred direction a valid option, use it
                return _preferredDirection;
            }
            if (directionOptions.Contains(_movementDirection))
            {
                // Keep the current direction if it's still a valid option
                return _movementDirection;
            }
            if (directionOptions.Count >= 2)
            {
                // If there are multiple options, avoid turning back
                directionOptions.Remove(_movementDirection.Opposite);
            }
            return directionOptions.First();
        }
    }
}
