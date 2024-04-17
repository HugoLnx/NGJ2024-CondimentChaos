using System;
using System.Collections;
using System.Collections.Generic;
using LnxArch;
using UnityEngine;

namespace SensenToolkit.Animations
{
    public class AnimatorPlayer : MonoBehaviour
    {
        #region Properties
        private Animator _animator;
        #endregion

        #region Constructors
        [LnxInit]
        private void Init(Animator animator)
        {
            _animator = animator;
        }
        #endregion

        #region Public
        public AnimatorPlayback Play(int stateHash, int layer = 0, float normalizedStartAt = 0f, float normalizedTransitionDuration = 0f)
        {
            if (normalizedTransitionDuration == 0f)
            {
                _animator.Play(stateHash, layer, normalizedStartAt);
            }
            else
            {
                _animator.CrossFade(
                    stateHash,
                    normalizedTransitionDuration,
                    layer,
                    normalizedStartAt
                );
            }
            return AfterPlay(stateHash, layer);
        }
        public AnimatorPlayback PlayInFixedTime(int stateHash, int layer = 0, float fixedStartAt = 0f, float fixedTransitionDuration = 0f)
        {
            if (fixedTransitionDuration == 0f)
            {
                _animator.PlayInFixedTime(stateHash, layer, fixedStartAt);
            }
            else
            {
                _animator.CrossFadeInFixedTime(
                    stateHash,
                    fixedTransitionDuration,
                    layer,
                    fixedStartAt
                );
            }
            return AfterPlay(stateHash, layer);
        }

        public AnimatorPlayback Play(string stateName, int layer = 0, float normalizedStartAt = 0f, float normalizedTransitionDuration = 0f)
            => Play(Animator.StringToHash(stateName), layer, normalizedStartAt, normalizedTransitionDuration);

        public AnimatorPlayback PlayInFixedTime(string stateName, int layer = 0, float fixedStartAt = 0f, float fixedTransitionDuration = 0f)
            => PlayInFixedTime(Animator.StringToHash(stateName), layer, fixedStartAt, fixedTransitionDuration);
        #endregion

        #region Private
        private AnimatorPlayback AfterPlay(int stateHash, int layer)
        {
            EnsureAnimatorStateWasUpdated();
            return AnimatorPlayback.BuildForHash(_animator, stateHash, layer);
        }

        private void EnsureAnimatorStateWasUpdated()
        {
            // Ensure GetNextAnimatorStateInfo and GetCurrentAnimatorStateInfo are updated
            _animator.Update(0f);
        }
        #endregion
    }
}
