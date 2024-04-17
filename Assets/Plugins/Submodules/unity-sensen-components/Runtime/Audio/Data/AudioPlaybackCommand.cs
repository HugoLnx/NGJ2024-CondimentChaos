using UnityEngine;

namespace Sensen.Components
{
    public struct AudioPlaybackCommand
    {
        public AudioClip Clip { get; }
        public float Volume { get; }
        public bool Loop { get; }
        public float Pitch { get; }
        public AudioTrack Track { get; }
        public bool UseGlobalTrack => Track.IsGlobal;

        public AudioPlaybackCommand(AudioClip clip, float volume, bool loop, float pitch, AudioTrack track)
        {
            Clip = clip;
            Volume = volume;
            Loop = loop;
            Pitch = pitch;
            Track = track;
        }

        public AudioPlaybackCommand WithDefaultTrack(AudioTrack track)
        {
            if (this.Track != null) return this;
            return new AudioPlaybackCommand(Clip, Volume, Loop, Pitch, track);
        }
    }
}
