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

        public async Task<IEnumerable<Package>> GetAll()
        {
            var procedureName = "GetAllPackage";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<Package>
           (procedureName, null, commandType: CommandType.StoredProcedure);
                return packages;
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


    }
}
