using System;
using System.Collections;
using System.Collections.Generic;
using SensenToolkit;
using SensenToolkit.Gizmosx;
using Unity.VisualScripting;
using UnityEngine;

namespace Jam
{
    public class TrackWaypoint : MonoBehaviour
    {
        [SerializeField] private List<WaypointDirectionConfig> _connectionsList;
        private readonly Dictionary<Direction, TrackWaypoint> _connections;
        public Dictionary<Direction, TrackWaypoint> Connections
            => _connections ?? BuildConnectionsDictionary();
        public Vector2 Position => transform.position;

        public TrackWaypoint GetWaypointInDirection(Direction direction)
        {
            return Connections.TryGetValue(direction, out TrackWaypoint waypoint) ? waypoint : null;
        }

        public void EnsureBackReferenceConnections()
        {
            foreach (WaypointDirectionConfig config in _connectionsList)
            {
                TrackWaypoint connectedWaypoint = config.Waypoint;
                if (connectedWaypoint == null) continue;
                var direction = Direction.FromEnum(config.Direction);
                Direction oppositeDirection = direction.Opposite;
                connectedWaypoint.EnsureConnectionTo(oppositeDirection, this);
            }
        }

        private void OnDrawGizmos()
        {
            Vector3 pos = this.transform.position;
            foreach (WaypointDirectionConfig config in _connectionsList)
            {
                var direction = Direction.FromEnum(config.Direction);
                Color color = DirectionToColor(config.Direction);
                Vector3 offset = DirectionToOffset(config.Direction);
                using (Gizmosx.Color(color))
                {
                    Vector3 target = config.Waypoint.transform.position;
                    Gizmos.DrawLine(pos + offset, target + offset);
                }
            }
        }

        private void EnsureConnectionTo(Direction direction, TrackWaypoint waypoint)
        {
            Dictionary<Direction, TrackWaypoint> connections = Connections;
            connections[direction] = waypoint;
            _connectionsList = ConvertConnectionsToList(connections);
        }

        private List<WaypointDirectionConfig> ConvertConnectionsToList(Dictionary<Direction, TrackWaypoint> connections)
        {
            List<WaypointDirectionConfig> list = new();
            foreach (KeyValuePair<Direction, TrackWaypoint> connection in connections)
            {
                Direction direction = connection.Key;
                TrackWaypoint waypoint = connection.Value;
                list.Add(new WaypointDirectionConfig(direction.Enumeration, waypoint));
            }

            return list;
        }

        private static Color DirectionToColor(DirectionEnum direction)
        {
            return direction switch
            {
                DirectionEnum.Up => Color.red,
                DirectionEnum.Down => Color.yellow,
                DirectionEnum.Right => Color.blue,
                DirectionEnum.Left => Color.cyan,
                _ => throw new Exception($"Can't recognize direction {direction}"),
            };
        }

        private static Vector2 DirectionToOffset(DirectionEnum direction)
        {
            float offset = 0.05f;
            return direction switch
            {
                DirectionEnum.Up => Vector2.right * offset,
                DirectionEnum.Down => Vector2.left * offset,
                DirectionEnum.Right => Vector2.up * offset,
                DirectionEnum.Left => Vector2.down * offset,
                _ => throw new Exception($"Can't recognize direction {direction}"),
            };
        }

        private Dictionary<Direction, TrackWaypoint> BuildConnectionsDictionary()
        {
            Dictionary<Direction, TrackWaypoint> connections = new();
            foreach (WaypointDirectionConfig config in _connectionsList)
            {
                var direction = Direction.FromEnum(config.Direction);
                connections[direction] = config.Waypoint;
                if (config.Waypoint == null) continue;
            }
            return connections;
        }
    }
}
