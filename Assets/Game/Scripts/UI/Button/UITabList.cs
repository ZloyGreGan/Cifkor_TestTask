using UnityEngine;
using System.Collections.Generic;

public class UITabList : MonoBehaviour
{
    [SerializeField] private List<UISelectedButton> _buttonList = new();
    private UISelectedButton _currentSelectedButton;

    private void Awake()
    {
        InitializeButtons();
        SelectInitialButton();
    }

    private void InitializeButtons()
    {
        _buttonList.Clear();
        
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out UISelectedButton button))
            {
                button.Initialize(this);
                _buttonList.Add(button);
            }
        }
    }

    private void SelectInitialButton()
    {
        if (_buttonList.Count > 0)
        {
            SelectButton(_buttonList[0]);
        }
    }

    public void SelectButton(UISelectedButton selectedButton)
    {
        if (selectedButton == null || _currentSelectedButton == selectedButton)
        {
            return;
        }

        DeselectCurrentButton();
        SelectNewButton(selectedButton);
    }

    private void DeselectCurrentButton()
    {
        if (_currentSelectedButton != null)
        {
            _currentSelectedButton.SetSelected(false, Vector2.zero);
        }
    }

    private void SelectNewButton(UISelectedButton selectedButton)
    {
        _currentSelectedButton = selectedButton;
        _currentSelectedButton.SetSelected(true, Vector2.zero);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_buttonList == null)
        {
            _buttonList = new List<UISelectedButton>();
        }
    }
#endif
}