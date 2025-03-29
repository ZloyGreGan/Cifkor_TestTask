using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class WeatherModel
{
    [Inject] private RequestQueueSystem _requestQueue;
    [Inject] private WeatherView _weatherView;

    public void RequestWeather()
    {
        _requestQueue.AddRequest(() =>
        {
            UnityWebRequest request = UnityWebRequest.Get("https://api.weather.gov/gridpoints/TOP/32,81/forecast");
            _requestQueue.SetCurrentRequest(request);
            
            request.SendWebRequest().completed += _ =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    WeatherData data = JsonUtility.FromJson<WeatherData>(request.downloadHandler.text);
                    if (data?.properties?.periods != null && data.properties.periods.Length > 0)
                    {
                        _weatherView.DisplayWeather(data.properties.periods[0].temperature, data.properties.periods[0].shortForecast);
                    }
                }
                
                _requestQueue.CompleteRequest();
            };
        });
    }

    public void CancelRequests()
    {
        _requestQueue.ClearQueue();
    }
}