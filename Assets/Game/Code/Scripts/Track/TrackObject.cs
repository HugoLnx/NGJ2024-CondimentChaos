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
        [SerializeField] private bool _canTurnBackInMiddleOfThePath = true;
        public event Action OnChangedDirection;
        private Direction _preferredDirection;
        private Direction _movementDirection;
        private TrackWaypoint _targetWaypoint;
        private TrackWaypoint OriginWaypoint
            => this._targetWaypoint.GetWaypointInDirection(this._movementDirection.Opposite);
        private bool IsReady => this._targetWaypoint != null;
        private Rigidbody2D _rbody;
        public Vector2 Position
        {
            get => this.transform.position;
            set => this.transform.position = value;
        }

        public void Setup(TrackWaypoint waypoint, Direction movementDirection)
        {
            bool hasChangedDirection = this._movementDirection != movementDirection;
            this._targetWaypoint = waypoint;
            this._movementDirection = movementDirection;
            if (hasChangedDirection)
            {
                OnChangedDirection?.Invoke();
            }
        }

        public void MoveForward(float distance)
        {
            if (!IsReady) return;
            Vector2 directionVector = this._targetWaypoint.Position - this.Position;
            float distanceToReach = directionVector.magnitude;
            Vector2 direction = directionVector.normalized;
            float initialMoveDistance = Mathf.Min(distance, distanceToReach);
            this.Position += direction * initialMoveDistance;
            if (distance >= distanceToReach)
            {
                float remainingDistance = distance - distanceToReach;
                Vector2 reachedPosition = this._targetWaypoint.Position;
                TargetNextWaypoint();
                direction = this._targetWaypoint.Position - this.Position;
                this.Position = reachedPosition + direction * remainingDistance;
            }
        }

        public void SetPreferredDirection(Direction direction)
        {
            bool isTryingToGoBack = direction == this._movementDirection.Opposite;
            this._preferredDirection = direction;
            if (isTryingToGoBack && _canTurnBack && _canTurnBackInMiddleOfThePath)
            {
                TargetNextWaypoint();
            }
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
