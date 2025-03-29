using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DogBreedsView : MonoBehaviour
{
    private const int MAX_VISIBLE_BREEDS = 10;

    [SerializeField] private GameObject _loadingIndicator;
    [SerializeField] private UIPopup _popup;
    [SerializeField] private Button[] _breedButtons;

    [Inject] private DogBreedsModel _dogBreedsModel;

    private DogBreed[] _breeds;
    private GameObject[] _breedLoadings;

    private void Awake()
    {
        InitializeLoadingsArray();
    }

    private void Start()
    {
        SetupButtonListeners();
    }

    private void OnDestroy()
    {
        CleanupButtonListeners();
    }

    public void DisplayDogBreeds(DogBreed[] breeds)
    {
        _breeds = breeds;
        UpdateBreedButtons();
    }

    public void ShowLoading(bool show)
    {
        _loadingIndicator.SetActive(show);
    }

    public void ShowBreedPopup(string name, string description)
    {
        _popup.ShowPopup(name, description);
        DisableAllLoadingIndicators();
    }

    private void InitializeLoadingsArray()
    {
        _breedLoadings = new GameObject[_breedButtons.Length];
    }

    private void SetupButtonListeners()
    {
        for (int i = 0; i < _breedButtons.Length; i++)
        {
            int index = i;
            _breedButtons[i].onClick.AddListener(() => OnBreedClicked(index));
            _breedLoadings[i] = _breedButtons[i].transform.Find("Loading").gameObject;
            _breedLoadings[i]?.SetActive(false);
        }
    }

    private void CleanupButtonListeners()
    {
        for (int i = 0; i < _breedButtons.Length; i++)
        {
            _breedButtons[i].onClick.RemoveAllListeners();
        }
    }

    private void UpdateBreedButtons()
    {
        int displayCount = Mathf.Min(MAX_VISIBLE_BREEDS, _breeds.Length);

        for (int i = 0; i < displayCount; i++)
        {
            ConfigureButton(i);
        }

        for (int i = displayCount; i < _breedButtons.Length; i++)
        {
            _breedButtons[i].gameObject.SetActive(false);
        }
    }

    private void ConfigureButton(int index)
    {
        _breedButtons[index].gameObject.SetActive(true);
        SetLoadingIndicator(index, false);

        TextMeshProUGUI[] textComponents = _breedButtons[index].GetComponentsInChildren<TextMeshProUGUI>();
        textComponents[0].text = index.ToString();
        textComponents[1].text = _breeds[index].attributes.name;
    }

    private void OnBreedClicked(int index)
    {
        string breedId = _breeds[index].id;
        _dogBreedsModel.RequestBreedDetails(breedId);
        SetLoadingIndicator(index, true);
    }

    private void SetLoadingIndicator(int index, bool active)
    {
        _breedLoadings[index]?.SetActive(active);
    }

    private void DisableAllLoadingIndicators()
    {
        for (int i = 0; i < _breedLoadings.Length; i++)
        {
            SetLoadingIndicator(i, false);
        }
    }

    public void OnTabDeactivated()
    {
        if (_popup != null)
        {
            _popup.SetActive(false);
        }
    }
}