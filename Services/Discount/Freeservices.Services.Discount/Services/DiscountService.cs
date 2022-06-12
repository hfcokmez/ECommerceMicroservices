using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Freeservices.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public Task<Response<NoContent>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Models.Discount>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> Save(Models.Discount discount)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> Update(Models.Discount discount)
        {
            throw new NotImplementedException();
        }
    }
}

