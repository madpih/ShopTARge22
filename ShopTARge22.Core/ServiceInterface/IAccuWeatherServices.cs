using ShopTARge22.Core.Dto.AccuWeatherDtos;

namespace ShopTARge22.Core.ServiceInterface
{
	public interface IAccuWeatherServices
	{
		Task<AccuWeatherResultDto> AccuWeatherResult(AccuWeatherResultDto dto);

		Task<string> GetLocationKey(string city);
	

	}
}
