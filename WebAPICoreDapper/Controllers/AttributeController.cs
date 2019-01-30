using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using WebAPICoreDapper.Data.Repositories.Interfaces;
using WebAPICoreDapper.Data.ViewModels;
using WebAPICoreDapper.Extensions;
using WebAPICoreDapper.Filters;

namespace WebAPICoreDapper.Controllers
{
    [Route("api/{culture}/[controller]")]
    [ApiController]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class AttributeController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IAttributeRepository _attributeRepository;

        public AttributeController(IConfiguration configuration, IAttributeRepository attributeRepository)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
            _attributeRepository = attributeRepository;

        }

        [HttpGet("{id}")]
        public async Task<AttributeViewModel> Get(int id)
        {
            return await _attributeRepository.GetById(id, CultureInfo.CurrentCulture.Name);
        }

        [HttpGet]
        public async Task<List<AttributeViewModel>> GetAll()
        {
            return await _attributeRepository.GetAll(CultureInfo.CurrentCulture.Name);
        }

        [HttpPost]
        [ValidateModel]
        public async Task AddAttribute([FromBody]AttributeViewModel attribute)
        {
            await _attributeRepository.Add(CultureInfo.CurrentCulture.Name, attribute);
        }

        [HttpPut("{id}")]
        [ValidateModel]

        public async Task Update(int id, [FromBody]AttributeViewModel attribute)
        {
            await _attributeRepository.Update(id, CultureInfo.CurrentCulture.Name, attribute);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _attributeRepository.Delete(id);
        }
    }
}