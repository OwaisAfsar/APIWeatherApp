namespace APIWeatherApp.Models
{
    public class WeatherData
    {
        public static ResultModel GetWeatherData(ResultModel _resultModel, RootObject _weatherInformation)
        {
            _resultModel.City = _weatherInformation.name;
            _resultModel.Country = _weatherInformation.sys.country;
            _resultModel.description = _weatherInformation.weather[0].main;
            _resultModel.Temperature = string.Format("{0} °C", ValueConverter(_weatherInformation.main.temp));
            _resultModel.TemperatureMinimum = string.Format("{0} °C", ValueConverter(_weatherInformation.main.temp_min));
            _resultModel.TemperatureMaximum = string.Format("{0} °C", ValueConverter(_weatherInformation.main.temp_max));
            _resultModel.AirPressure = string.Format("{0} hPa", _weatherInformation.main.pressure);
            _resultModel.Humidity = string.Format("{0}%", _weatherInformation.main.humidity);
            _resultModel.WindSpeed = string.Format("{0} m/s", _weatherInformation.wind.speed);
            _resultModel.WindDirection = string.Format("{0}°", _weatherInformation.wind.deg);
            _resultModel.CloudCover = _weatherInformation.clouds.all.ToString();

            return _resultModel;
        }

        public static double ValueConverter(double _value)
        {
            double convertedValue = _value - 273.15;
            return Math.Round(convertedValue, 2);
        }
    }
}
