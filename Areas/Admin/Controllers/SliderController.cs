using Juan.Helpers;
using Juan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Juan.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SliderController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _env;
		public SliderController(DataContext dataContext, IWebHostEnvironment env)
		{
			_dataContext = dataContext;
			_env = env;
		}

		public IActionResult Index()
		{
			List<Slider> sliderlist = _dataContext.Sliders.ToList();
			return View(sliderlist);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Slider slider)
		{
			if (slider.FormFile != null)
			{
				if (slider.FormFile.ContentType != "image/png" && slider.FormFile.ContentType != "image/jpeg")
				{
					ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
					return View();
				}
				if (slider.FormFile.Length > 3145728)
				{
					ModelState.AddModelError("ImageFile", "It can be 3 Mb!");
					return View();
				}

				if (!ModelState.IsValid) return View();

				slider.Image = FileManage.SaveFile(_env.WebRootPath, "uploads/sliders", slider.FormFile);
				_dataContext.Sliders.Add(slider);
				_dataContext.SaveChanges();
				return RedirectToAction("index");
			}
			return View();
		}

		[HttpGet]

		public IActionResult Update(int id)
		{
			Slider slider = _dataContext.Sliders.Find(id);
			if (slider == null) return NotFound();
			return View(slider);

		}

		[HttpPost]
		public IActionResult Update(Slider slider)
		{
			Slider existslider = _dataContext.Sliders.Find(slider.Id);
			if (existslider == null) return NotFound();
			if (slider.FormFile != null)
			{

				if (slider.FormFile.ContentType != "image/png" && slider.FormFile.ContentType != "image/jpeg")
				{
					ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
					return View();
				}
				if (slider.FormFile.Length > 3145728)
				{
					ModelState.AddModelError("ImageFile", "It can be 3 Mb!");
					return View();
				}

				string name = FileManage.SaveFile(_env.WebRootPath, "uploads/sliders", slider.FormFile);
				//string deletePath = Path.Combine(_env.WebRootPath, "uploads/sliders", existslider.ImageUrl);
				//if(System.IO.File.Exists(deletePath))
				//{
				//    System.IO.File.Delete(deletePath);
				//}
				FileManage.DeleteFile(_env.WebRootPath, "uploads/sliders", existslider.Image);
				existslider.Image = name;
			}
			existslider.Title = slider.Title;
			existslider.Desc = slider.Desc;
			existslider.Button = slider.Button;
			existslider.URL = slider.URL;
			existslider.Tag = slider.Tag;
			//existslider. = slider.;
			_dataContext.SaveChanges();
			return RedirectToAction("index");
		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{


			Slider slider = _dataContext.Sliders.Find(id);
			if (slider == null) return NotFound();
			return View(slider);
		}
		[HttpPost]

		public IActionResult Delete(int id)
		{
			Slider slider = _dataContext.Sliders.Find(id);
			if (slider == null) return NotFound();
			if (slider.Image != null)
			{
				FileManage.DeleteFile(_env.WebRootPath, "uploads/sliders", slider.Image);
			}

			_dataContext.Sliders.Remove(slider);
			_dataContext.SaveChanges();
			return Ok();
		}

	}
}
