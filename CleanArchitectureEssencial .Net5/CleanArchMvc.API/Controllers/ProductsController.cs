using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;

        public ProductsController(ILogger<ProductsController> logger,
                IProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/v1/products")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            try
            {
                _logger.LogInformation("Getting all products");
                var result = await _productService.GetProductsAsync();
                if (result.Data == null)
                    return NotFound(new { message = $"No products could be found" });
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("api/v1/products/{id}")]
        [HttpGet]
        public async Task<ActionResult<ProductDTO>> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting product by id {1}", id);
            var result = await _productService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result.Data);
            else
                return BadRequest(result.Notifications);
        }

        [Route("api/v1/products")]
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDTO)
        {
            _logger.LogInformation("Creating product");
            try
            {
                var result = await _productService.CreateAsync(productDTO);
                if (result.Data == null)
                {
                    _logger.LogError("It was not possible to create a new product");
                    return StatusCode(500, new {Messages = result.Notifications});
                }
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("api/v1/products")]
        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Update(ProductDTO productDTO)
        {
            _logger.LogInformation("Updating product, categoryId {1}", productDTO.Id);

            try
            {
                var result = await _productService.GetByIdAsync(productDTO.Id);

                if (result.Data == null)
                    return NotFound(new { message = $"Id {productDTO.Id} could not be found" });
                
                var resultUpdate = await _productService.UpdateAsync(productDTO);

                if (resultUpdate == null)
                {
                    _logger.LogError("It was not possible to update the product id {1}", productDTO.Id);
                    return StatusCode(500);
                }

                return Ok(resultUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("api/v1/products/{id}")]
        [HttpDelete]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            _logger.LogInformation("Deleting product, productId {1}", id);
            try
            {
                var result = await _productService.GetByIdAsync(id);                            
                if (result.Data == null)
                    return NotFound(new { message = $"Id {id} could not be found" });
                var resultDto = await _productService.RemoveAsync(id);
                if (resultDto == null)
                {
                    _logger.LogError("It was not possible to delete the category id {1}", id);
                    return StatusCode(500);
                }
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

    }
}
