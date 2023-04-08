﻿using BusinessEntities.RequestDto;
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
    public class PackageRateRepository : IPackageRateRepository
    {
        private readonly ApplicationDBContext _context;
        public PackageRateRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(PackageRateRequest viewModel)
        {
            var procedureName = "SavePackageRate";
            var parameters = new DynamicParameters();
            parameters.Add("PackageId", viewModel.PackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SubPackageId", viewModel.SubPackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ServiceId", viewModel.ServiceId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Rate", viewModel.Rate, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("Discount", viewModel.Discount, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("PackageAmount", viewModel.PackageAmount, DbType.Decimal, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var PackageRate = await connection.QuerySingleAsync<PackageRate>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return PackageRate.Id;
            }
        }
        public async Task<long> Update(PackageRateRequest viewModel)
        {
            var procedureName = "SavePackageRate";
            var parameters = new DynamicParameters();
            parameters.Add("Id", viewModel.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("PackageId", viewModel.PackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SubPackageId", viewModel.SubPackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ServiceId", viewModel.ServiceId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Rate", viewModel.Rate, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("Discount", viewModel.Discount, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("PackageAmount", viewModel.PackageAmount, DbType.Decimal, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var PackageRate = await connection.QuerySingleAsync<PackageRate>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return PackageRate.Id;
            }
        }
        public async Task<long> Delete(long id)
        {
            var procedureName = "DeletePackageRate";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var PackageRate = await connection.QuerySingleAsync<PackageRate>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return PackageRate.Id;
            }
        }

        public async Task<IEnumerable<PackageRate>> GetAll()
        {
            var procedureName = "GetAllPackageRate";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var PackageRate = await connection.QueryAsync<PackageRate>
           (procedureName, null, commandType: CommandType.StoredProcedure);
                return PackageRate;
            }
        }

        public async Task<PackageRate> GetById(long id)
        {
            var procedureName = "GetPackageRateById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var PackageRate = await connection.QueryFirstOrDefaultAsync<PackageRate>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return PackageRate;
            }
        }
 
    }
}