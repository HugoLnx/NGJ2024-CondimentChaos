using System;
using System.Collections.Generic;
using UnityEngine;

namespace SensenToolkit.Animations
{
    public class AnimatorPlayback

    {
        private readonly Animator _animator;
        private readonly Func<AnimatorStateInfo, bool> _identifyState;
        private readonly int _layer;
        private List<AnimatorClipInfo> _clipsBuffer = new();

        public AnimatorPlayback(Animator animator, Func<AnimatorStateInfo, bool> identifyState, int layer = 0)
        {
            _identifyState = identifyState;
            _layer = layer;
            _animator = animator;
        }

        public static AnimatorPlayback BuildForHash(Animator animator, int hash, int layer = 0)
        {
            return new AnimatorPlayback(
                animator: animator,
                identifyState: state => state.fullPathHash == hash || state.shortNameHash == hash,
                layer: layer
            );
        }

        public AnimatorPlaybackState GetCurrentState()
        {
            AnimatorStateInfo? currentState = IfValidState(_animator.GetCurrentAnimatorStateInfo(_layer));
            AnimatorStateInfo? nextState = IfValidState(_animator.GetNextAnimatorStateInfo(_layer));

            if (nextState.HasValue)
            {
                return new AnimatorPlaybackState(
                    state: nextState.Value,
                    clip: GetNextFirstClip(_layer),
                    isTransitioningIn: true
                );
            }
            else if (currentState.HasValue)
            {
                return new AnimatorPlaybackState(
                    state: currentState.Value,
                    clip: GetCurrentFirstClip(_layer),
                    isTransitioningOut: _animator.IsInTransition(_layer)
                );
            }
            else
            {
                return AnimatorPlaybackState.None;
            }
        }

        private AnimationClip GetCurrentFirstClip(int layer)
        {
            _clipsBuffer.Clear();
            _animator.GetCurrentAnimatorClipInfo(_layer, _clipsBuffer);
            if (_clipsBuffer.Count == 0)
            {
                return null;
            }
            return _clipsBuffer[0].clip;
        }

        private AnimationClip GetNextFirstClip(int layer)
        {
            _clipsBuffer.Clear();
            _animator.GetNextAnimatorClipInfo(_layer, _clipsBuffer);
            if (_clipsBuffer.Count == 0)
            {
                return null;
            }
            return _clipsBuffer[0].clip;
        }

        private AnimatorStateInfo? IfValidState(AnimatorStateInfo state)
        {
            bool isBlankState = state.fullPathHash == 0;
            if (!isBlankState && _identifyState.Invoke(state))
            {
                return state;
            }
            return null;
        }
    }
}
