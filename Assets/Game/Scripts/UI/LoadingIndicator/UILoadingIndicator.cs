using UnityEngine;
using DG.Tweening;

public class UILoadingIndicator : MonoBehaviour
{
    private const float ROTATION_DURATION = 1f;
    private const float FULL_CIRCLE = 360f;
    
    private RectTransform _rectTransform;
    private Tween _rotationTween;

    private void Awake()
    {
        InitializeRectTransform();
    }

    private void OnEnable()
    {
        StartRotation();
    }

    private void OnDisable()
    {
        StopRotation();
    }

    private void OnDestroy()
    {
        CleanupTween();
    }

    private void InitializeRectTransform()
    {
        _rectTransform = GetComponent<RectTransform>();
        if (_rectTransform != null)
        {
            _rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
    }

    private void StartRotation()
    {
        CleanupTween();
        
        _rotationTween = transform.DORotate(
                new Vector3(0, 0, FULL_CIRCLE),
                ROTATION_DURATION,
                RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .SetRelative(false);
    }

    private void StopRotation()
    {
        if (_rotationTween != null)
        {
            _rotationTween.Kill();
            transform.rotation = Quaternion.identity;
        }
    }

    private void CleanupTween()
    {
        _rotationTween?.Kill();
        _rotationTween = null;
    }
}