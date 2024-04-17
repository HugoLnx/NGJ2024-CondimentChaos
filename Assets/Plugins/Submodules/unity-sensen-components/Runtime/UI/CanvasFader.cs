using System;
using LnxArch;
using UnityEngine;
using DG.Tweening;

namespace Sensen.Components
{
    public class CanvasFader : MonoBehaviour
    {
        public const float MinFadeDuration = .05f;
        public const float FastFadeDuration = .2f;
        public const float NormalFastFadeDuration = .35f;
        public const float NormalFadeDuration = .5f;
        public const float SlowFadeDuration = 1f;
        private CanvasGroup _canvasGroup;
        private RectTransform _rect;
        private Vector2 _originalPosition;
        private Tween _tween;
        public bool IsFading => _tween != null && _tween.IsActive();

        public bool IsHidden => _canvasGroup.alpha == 0f;
        public bool IsVisible => _canvasGroup.alpha >= 0.1f;
        public bool IsFullyVisible => _canvasGroup.alpha == 1f;

        [LnxInit]
        private void Init([FromLocal] CanvasGroup canvasGroup)
        {
            _canvasGroup = canvasGroup;
            _rect = transform as RectTransform;
            _originalPosition = _rect.anchoredPosition;
        }

        public void FadeIn(FadeSpeed speed, bool resetAlpha = true, Action onComplete = null)
        {
            if (resetAlpha) _canvasGroup.alpha = 0f;
            TweenAlphaTo(1f, speed, onComplete);
        }

        public void FadeOut(FadeSpeed speed, Action onComplete = null)
        {
            TweenAlphaTo(0f, speed, onComplete);
        }

        public void ImmediateAppear()
        {
            _canvasGroup.alpha = 1f;
        }

        public void ImmediateDisappear()
        {
            _canvasGroup.alpha = 0f;
        }

        private void TweenAlphaTo(float targetAlpha, FadeSpeed speed, Action onComplete = null)
        {
            KillTween();
            if (speed == FadeSpeed.Immediate)
            {
                _canvasGroup.alpha = targetAlpha;
                onComplete?.Invoke();
                return;
            }
            float duration = GetDurationFor(_canvasGroup.alpha - targetAlpha, speed);
            _tween = DOTween.To(
                getter: () => _canvasGroup.alpha,
                setter: x => _canvasGroup.alpha = x,
                endValue: targetAlpha,
                duration: duration
            )
            .SetUpdate(isIndependentUpdate: true)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => onComplete?.Invoke());
            return;
        }

        private float GetDurationFor(float alphaDistance, FadeSpeed speed)
        {
            float fullDuration = speed switch
            {
                FadeSpeed.Fast => FastFadeDuration,
                FadeSpeed.NormalFast => NormalFastFadeDuration,
                FadeSpeed.Normal => NormalFadeDuration,
                FadeSpeed.Slow => SlowFadeDuration,
                _ => throw new ArgumentOutOfRangeException(nameof(speed), speed, null)
            };
            return Mathf.Lerp(MinFadeDuration, fullDuration, Mathf.Abs(alphaDistance));
        }

        private void KillTween()
        {
            if (_tween == null) return;
            _tween.Kill();
            _tween = null;
        }
    }
}
