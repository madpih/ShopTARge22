using Microsoft.EntityFrameworkCore.Metadata;
using Nancy;
using Nancy.Json;
using ShopTARge22.Core.Dto.AccuWeatherDtos;
using ShopTARge22.Core.ServiceInterface;
using System.Net;

namespace ShopTARge22.ApplicationServices.Services
{
	public class AccuWeatherServices : IAccuWeatherServices

	{
		public async Task<AccuWeatherResultDto> AccuWeatherResult(AccuWeatherResultDto dto)
		{
			string locationKey = await GetLocationKey(dto.City);


			if (locationKey != null)
			{

				string apiKey = "fL9TvTyxlGati01Zg6ASvGquQQ3vzxTX";
				string url = $"http://dataservice.accuweather.com/currentconditions/v1/{locationKey}?apikey={apiKey}&details=true";

				using (WebClient client = new WebClient())

				{
					string json = client.DownloadString(url);

					List<AccuWeatherRootDto> weatherResult = new JavaScriptSerializer().Deserialize<List<AccuWeatherRootDto>>(json);

					dto.Temperature = weatherResult[0].Temperature.Metric.Value;
					dto.RealFeelTemperature = weatherResult[0].RealFeelTemperature.Metric.Value;
					dto.RelativeHumidity = weatherResult[0].RelativeHumidity;
					dto.Pressure = weatherResult[0].Pressure.Metric.Value;
					dto.WindSpeed = weatherResult[0].Wind.Speed.Metric.Value;
					dto.WeatherText = weatherResult[0].WeatherText;
				}

				return dto;
			}
			return null;
		}

		public async Task<string> GetLocationKey(string city)
		{
			string apiKey = "8eDECvxiN7UjAN31cXKlLeXLawl84Cn3";
			string urlCity = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={city}";

			using (WebClient client = new WebClient())
			{
				string json = client.DownloadString(urlCity);
				List<AccuWeatherRootDto> locationResults = new JavaScriptSerializer().Deserialize<List<AccuWeatherRootDto>>(json);

				if (locationResults != null)
				{
					return locationResults[0].Key;
				}

				return null;
			}
		}

	}

}


