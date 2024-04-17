using SensenToolkit.Rand;
using UnityEngine;

namespace Sensen.Components
{
    [CreateAssetMenu(fileName = "AudioProfile", menuName = "Sensen/Audio/Profile", order = 1)]
    public class AudioProfile : ScriptableObject
    {
        [field: SerializeField]
        public AudioClip[] Clips { get; private set; }
        [field: SerializeField]
        public bool RandomizeClips { get; private set; } = false;
        [field: SerializeField]
        public AudioTrack Track { get; private set; }
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float Volume { get; private set; } = 1f;
        private RandomWithVariability _random;
        private RandomWithVariability VarRandom => _random ??= new(
            optionsAmount: Clips.Length,
            percentReductionOnSelect: 0.25f,
            noSequentialRepetition: true
        );

        private int _clipIndex = 0;

        [field: SerializeField]
        public AudioPlaybackProfileBase PlaybackProfile { get; private set; }

        public AudioPlaybackCommand GetCommand(AudioTrack track = null)
        {
            return new AudioPlaybackCommand(
                clip: ChooseClip(),
                volume: PlaybackProfile.Volume * Volume,
                loop: PlaybackProfile.Loop,
                pitch: PlaybackProfile.ChoosePitch(),
                track: track ?? Track ?? AudioTrack.Global
            );
        }

        private AudioClip ChooseClip()
        {
            if (Clips.Length == 0) return null;
            if (RandomizeClips) return Clips[VarRandom.Select()];
            AudioClip clip = Clips[_clipIndex];
            _clipIndex = (_clipIndex + 1) % Clips.Length;
            return clip;
        }
    }
}
