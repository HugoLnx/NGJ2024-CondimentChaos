using System;
using UnityEngine;

namespace Sensen.Components
{
    public abstract class AudioPlaybackProfileBase : ScriptableObject
    {
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float Volume { get; protected set; } = 1f;

        [field: SerializeField]
        public bool Loop { get; protected set; } = false;

        public abstract float ChoosePitch();
    }
}
