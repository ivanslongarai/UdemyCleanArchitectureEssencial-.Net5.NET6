using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/v1/categories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            try
            {
                _logger.LogInformation("Getting all categories");
                var categories = await _categoryService.GetCategoriesAsync();
                if (categories == null || categories.Count() == 0)
                    return NotFound(new { message = $"No categories could be found" });
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("api/v1/categories/{id}")]
        [HttpGet]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            _logger.LogInformation("Getting category by id {1}", id);
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null)
                    return NotFound(new { message = $"Id {id} could not be found" });
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("api/v1/categories")]
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CategoryDTO categoryDTO)
        {
            _logger.LogInformation("Creating category");
            try
            {
                var category = await _categoryService.CreateAsync(categoryDTO);
                if (category == null)
                {
                    _logger.LogError("It was not possible to create a new category");
                    return StatusCode(500);
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("api/v1/categories")]
        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> Update(CategoryDTO categoryDTO)
        {
            _logger.LogInformation("Updating category, categoryId {1}", categoryDTO.Id);
            try
            {
                var category = await _categoryService.GetByIdAsync(categoryDTO.Id);
                if (category == null)
                    return NotFound(new { message = $"Id {categoryDTO.Id} could not be found" });
                var result = await _categoryService.UpdateAsync(categoryDTO);
                if (result == null)
                {
                    _logger.LogError("It was not possible to update the category id {1}", categoryDTO.Id);
                    return StatusCode(500);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("api/v1/categories/{id}")]
        [HttpDelete]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            _logger.LogInformation("Deleting category, categoryId {1}", id);
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null)
                    return NotFound(new { message = $"Id {id} could not be found" });
                var result = await _categoryService.RemoveAsync(id);
                if (result == null)
                {
                    _logger.LogError("It was not possible to delete the category id {1}", id);
                    return StatusCode(500);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
