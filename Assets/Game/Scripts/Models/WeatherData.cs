using System;

[Serializable]
public class WeatherData
{
    public WeatherProperties properties;
}

[Serializable]
public class WeatherProperties
{
    public WeatherPeriod[] periods;
}

[Serializable]
public class WeatherPeriod
{
    public int temperature;
    public string shortForecast;
}