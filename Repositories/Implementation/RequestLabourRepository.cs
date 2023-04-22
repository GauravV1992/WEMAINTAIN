using BusinessEntities.RequestDto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Dapper;
using System.Data;
using Azure.Core;

namespace Repositories.Implementation
{
    public class RequestLabourRepository : IRequestLabourRepository
    {
        private readonly ApplicationDBContext _context;
        public RequestLabourRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RequestLabour>> GetAll(RequestLabourRequest request)
        {
            var procedureName = "GetAllRequestLabour";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<RequestLabour>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<RequestLabour> GetById(long id)
        {
            var procedureName = "GetRequestLabourById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var RequestLabour = await connection.QueryFirstOrDefaultAsync<RequestLabour>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return RequestLabour;
            }
        }
 
    }
}
