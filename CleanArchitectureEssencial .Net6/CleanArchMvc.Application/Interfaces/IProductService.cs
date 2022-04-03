using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Application.CQRS.CommandResults;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{

    public interface IProductService
    {
        Task<GenericCommandResult> GetProductsAsync();
        Task<GenericCommandResult> GetByIdAsync(int id);
        Task<GenericCommandResult> CreateAsync(ProductDTO productDTO);
        Task<GenericCommandResult> UpdateAsync(ProductDTO productDTO);
        Task<GenericCommandResult> RemoveAsync(int id);
    }
}
