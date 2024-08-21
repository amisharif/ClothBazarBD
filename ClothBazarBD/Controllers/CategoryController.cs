using ClothBazar.Entities;
using ClothBazar.ServiceContracts;
using ClothBazarBD.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothBazarBD.Controllers
{
	

	[Route("[controller]")]
	public class CategoryController : Controller
    {
	
		private readonly ICategoriesService _categoriesService;
		IWebHostEnvironment env;

        public CategoryController(ICategoriesService categoriesService,IWebHostEnvironment webHostEnvironment)
        {
            _categoriesService = categoriesService;
			env = webHostEnvironment;
        }

		
		[Route("[action]")]
		public IActionResult Index()
        {
			List<Category> categories = _categoriesService.GetAllCategories();
			return View(categories);
        }

		[HttpGet]
		//[Route("/")]
		[Route("[action]")]
        public IActionResult Create()
        {
			return View();
        }


		[HttpPost]
		[Route("[action]")]
		
		public IActionResult Create(CategoryViewModel prod)
		{

			string fileName = "";

			if (prod != null)
			{
				string folder = Path.Combine(env.WebRootPath, "images/category");
				fileName = Guid.NewGuid().ToString() + "_" + prod.photo.FileName;


				string filePath = Path.Combine(folder, fileName);
				prod.photo.CopyTo(new FileStream(filePath, FileMode.Create));

				Category category = new Category()
				{
					Name = prod.Name,
					Description = prod.Description,
					ImageURL = filePath,
				};


				category.ImageURL = fileName;
				category.Name = prod.Name;
				category.Description = prod.Description;
				category.isFeatured = prod.isFeatured;

				_categoriesService.SaveCategory(category);
			}


			return View();
		}


	}
}
