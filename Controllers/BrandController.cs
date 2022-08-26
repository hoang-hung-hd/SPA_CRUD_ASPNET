using Microsoft.AspNetCore.Mvc;
using useCookieAuth.Models;
using useCookieAuth.Services;

namespace useCookieAuth.Controllers
{
    public class BrandController : Controller
    {
        private BrandService _brandService;

        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }

        public IActionResult Index()
        {
            var brands = _brandService.GetAll();
            return View(brands);
        }
        public IActionResult GetById(int id)
        {          
            return View();
        }
        public IActionResult GetAllBrand()
        {
            var brands = _brandService.GetAll();
            return Ok(new { models = brands, status = 1, message = "Brand retrived success!" });
        }
        public IActionResult GetDetailBrand(int id)
        {
            try
            {
                var brand = _brandService.GetById(id);
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOrEdit([FromBody] Brand model)
        {
            if(model.BrandId == 0 || model.BrandId == null)
            {
                _brandService.Create(model);
                return Ok(new { status = 1, message = "Create success!" });
            }else
            {
                _brandService.Update(model.BrandId, model);
                return Ok(new { status = 1, message = "Update success!" , model = model });
            }          
            
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var brand = _brandService.GetById(id);

            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }
        public IActionResult Update(int id, Brand model)
        {
            _brandService.Update(id, model);
            return RedirectToAction("Index", "Brand");
        }
        public IActionResult DeleteBrand(int id)
        {
            _brandService.Delete(id);
            return Ok(new { status = 1, message = "Delete brand success" });
        }
    }
}
