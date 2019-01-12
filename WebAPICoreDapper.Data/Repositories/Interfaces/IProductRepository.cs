using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPICoreDapper.Data.Models;
using WebAPICoreDapper.Utilities.Dtos;

namespace WebAPICoreDapper.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(string culture);
        Task<Product> GetByIdAsync(int id, string culture);

        Task<PagedResult<Product>> GetPaging(string keyword, string culture, int categoryId, int pageIndex, int pageSize);

        Task<int> Create(string culture, Product product);

        Task Update(string culture, int id, Product product);

        Task Delete(int id);


    }
}
