﻿
using Nancy.Json;
using ShopTARge22.Core.Dto.WeatherDtos;
using ShopTARge22.Core.ServiceInterface;
using System.Net;

namespace ShopTARge22.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
       public async Task<OpenWeatherResultDto> OpenWeatherResult(OpenWeatherResultDto dto)
        {
            string idOpenWeather = "2fbc81f7d6f5a60205aef5949d8d2c82"; //api key, kui puudu, siis tulemusi ei kuva, nullid.
            
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={dto.City},&units=metric&appid={idOpenWeather}";

            //mis peab tegema andmetega api call puhul -> deserialiseerima
            //andmed tulevad JSOn kujul siia ja mis peab tegema, et need muuta c# arusaadavaks
            using (WebClient client = new WebClient()) { 
                string json = client.DownloadString(url);
                WeatherResponseRootDto weatherResult = new JavaScriptSerializer().Deserialize<WeatherResponseRootDto>(json);

                dto.City = weatherResult.Name;
                dto.Temp = weatherResult.Main.Temp;
                dto.FeelsLike = weatherResult.Main.Feels_like;
                dto.Humidity = weatherResult.Main.Humidity;
                dto.Pressure = weatherResult.Main.Pressure;
                dto.WindSpeed = weatherResult.Wind.Speed;
                dto.Description = weatherResult.Weather[0].Description;

            }

            return dto;
        }
    }
}
