using ClothBazar.Entities;

namespace ClothBazarBD.ViewModels
{
    public class CheckoutViewModels
    {
        public List<Product>  CartProducts { get; set; }
        public List<int> CartProductIDs { get; set; }
    }
}
