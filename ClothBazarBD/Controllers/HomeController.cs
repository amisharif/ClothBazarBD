using ClothBazar.Entities;
using ClothBazar.ServiceContracts;
using ClothBazarBD.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothBazarBD.Controllers
{

	[Route("[controller]")]
	public class HomeController : Controller
	{

		private readonly ICategoriesService _categoriesService;
		private readonly IProductsService _productsService;
		public HomeController(ICategoriesService categoriesService,IProductsService productsService)
        {
            _categoriesService = categoriesService;
			_productsService = productsService;
        }


		[Route("/")]
		[Route("[action]")]
		public IActionResult Index()
		{
			HomeViewModels models = new HomeViewModels();
			models.Categories = _categoriesService.GetAllCategories();
			models.Products = _productsService.GetAllProducts().Take(4).OrderByDescending(x=>x.ID).ToList();


		//	var cartProductCookie = Request.Cookies['']

			if (models.Categories.Count > 0)
			{
				return View(models);
			}
			return RedirectToAction("Create","Category");


		}

		[Route("[action]")]
		public IActionResult CategoryProduct(int ID)
		{

			List<Product> allProducts = _productsService.GetAllProducts().Take(8).ToList();
			List<Product> filteredProduct = _productsService.GetProductsByCategoryId(ID).Take(4).ToList();

			if (ID == 0)
			{
				return PartialView(allProducts);
			}

			return PartialView(filteredProduct);
		}
	}
}
