using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPICoreDapper.Data.Repositories.Interfaces;
using WebAPICoreDapper.Data.ViewModels;

namespace WebAPICoreDapper.Data.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly string _connectionString;

        public AttributeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
        }
        public async Task Add(string culture, AttributeViewModel attribute)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                var paramaters = new DynamicParameters();
                paramaters.Add("@code", attribute.Code);
                paramaters.Add("@name", attribute.Name);
                paramaters.Add("@sortOrder", attribute.SortOrder);
                paramaters.Add("@backendType", attribute.BackendType);
                paramaters.Add("@isActive", attribute.IsActive);
                paramaters.Add("@hasOption", attribute.HasOption);
                paramaters.Add("@values", attribute.Values);
                await conn.ExecuteAsync("Create_Attribute", paramaters, null, null, System.Data.CommandType.StoredProcedure);
            }

        }

        public async Task Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                var paramaters = new DynamicParameters();
                paramaters.Add("@id", id);
                await conn.ExecuteAsync("Delete_Attribute_ById", paramaters, null, null, System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<List<AttributeViewModel>> GetAll(string culture)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();

                var paramaters = new DynamicParameters();
                paramaters.Add("@language", culture);

                var result = await conn.QueryAsync<AttributeViewModel>("Get_Attribute_All", paramaters, null, null, System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<AttributeViewModel> GetById(int id, string culture)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                var paramaters = new DynamicParameters();
                paramaters.Add("@id", id);
                paramaters.Add("@language", culture);

                var result = await conn.QueryAsync<AttributeViewModel>("Get_Attribute_ById", paramaters, null, null, System.Data.CommandType.StoredProcedure);
                return result.SingleOrDefault();
            }
        }

        public async Task Update(int id, string culture, AttributeViewModel attribute)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();
                var paramaters = new DynamicParameters();
                paramaters.Add("@id", id);
                paramaters.Add("@name", attribute.Name);
                paramaters.Add("@sortOrder", attribute.SortOrder);
                paramaters.Add("@backendType", attribute.BackendType);
                paramaters.Add("@isActive", attribute.IsActive);
                paramaters.Add("@hasOption", attribute.HasOption);
                paramaters.Add("@values", attribute.Values);
                await conn.ExecuteAsync("Update_Attribute", paramaters, null, null, System.Data.CommandType.StoredProcedure);
            }

        }
    }
}
