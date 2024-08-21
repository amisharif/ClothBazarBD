
using ClothBazar.Entities;
using ClothBazar.ServiceContracts;

namespace ClothBazar.Services
{

	public class CategoriesService : ICategoriesService
	{

		private readonly CBContext _categoryDb;
        public CategoriesService( CBContext cBContext )
        {
            _categoryDb = cBContext;
        }


        public void SaveCategory(Category category)
		{
			_categoryDb.Add( category );
			_categoryDb.SaveChanges();
		}

		public void DeleteCategoryByCategoryId(int ID)
		{
			throw new NotImplementedException();
		}


		public void UpdateCategory(Category category)
		{
			throw new NotImplementedException();
		}

		public List<Category> GetAllCategories()
		{
			return _categoryDb.Categories.ToList();
		}
	}
}
