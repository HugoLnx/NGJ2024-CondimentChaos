using System;
using UnityEngine;

namespace Jam
{
    [Serializable]
    public struct SimpleRange
    {
        [field: SerializeField] public float Begin { get; private set; }
        [field: SerializeField] public float End { get; private set; }

        public SimpleRange(float begin, float end)
        {
            Begin = begin;
            End = end;
        }

        public float Lerp(float difficulty)
        {
            return Mathf.Lerp(Begin, End, difficulty);
        }
    }
}
