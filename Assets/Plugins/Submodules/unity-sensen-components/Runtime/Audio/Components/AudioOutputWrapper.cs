using LnxArch;
using UnityEngine;

namespace Sensen.Components
{
    public class AudioOutputWrapper : MonoBehaviour
    {
        public AudioOutput Output { get; private set; }
        private AudioSource _source;
        [LnxInit]
        private void Init([FromLocal] AudioSource source)
        {
            _source = source;
            Output = new AudioOutput(_source, this);
        }
    }
}
