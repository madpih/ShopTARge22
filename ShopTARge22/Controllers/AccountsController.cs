using Microsoft.AspNetCore.Mvc;

namespace ShopTARge22.Controllers
{
    public class AccountsController : Controller
    {
        //suunab indexi asemel Register lehele esimese asjana, tagastab Register view
        //public IActionResult Index()
        public IActionResult Register()

        {
            return View();
        }
    }
}
