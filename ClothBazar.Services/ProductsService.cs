using ClothBazar.Entities;
using ClothBazar.ServiceContracts;
using ClothBazar.ServiceContracts.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
	public class ProductsService : IProductsService
	{
		private readonly CBContext _db;
        public ProductsService(CBContext cBContext)
        {
            _db = cBContext;
        }
        public void AddProduct(Product product)
		{
			_db.Products.Add(product);
			_db.SaveChanges();
		}

		public List<Product> GetAllProducts()
		{
			return _db.Products.Include("Category").ToList();
          //  return _db.Products.ToList();
		}

        public List<Product> GetFilteredProducts(string? searchBy, string? searchString)
        {
            List<Product> allProducts = GetAllProducts();
            List<Product> matchingProducts = allProducts;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
            {
                return matchingProducts;
            }

            switch (searchBy)
            {
                case nameof(Product.Name):
                    matchingProducts = allProducts.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Name) ?
                    temp.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Product.Category):
                    matchingProducts = allProducts.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Category?.Name) ?
                    temp.Category.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
            }

            return matchingProducts;
 
        }

		public List<Product> GetProductsByCategoryId(int ID)
		{
			return _db.Products.Where(x => x.CategoryID == ID).ToList();
		}

		public List<Product> GetProductsByID(List<int> IDs)
        {
            return _db.Products.Where(x=>IDs.Contains(x.ID)).ToList();  
        }

        public List<Product> GetSortedProducts(List<Product> products, string? sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy)) return products;

            List<Product> sortedProducts = (sortBy, sortOrder) switch
            {
                (nameof(Product.Name),SortOrderOptions.ASC) => products.OrderBy(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(), 

                (nameof(Product.Name),SortOrderOptions.DESC) => products.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(Product.Price), SortOrderOptions.ASC) => products.OrderBy(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(Product.Price), SortOrderOptions.DESC) => products.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(Product.Category), SortOrderOptions.ASC) => products.OrderBy(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(Product.Category), SortOrderOptions.DESC) => products.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),



            };
            return sortedProducts;
        }

        
    }
}
