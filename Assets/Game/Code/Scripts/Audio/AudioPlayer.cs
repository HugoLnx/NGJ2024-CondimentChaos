using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SensenToolkit;
using UnityEngine;

namespace Jam
{
    public class AudioPlayer : ASingleton<AudioPlayer>
    {
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource _backgroundSource;
        public void PlaySFX(params AudioClip[] clips)
        {
            if (clips.Length <= 0)
            {
                Debug.LogWarning("SFX list length is 0");
                return;
            }
            _sfxSource.PlayOneShot(clips.Shuffle().First());
        }
    }
}
