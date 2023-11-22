using Microsoft.AspNetCore.Mvc;
using ShopTARge22.Core.Dto.AccuWeatherDtos;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Models.AccuWeather;

namespace ShopTARge22.Controllers
{
	public class AccuWeatherController : Controller
	{
		private readonly IAccuWeatherServices _accuWeatherServices;

		public AccuWeatherController
			(
			IAccuWeatherServices accuWeatherServices)
		{
			_accuWeatherServices = accuWeatherServices;
		}

		[HttpGet]

		public IActionResult Index()
		{
			//SearchCityViewModel model = new();
			return View();
		}

		[HttpPost]
		public IActionResult SearchCity(SearchCityViewModel model)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("City", "AccuWeather", new { city = model.CityName });
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> City(string city)
		{
			string locationKey = await _accuWeatherServices.GetLocationKey(city);

			if (locationKey != null)
			{
				AccuWeatherResultDto dto = new ();
				dto.City = city;
				dto.Key = locationKey;
				
				await _accuWeatherServices.AccuWeatherResult(dto);

				AccuWeatherViewModel vm = new AccuWeatherViewModel();

				vm.City = dto.City;
				vm.Temperature = dto.Temperature;
				vm.RealFeelTemperature = dto.RealFeelTemperature;
				vm.RelativeHumidity = dto.RelativeHumidity;
				vm.Pressure = dto.Pressure;
				vm.WindSpeed = dto.WindSpeed;
				vm.WeatherText = dto.WeatherText;
				
				return View(vm);
			}

			return RedirectToAction("Index");
		
		}
		

	}
}

