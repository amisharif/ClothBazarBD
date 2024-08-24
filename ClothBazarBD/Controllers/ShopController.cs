using ClothBazar.Entities;
using ClothBazar.ServiceContracts;
using ClothBazar.Services;
using ClothBazarBD.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothBazarBD.Controllers
{
	[Route("[controller]")]
	public class ShopController : Controller
	{

		private readonly IProductsService _productService;
        public ShopController(IProductsService productsService)
        {
            _productService = productsService;
        }

        [Route("[action]")]
        public IActionResult Index()
		{
	

            CheckoutViewModels checkoutViewModels = new CheckoutViewModels();
            var CartProductsCookie = Request.Cookies["CartProducts"];

            List<Product> allProduct = _productService.GetAllProducts();

            int totalProducts = allProduct.Count();
            var numberOfPage = (int)Math.Ceiling(totalProducts / 9.0);
            ViewData["totalPage"] = numberOfPage;

            var mx = allProduct.Max(x => x.Price);
            var mn = allProduct.Min(x => x.Price);

			ViewBag.mx = mx;
			ViewBag.mn = mn;


            if (CartProductsCookie != null)
			{
				var productIDs = CartProductsCookie;
				var ids = productIDs.Split('-');
				List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

				checkoutViewModels.CartProducts = _productService.GetProductsByID(pIDs);
				
				checkoutViewModels.CartProductIDs = pIDs;
			}

			return View();
		}



        [Route("[action]")]
        public IActionResult FilterProducts(int minimumPrice,int maximumPrice,int pageNo=1)
		{

			List<Product>allProduct = _productService.GetAllProducts();
            List<Product> productByPage = _productService.GetProductsByPageNo(pageNo, 9).ToList();
            List<Product>  filterProducts = _productService.GetFilterProductsByPrice(minimumPrice, maximumPrice, productByPage).ToList();
           


            return PartialView(filterProducts);

   //         if (minimumPrice==0 && maximumPrice == 0)
			//{
			//	return PartialView(productByPage);
			//}
	
		}




    }
}
