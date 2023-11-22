using Microsoft.AspNetCore.Mvc;
using ShopTARge22.Core.Dto.ChuckNorrisJokesDtos;
using ShopTARge22.Core.Dto.WeatherDtos;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.Models.ChuckNorrisJokes;


namespace ShopTARge22.Controllers
{
    public class ChuckNorrisJokesController : Controller
    {
        private readonly IChuckNorrisJokesServices _chuckNorrisJokesServices;
        public ChuckNorrisJokesController
            (
            IChuckNorrisJokesServices chuckNorrisJokesServices)
        {
           _chuckNorrisJokesServices = chuckNorrisJokesServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
           
            return View();

        }

        [HttpPost]
        public IActionResult SearchChuckNorrisJokes(ChuckNorrisJokesViewModel model)
        {
            //if(ModelState.IsValid)
            //{
            return RedirectToAction("Joke", "ChuckNorrisJokes");
            //}

            //return View(model);
        }


        [HttpGet]
        public IActionResult Joke ()
        {

            ChuckNorrisJokesResultDto dto = new();

            _chuckNorrisJokesServices.ChuckNorrisJokesResult(dto); //service väljakutsumine
            ChuckNorrisJokesViewModel vm = new();

            vm.Categories = dto.Categories;
            vm.CreatedAt = dto.CreatedAt;
            vm.IconUrl = dto.IconUrl;
            vm.Id = dto.Id;
            vm.UpdatedAt = dto.UpdatedAt;
            vm.Url = dto.Url;
            vm.Value = dto.Value;


            return View(vm);

        }

    }
    }
