using ClothBazar.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClothBazarBD.ViewModels
{
	public class ProductViewModel : BaseEntity
	{
		[Range(1, 100000)]
		public decimal Price { get; set; }

		public int CategoryID { get; set; }
		public virtual Category? Category { get; set; }

		public IFormFile Photo { get; set; }
	}
}
