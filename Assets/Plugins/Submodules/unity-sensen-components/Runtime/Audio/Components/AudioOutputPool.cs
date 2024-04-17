using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace Sensen.Components
{
    public class AudioOutputPool : PrefabPoolBase<AudioOutput, AudioSource>
    {
        [SerializeField] private bool _debug;
        private const float PitchPrecision = 1e2f;
        private readonly Dictionary<int, AudioOutput> _pitchedOutputs = new();
        private readonly Dictionary<int, int> _pitchedOutputsCount = new();

        public AudioOutput GetReusable(float pitch)
        {
            int pitchKey = Mathf.RoundToInt(pitch * PitchPrecision);
            _pitchedOutputs.TryGetValue(pitchKey, out AudioOutput output);
            if (output != null)
            {
                _pitchedOutputsCount[pitchKey]++;
            }
            else {
                output = Get();
                _pitchedOutputs.Add(pitchKey, output);
                _pitchedOutputsCount.Add(pitchKey, 1);
            }
            return output;
        }

        public override void Release(AudioOutput releasingOutput)
        {
            if (_debug)
            {
                Debug.Log($"[{nameof(AudioOutputPool)}] Releasing Output {releasingOutput.SourceName}");
            }
            int pitchKey = Mathf.RoundToInt(releasingOutput.Pitch * PitchPrecision);
            _pitchedOutputs.TryGetValue(pitchKey, out AudioOutput output);
            if (output == null || output != releasingOutput)
            {
                base.Release(releasingOutput);
                return;
            }

            _pitchedOutputsCount[pitchKey]--;
            if (_pitchedOutputsCount[pitchKey] == 0)
            {
                _pitchedOutputs.Remove(pitchKey);
                _pitchedOutputsCount.Remove(pitchKey);
                base.Release(releasingOutput);
            }
        }

        protected override AudioOutput InstantiateNew()
        {
            AudioSource source = Instantiate(_prefab, this.transform);
            source.name = $"[{Creations.Count+1}] {_prefab.name}";
            AudioOutput output = new(source, this);
            output.OnFinishedPlaying += Release;
            return output;
        }
    }
}
