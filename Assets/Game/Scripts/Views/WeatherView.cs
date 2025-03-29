using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weatherText;
    [SerializeField] private Image _weatherIcon;
    
    [SerializeField] private Sprite _sunnySprite;
    [SerializeField] private Sprite _cloudySprite;
    [SerializeField] private Sprite _partlyCloudySprite;
    [SerializeField] private Sprite _rainySprite;
    [SerializeField] private Sprite _snowySprite;
    [SerializeField] private Sprite _thunderstormSprite;
    [SerializeField] private Sprite _foggySprite;
    [SerializeField] private Sprite _defaultSprite;

    public void DisplayWeather(int temperature, string forecast)
    {
        _weatherText.text = $"Сегодня - {temperature}F";
        SetWeatherIcon(forecast);
    }
    
    private void SetWeatherIcon(string forecast)
    {
        string forecastLower = forecast.ToLower();

        if (forecastLower.Contains("sunny") || forecastLower.Contains("clear"))
        {
            _weatherIcon.sprite = _sunnySprite;
        }
        else if (forecastLower.Contains("cloudy") && !forecastLower.Contains("partly"))
        {
            _weatherIcon.sprite = _cloudySprite;
        }
        else if (forecastLower.Contains("partly cloudy") || forecastLower.Contains("partly sunny"))
        {
            _weatherIcon.sprite = _partlyCloudySprite;
        }
        else if (forecastLower.Contains("rain") || forecastLower.Contains("showers"))
        {
            _weatherIcon.sprite = _rainySprite;
        }
        else if (forecastLower.Contains("snow"))
        {
            _weatherIcon.sprite = _snowySprite;
        }
        else if (forecastLower.Contains("thunderstorm"))
        {
            _weatherIcon.sprite = _thunderstormSprite;
        }
        else if (forecastLower.Contains("fog"))
        {
            _weatherIcon.sprite = _foggySprite;
        }
        else
        {
            _weatherIcon.sprite = _defaultSprite;
            Debug.LogWarning($"Неизвестное значение погоды: {forecast}. Использована стандартная иконка.");
        }
    }
    
}