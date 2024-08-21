using ClothBazar.Entities;

namespace ClothBazarBD.ViewModels
{
	public class HomeViewModels
	{

        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set; }
        public List<Product> filteredProduct { get; set; }
    }
}
