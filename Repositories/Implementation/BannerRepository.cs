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
    public class BannerRepository : IBannerRepository
    {
        private readonly ApplicationDBContext _context;
        public BannerRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(BannerRequest viewModel)
        {
            var procedureName = "SaveBanner";
            var parameters = new DynamicParameters();
            parameters.Add("Rank", viewModel.Rank, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Ext", viewModel.Ext, DbType.String, ParameterDirection.Input);
            parameters.Add("CreatedBy", viewModel.CreatedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var banners = await connection.QuerySingleAsync<Banner>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return banners.Id;
            }
        }
        public async Task<long> Delete(long id)
        {
            var procedureName = "DeleteBanner";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var banners = await connection.QuerySingleAsync<Banner>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return banners.Id;
            }
        }
        public async Task<IEnumerable<Banner>> GetAll(BannerRequest request)
        {
            var procedureName = "GetAllBanner";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var banner = await connection.QueryAsync<Banner>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return banner;
            }
        }
    }
}
