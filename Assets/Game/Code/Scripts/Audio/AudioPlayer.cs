using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{
    public class AudioPlayer : ASingleton<AudioPlayer>
    {
        [SerializeField] private AudioSource _source;
        public void PlaySFX(AudioClip clip)
        {
            _source.PlayOneShot(clip);
        }
    }
}
