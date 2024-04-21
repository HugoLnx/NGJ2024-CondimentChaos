using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class Direction
    {
        public static Direction Up = new(Vector2.up, DirectionEnum.Up);
        public static Direction Down = new(Vector2.down, DirectionEnum.Down);
        public static Direction Right = new(Vector2.right, DirectionEnum.Right);
        public static Direction Left = new(Vector2.left, DirectionEnum.Left);
        public static readonly HashSet<Direction> All = new() {
            Up, Down, Right, Left
        };
        private static readonly Dictionary<DirectionEnum, Direction> s_fromEnumDict;
        private static Dictionary<DirectionEnum, Direction> FromEnumDict =>
            s_fromEnumDict ?? BuildFromEnumDictionary();

        private readonly Vector2 _vector;
        private readonly DirectionEnum _enumeration;

        private Direction _opposite;
        public Direction Opposite => _opposite ??= FromVector(this.Vector * -1f);

        private readonly DirectionEnum _oppositeEnum;

        public Vector2 Vector => _vector;
        public DirectionEnum Enumeration => _enumeration;
        public bool IsVertical => Vector.y != 0f;
        public bool IsHorizontal => Vector.x != 0f;

        private Direction(Vector2 vector, DirectionEnum enumeration)
        {
            this._vector = vector;
            this._enumeration = enumeration;
        }

        public static Direction FromEnum(DirectionEnum enumeration)
        {
            return FromEnumDict[enumeration];
        }

        public static Direction FromVector(Vector2 vector)
        {
            if (vector == Vector2.up) return Direction.Up;
            if (vector == Vector2.down) return Direction.Down;
            if (vector == Vector2.right) return Direction.Right;
            if (vector == Vector2.left) return Direction.Left;
            throw new System.Exception($"Can't recognize direction {vector}");
        }

        private static Dictionary<DirectionEnum, Direction> BuildFromEnumDictionary()
        {
            Dictionary<DirectionEnum, Direction> fromEnumDict = new();
            foreach (Direction direction in All)
            {
                fromEnumDict[direction.Enumeration] = direction;
            }
            return fromEnumDict;
        }
    }
}
