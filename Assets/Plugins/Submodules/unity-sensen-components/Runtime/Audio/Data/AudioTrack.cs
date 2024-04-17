using System.Collections.Generic;
using UnityEngine;

namespace Sensen.Components
{
    [CreateAssetMenu(fileName = "AudioTrack", menuName = "Sensen/Audio/Track", order = 1)]
    public class AudioTrack : ScriptableObject
    {
        [field: SerializeField] public bool Mute { get; private set; } = false;
        [field: SerializeField] public float ConfiguredVolumeModifier { get; private set; } = 1f;
        public float TmpVolumeModifier { get; private set; } = 1f;
        public float VolumeModifier => ConfiguredVolumeModifier * TmpVolumeModifier;
        public virtual bool IsGlobal => false;
        public static AudioTrack Global => _globalTrack ??= CreateInstance<GlobalAudioTrack>();
        private static AudioTrack _globalTrack;
        private readonly HashSet<AudioOutput> _outputs = new();

        private void OnEnable()
        {
            TmpVolumeModifier = 1f;
        }

        internal void Register(AudioOutput output)
        {
            _outputs.Add(output);
        }

        internal void Unregister(AudioOutput output)
        {
            _outputs.Remove(output);
        }

        public void SetTmpVolumeModifier(float modifier)
        {
            TmpVolumeModifier = modifier;
            RefreshOutputs();
        }

        public void SetMute(bool mute)
        {
            Mute = mute;
            RefreshOutputs();
        }

        public void Stop()
        {
            foreach (AudioOutput output in _outputs)
            {
                output.Stop();
            }
            _outputs.Clear();
        }

        private void RefreshOutputs()
        {
            foreach (AudioOutput output in _outputs)
            {
                output.RefreshSource();
            }
        }
    }
}
