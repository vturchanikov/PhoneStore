using Microsoft.AspNetCore.Mvc;

namespace PhoneShop.Controllers
{
    public class ProductController : Controller
    {
        public ViewResult Index() =>
            View();

        public ViewResult List(int categoryId)
        {
            return View();
        }
    }
}
