using ClothBazar.Entities;

namespace ClothBazarBD.ViewModels
{
	public class CategoryViewModel : BaseEntity
	{

		public IFormFile photo { get; set; } = null!;
		public List<Product>? Products { get; set; }

		public bool isFeatured { get; set; }

	}
}
