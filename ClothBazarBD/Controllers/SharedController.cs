using Microsoft.AspNetCore.Mvc;

namespace ClothBazarBD.Controllers
{
	public class SharedController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
