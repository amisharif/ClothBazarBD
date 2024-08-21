using ClothBazar.Entities;


namespace ClothBazar.ServiceContracts
{
	public interface ICategoriesService
	{

		void SaveCategory(Category category);
		void UpdateCategory(Category category);
		void DeleteCategoryByCategoryId(int ID);
		List<Category> GetAllCategories();
		//Category GetCategoryByCategoryId(int ID);
		//int GetCategoriesCount(string search);

	}
}
