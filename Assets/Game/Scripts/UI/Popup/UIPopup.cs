using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPopup : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _popupTitle;
    [SerializeField] private TextMeshProUGUI _popupDescription;
    [SerializeField] private Button _popupCloseButton;

    private RectTransform _rectTransform;

    private void Awake()
    {
        InitializeComponents();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    public void ShowPopup(string title, string description)
    {
        UpdateContent(title, description);
        RebuildLayout();
        SetActive(true);
    }

    private void HidePopup()
    {
        SetActive(false);
    }

    private void InitializeComponents()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }

    private void SubscribeToEvents()
    {
        if (_popupCloseButton != null)
        {
            _popupCloseButton.onClick.AddListener(HidePopup);
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (_popupCloseButton != null)
        {
            _popupCloseButton.onClick.RemoveListener(HidePopup);
        }
    }

    private void UpdateContent(string title, string description)
    {
        if (_popupTitle != null) _popupTitle.text = title;
        if (_popupDescription != null) _popupDescription.text = description;
    }

    private void RebuildLayout()
    {
        if (_rectTransform != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        }
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}