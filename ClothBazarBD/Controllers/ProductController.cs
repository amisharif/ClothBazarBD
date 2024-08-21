using ClothBazar.Entities;
using ClothBazar.ServiceContracts;
using ClothBazar.ServiceContracts.Enums;
using ClothBazar.Services;
using ClothBazarBD.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClothBazarBD.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
	{
        private readonly ICategoriesService _catgoriesService;
		private readonly IProductsService _productsService;

		IWebHostEnvironment env;
		public ProductController(ICategoriesService categoriesService, IWebHostEnvironment webHostEnvironment,IProductsService productsService)
        {
            _catgoriesService = categoriesService;
			_productsService = productsService;
			env = webHostEnvironment;
        }





      //  [Route("/")]
        [Route("[action]")]
        public IActionResult Index(string searchBy,string? searchString,string sortBy = nameof(Product.Name),SortOrderOptions sortOrder = SortOrderOptions.ASC)
		{

		
			ViewBag.SearchFields = new Dictionary<string, string>()
			{
			{ nameof(Product.Name), "Product Name" },
			{ nameof(Product.Category), "Category" },
			};


            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;
			ViewBag.CurrentSortBy = sortBy;
			ViewBag.CurrentSortOrder = sortOrder;

            List<Product> productss = _productsService.GetAllProducts();
			
			List<Product> products = _productsService.GetFilteredProducts(searchBy, searchString);
			List<Product> sortedProducts = _productsService.GetSortedProducts(products, sortBy, sortOrder);

			//return View(sortedProducts);
			return View(productss);
		}



        [HttpGet]
      //  [Route("/")]
        [Route("[action]")]
        public IActionResult Create( )
		{
			List<Category> categories = _catgoriesService.GetAllCategories();

			ViewBag.Products = categories.Select(category =>
				new SelectListItem() { Text = category.Name, Value = category.ID.ToString() }
			);

			return View();
		}


        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(ProductViewModel prod)
        {
			List<Category> categories = _catgoriesService.GetAllCategories();
			ViewBag.Products = categories.Select(temp =>
			   new SelectListItem() { Text = temp.Name, Value = temp.ID.ToString() }
		     );


			string fileName = "";

			if (prod != null)
			{
				string folder = Path.Combine(env.WebRootPath, "images/products");
				fileName = Guid.NewGuid().ToString() + "_" + prod.Photo.FileName;


				string filePath = Path.Combine(folder, fileName);
				prod.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

				Product product = new Product()
				{
					Name = prod.Name,
					Description = prod.Description,
					ImageURL = fileName,
					Price = prod.Price,
					CategoryID = prod.CategoryID,
				};
				_productsService.AddProduct(product);
				
			}

			return View();
        }
    }
}
