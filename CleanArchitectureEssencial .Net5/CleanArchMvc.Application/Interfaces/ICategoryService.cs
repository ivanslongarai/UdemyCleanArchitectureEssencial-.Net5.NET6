using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO);
        Task<CategoryDTO> UpdateAsync(CategoryDTO categoryDTO);
        Task<CategoryDTO> RemoveAsync(int id);
    }
}
