using UnityEngine;
using UnityEngine.UI;
using System;

public class NavigationView : MonoBehaviour
{
    [SerializeField] private Button _weatherTabButton;
    [SerializeField] private Button _dogBreedsTabButton;

    public event Action<int> OnTabSwitched;

    private void Awake()
    {
        _weatherTabButton.onClick.AddListener(() => OnTabSwitched?.Invoke(0));
        _dogBreedsTabButton.onClick.AddListener(() => OnTabSwitched?.Invoke(1));
    }
}