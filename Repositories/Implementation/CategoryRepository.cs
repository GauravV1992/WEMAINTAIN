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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(CategoryRequest viewModel)
        {
            var procedureName = "SavePackage";
            var parameters = new DynamicParameters();
            parameters.Add("Name", viewModel.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("CreatedBy", viewModel.CreatedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QuerySingleAsync<Package>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages.Id;
            }
        }
        public async Task<long> Update(CategoryRequest viewModel)
        {
            var procedureName = "SavePackage";
            var parameters = new DynamicParameters();
            parameters.Add("Name", viewModel.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("Id", viewModel.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ModifiedBy", viewModel.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QuerySingleAsync<Package>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages.Id;
            }
        }
        public async Task<long> Delete(long id)
        {
            var procedureName = "DeletePackage";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QuerySingleAsync<Package>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages.Id;
            }
        }
        public async Task<IEnumerable<Package>> GetAll(CategoryRequest request)
        {
            var procedureName = "GetAllPackage";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<Package>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
        public async Task<Package> GetById(long id)
        {
            var procedureName = "GetPackageById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryFirstOrDefaultAsync<Package>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages;
            }
        }
        public async Task<IList<SelectListItem>> GetPackages()
        {
            List<SelectListItem> dataList = new List<SelectListItem>();
            var procedureName = "GetPackageNames";
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<Package>
           (procedureName, null, commandType: CommandType.StoredProcedure);

                dataList.Add(new SelectListItem() { Text = "Select", Value = "" });
                foreach (var item in packages)
                {
                    dataList.Add(new SelectListItem { Text = item.Name.ToString(), Value = Convert.ToInt32(item.Id).ToString() });
                }
                return dataList;
            }
        }
    }
}
