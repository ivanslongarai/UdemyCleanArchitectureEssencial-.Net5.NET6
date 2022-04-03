using System.IO;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categotyService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService,
            ICategoryService categotyService, IWebHostEnvironment environment)
        {
            _logger = logger;
            _productService = productService;
            _categotyService = categotyService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            if (products.Success)
                return View(products.Data);
            return BadRequest(products.Notifications);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.CategoryId = new SelectList(await _categotyService.GetCategoriesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = new SelectList(await _categotyService.GetCategoriesAsync(), "Id", "Name");
            }
            return View(productDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            var categories = await _categotyService.GetCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", (product.Data as ProductDTO).CategoryId);
            return View(product.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(productDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product.Data == null)
                return NotFound();
            return View(product.Data);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, "Images\\" + (product.Data as ProductDTO).Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;
            return View(product.Data);
        }
    }
}
