using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SensenToolkit;
using UnityEngine;

namespace Jam
{
    public class AudioPlayer : ASingleton<AudioPlayer>
    {
        [SerializeField] private AudioSource _source;
        public void PlaySFX(params AudioClip[] clips)
        {
            _source.PlayOneShot(clips.Shuffle().First());
        }
    }
}
