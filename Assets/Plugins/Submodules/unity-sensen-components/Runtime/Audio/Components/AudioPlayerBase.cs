using System;
using LnxArch;
using UnityEngine;

namespace Sensen.Components
{
    public abstract class AudioPlayerBase : MonoBehaviour
    {
        [SerializeField] private float _globalVolume = 1f;
        [SerializeField] private bool _isMuted;
        private AudioOutputPool _outputPool;

        [LnxInit]
        protected void BaseInit(
            [FromLocalChild] AudioOutputPool outputPool = null
        )
        {
            _outputPool = outputPool;
        }

        public void Play(AudioProfile profile, AudioTrack track = null, Action onFinished = null)
        {
            Play(profile.GetCommand(track: track), onFinished);
        }

        public void Play(AudioPlaybackCommand command, Action onFinished = null)
        {
            AudioOutput output = command.Loop || command.UseGlobalTrack ? _outputPool?.Get() : _outputPool?.GetReusable(command.Pitch);
            if (output == null)
            {
                Debug.LogWarning($"[{nameof(AudioPlayerBase)}] No audio output available. Abort playing {command.Clip.name}");
                return;
            }
            output.UpdateVolume(modifier: _globalVolume);
            output.UpdateTrack(command.Track);
            output.Play(command, onFinished);
        }
        public void SetIsAudible(bool isAudible)
        {
            _isMuted = !isAudible;
            UpdateAllAudioOutputs();
        }

        public void SetGlobalVolume(float volume)
        {
            _globalVolume = volume;
            UpdateAllAudioOutputs();
        }

        private void UpdateAllAudioOutputs()
        {
            foreach (AudioOutput output in _outputPool.Creations)
            {
                output.UpdateVolume(modifier: _globalVolume);
                output.UpdateMute(isMute: _isMuted);
            }
        }
    }
}
