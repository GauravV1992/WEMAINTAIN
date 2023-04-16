using BusinessEntities.RequestDto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dapper;
using System.Data;
using Azure.Core;

namespace Repositories.Implementation
{
    public class PackageRateLogRepository : IPackageRateLogRepository
    {
        private readonly ApplicationDBContext _context;
        public PackageRateLogRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<PackageRateLog>> GetAll(PackageRateLogRequest request)
        {
            var procedureName = "GetAllPackageRateLog";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<PackageRateLog>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
   
    }
}
