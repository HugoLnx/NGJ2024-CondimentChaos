using System;
using System.Collections;
using SensenToolkit.Coroutines;
using UnityEngine;

namespace Sensen.Components
{
    public class AudioOutput
    {
        public bool Mute { get; private set; }
        public float VolumeModifier { get; private set; } = 1f;
        public float Volume { get; private set; }
        public float Pitch { get => _source.pitch; set => _source.pitch = value; }
        public bool Loop { get => _source.loop; set => _source.loop = value; }
        public AudioTrack Track { get; private set; }
        public bool IsPlaying { get; private set; }
        private float TrackVolumeModifier => Track?.VolumeModifier ?? 1f;
        private bool TrackMute => Track?.Mute == true;

        public string SourceName => _source.name;

        private readonly AudioSource _source;
        private readonly MonoBehaviour _mono;

        public event Action<AudioOutput> OnFinishedPlaying;

        public AudioOutput(AudioSource source, MonoBehaviour mono)
        {
            _source = source;
            _source.Stop();
            _mono = mono;
            _mono.StartCoroutine(MonitorLoop());
        }

        public void Play(AudioPlaybackCommand command, Action onFinished = null)
        {
            IsPlaying = true;
            Volume = command.Volume;
            Loop = command.Loop;
            Pitch = command.Pitch;
            Play(command.Clip, onFinished);
        }

        public void Play(AudioClip clip, Action onFinished = null)
        {
            IsPlaying = true;
            RefreshSource();
            if (Loop)
            {
                _source.clip = clip;
                _source.Play();
            }
            else {
                _source.clip = null;
                _source.PlayOneShot(clip, Volume);
            }
            if (onFinished != null)
            {
                _mono.StartCoroutine(ScheduleOnFinished(onFinished, clip.length));
            }
        }

        public void Stop()
        {
            _source.Stop();
            IsPlaying = false;
        }

        public void UpdateVolume(float? volume = null, float? modifier = null)
        {
            VolumeModifier = modifier ?? VolumeModifier;
            Volume = volume ?? Volume;
            RefreshSource();
        }

        public void UpdateMute(bool isMute)
        {
            Mute = isMute;
            RefreshSource();
        }

        public void UpdateTrack(AudioTrack track)
        {
            Track?.Unregister(this);
            Track = track;
            Track?.Register(this);
            RefreshSource();
        }

        public void RemoveTrack() => UpdateTrack(track: null);

        internal void RefreshSource()
        {
            if (_source.loop)
            {
                _source.volume = Volume * VolumeModifier * TrackVolumeModifier;
            }
            else {
                _source.volume = VolumeModifier * TrackVolumeModifier;
            }
            _source.mute = Mute || TrackMute;
        }

        private IEnumerator MonitorLoop()
        {
            while (true)
            {
                yield return new WaitUntil(() => _source.isPlaying);
                yield return new WaitWhile(() => Time.timeScale == 0 || !Application.isFocused || !Application.isPlaying || _source.isPlaying);
                IsPlaying = false;
                OnFinishedPlaying?.Invoke(this);
            }
        }

        private IEnumerator ScheduleOnFinished(Action onFinished, float clipLength)
        {
            yield return new WaitForSeconds(clipLength);
            onFinished?.Invoke();
        }
    }
}
