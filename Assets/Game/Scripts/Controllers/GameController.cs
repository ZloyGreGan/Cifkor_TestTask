using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject] private WeatherModel _weatherModel;
    [Inject] private DogBreedsModel _dogBreedsModel;
    [Inject] private WeatherView _weatherView;
    [Inject] private DogBreedsView _dogBreedsView;
    [Inject] private NavigationView _navigationView;

    private float _weatherTimer = 0f;
    private bool _isWeatherTabActive = true;

    private void Start()
    {
        _navigationView.OnTabSwitched += HandleTabSwitch;
        _weatherView.gameObject.SetActive(true);
        _dogBreedsView.gameObject.SetActive(false);
        _weatherModel.RequestWeather();
    }

    private void Update()
    {
        if (_isWeatherTabActive)
        {
            _weatherTimer += Time.deltaTime;
            if (_weatherTimer >= 5f)
            {
                _weatherModel.RequestWeather();
                _weatherTimer = 0f;
            }
        }
    }

    private void HandleTabSwitch(int tabIndex)
    {
        _isWeatherTabActive = tabIndex == 0;
        
        _weatherView.gameObject.SetActive(_isWeatherTabActive);
        _dogBreedsView.gameObject.SetActive(!_isWeatherTabActive);

        if (_isWeatherTabActive)
        {
            _dogBreedsModel.CancelRequests();
            _weatherModel.RequestWeather();
            _dogBreedsView.OnTabDeactivated();
        }
        else
        {
            _weatherModel.CancelRequests();
            _dogBreedsModel.RequestDogBreeds();
        }
    }

    private void OnDestroy()
    {
        _navigationView.OnTabSwitched -= HandleTabSwitch;
    }
}