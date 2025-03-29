using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISelectedButton : MonoBehaviour
{
    private const float MOVE_DURATION = 0.1f;
    private const float SCALE_DURATION = 0.3f;
    private const float SCALE_OFF_DURATION = 0.1f;

    [SerializeField] private RectTransform _selectableLine;
    
    private UITabList _tabList;
    private Button _button;
    private Tween _scaleTween;
    private Tween _moveTween;

    private void Awake()
    {
        CacheComponents();
    }

    private void OnDestroy()
    {
        Cleanup();
    }

    public void Initialize(UITabList tabList)
    {
        _tabList = tabList;
        _selectableLine.gameObject.SetActive(false);
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void CacheComponents()
    {
        _button = GetComponent<Button>();
    }

    private void OnButtonClicked()
    {
        _tabList.SelectButton(this);
    }

    public void SetSelected(bool selected, Vector2 targetPos)
    {
        KillTweens();

        if (selected)
        {
            ShowSelection(targetPos);
        }
        else
        {
            HideSelection();
        }
    }

    private void ShowSelection(Vector2 targetPos)
    {
        _selectableLine.gameObject.SetActive(true);
        _selectableLine.localScale = new Vector3(0, 1, 1);
        
        _moveTween = _selectableLine.DOAnchorPos(targetPos, MOVE_DURATION)
            .SetEase(Ease.InOutQuad);
            
        _scaleTween = _selectableLine.DOScaleX(1f, SCALE_DURATION)
            .SetEase(Ease.InOutQuad);
    }

    private void HideSelection()
    {
        _scaleTween = _selectableLine.DOScaleX(0f, SCALE_OFF_DURATION)
            .SetEase(Ease.InOutQuad)
            .OnComplete(DeactivateLine);
    }

    private void DeactivateLine()
    {
        _selectableLine.gameObject.SetActive(false);
    }

    private void KillTweens()
    {
        _scaleTween?.Kill();
        _moveTween?.Kill();
    }

    private void Cleanup()
    {
        KillTweens();
        if (_button != null)
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}