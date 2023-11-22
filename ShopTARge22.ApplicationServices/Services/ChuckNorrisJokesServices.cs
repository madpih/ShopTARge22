using Nancy.Json;
using ShopTARge22.Core.Dto.ChuckNorrisJokesDtos;
using ShopTARge22.Core.Dto.WeatherDtos;
using ShopTARge22.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.ApplicationServices.Services
{
    public class ChuckNorrisJokesServices : IChuckNorrisJokesServices
    {
        public async Task<ChuckNorrisJokesResultDto>ChuckNorrisJokesResult(ChuckNorrisJokesResultDto dto)
        {

            string url = $"https://api.chucknorris.io/jokes/random";
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                ChuckNorrisJokesResponseRootDto chuckNorrisJokesResult = new JavaScriptSerializer().Deserialize<ChuckNorrisJokesResponseRootDto>(json);

                dto.Id = chuckNorrisJokesResult.Id;
                dto.IconUrl = chuckNorrisJokesResult.IconUrl;
                dto.Url = chuckNorrisJokesResult.Url;
                dto.Value = chuckNorrisJokesResult.Value;
                dto.CreatedAt = chuckNorrisJokesResult.CreatedAt;
                dto.UpdatedAt = chuckNorrisJokesResult.UpdatedAt;
                //dto.Categories = chuckNorrisJokesResult.Categories[0];


            }

            return dto;
        }
    }
}



