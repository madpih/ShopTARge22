using ShopTARge22.Core.Dto.ChuckNorrisJokesDtos;
using ShopTARge22.Core.Dto.WeatherDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.Core.ServiceInterface
{
    public interface IChuckNorrisJokesServices
    {
        Task<ChuckNorrisJokesResultDto> ChuckNorrisJokesResult(ChuckNorrisJokesResultDto dto);
    }
}

