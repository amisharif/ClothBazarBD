using ClothBazar.Entities;
using ClothBazar.ServiceContracts.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.ServiceContracts
{
	public interface IProductsService
	{
		void AddProduct(Product product);
		List<Product> GetAllProducts();
		List<Product> GetFilteredProducts(string? searchBy, string? searchString);
		List<Product> GetSortedProducts(List<Product> products,string? sortBy,SortOrderOptions sortOrder);
		List<Product> GetProductsByID(List<int> IDs);
		List<Product> GetProductsByCategoryId(int ID);
		List<Product> GetFilterProductsByPrice(decimal min,decimal max,List<Product>products);
		List<Product> GetProductsByPageNo(int pageNo,int numOfProd);
	}
}
