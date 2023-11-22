using ShopTARge22.Core.Dto.ChuckNorrisJokesDtos;
using ShopTARge22.Core.Dto.CoctailsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.Core.ServiceInterface
{
    public interface ICoctailServices
    {
        Task<CoctailResultDto> GetCoctails(CoctailResultDto dto);

    }
}
