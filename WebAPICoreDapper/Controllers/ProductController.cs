using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using WebAPICoreDapper.Data.Models;
using WebAPICoreDapper.Data.Repositories;
using WebAPICoreDapper.Data.Repositories.Interfaces;
using WebAPICoreDapper.Data.ViewModels;
using WebAPICoreDapper.Extensions;
using WebAPICoreDapper.Filters;
using WebAPICoreDapper.Resources;
using WebAPICoreDapper.Utilities.Dtos;

namespace WebAPICoreDapper.Controllers
{

    [Route("api/{culture}/[controller]")]
    [ApiController]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class ProductController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<ProductController> _logger;
        private readonly IStringLocalizer<ProductController> _localizer;
        private readonly LocService _locService;

        private readonly IProductRepository _productRepository;

        public ProductController(IConfiguration configuration, LocService locService,
            ILogger<ProductController> logger, IProductRepository productRepository,
            IStringLocalizer<ProductController> localizer)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
            _logger = logger;
            _locService = locService;
            _localizer = localizer;
            _productRepository = productRepository;
        }
        // GET: api/Product
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            //string text = _localizer["Test"];
            //string text1 = _locService.GetLocalizedHtmlString("ForgotPassword");
            return await _productRepository.GetAllAsync(CultureInfo.CurrentCulture.Name);
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Product> Get(int id)
        {
            return await _productRepository.GetByIdAsync(id, CultureInfo.CurrentCulture.Name);
        }

        [HttpGet("paging", Name = "GetPaging")]
        public async Task<PagedResult<Product>> GetPaging(string keyword, int categoryId, int pageIndex, int pageSize)
        {
            return await _productRepository.GetPaging(keyword, CultureInfo.CurrentCulture.Name, categoryId, pageIndex, pageSize);
        }
        // POST: api/Product
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            var newId = await _productRepository.Create(CultureInfo.CurrentCulture.Name, product);
            return Ok(newId);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            await _productRepository.Update(CultureInfo.CurrentCulture.Name, id, product);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.Delete(id);
            return Ok();
        }

        [HttpGet("{id}/attributes")]
        public async Task<List<ProductAttributeViewModel>> GetProductAttributes(int id)
        {
            return await _productRepository.GetAttributes(id, CultureInfo.CurrentCulture.Name);
        }

        [HttpPost("search-attribute")]
        public async Task<PagedResult<Product>> SearchProductByAttributes(string keyword,
            int categoryId,string size, int pageIndex, int pageSize)
        {
            return await _productRepository.SearchByAttributes(keyword, CultureInfo.CurrentCulture.Name, categoryId, size, pageIndex, pageSize);
        }
    }
}
