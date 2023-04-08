using BusinessEntities.RequestDto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dapper;
using System.Data;

namespace Repositories.Implementation
{
    public class PackageRateLogRepository : IPackageRateLogRepository
    {
        private readonly ApplicationDBContext _context;
        public PackageRateLogRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<PackageRateLog>> GetAll()
        {
            var procedureName = "GetAllPackageRateLog";
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<PackageRateLog>
           (procedureName, null, commandType: CommandType.StoredProcedure);
                return packages;
            }
        }
   
    }
}
