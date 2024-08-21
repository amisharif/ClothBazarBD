﻿using ClothBazar.Entities;
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


	
	}
}
