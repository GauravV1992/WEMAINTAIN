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
    public class PurchaseDetailsRepository : IPurchaseDetailsRepository
    {
        private readonly ApplicationDBContext _context;
        public PurchaseDetailsRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(PurchaseDetailsRequest viewModel)
        {
            var procedureName = "SavePurchaseDetails";
            var parameters = new DynamicParameters();
            parameters.Add("PackageId", viewModel.PackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SubPackageId", viewModel.SubPackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ServiceId", viewModel.ServiceId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Rate", viewModel.Rate, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("Discount", viewModel.Discount, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("PackageAmount", viewModel.PackageAmount, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("AMCPeriod", viewModel.AMCPeriod, DbType.String, ParameterDirection.Input);
            parameters.Add("CreatedBy", viewModel.CreatedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var purchaseDetails = await connection.QuerySingleAsync<PurchaseDetails>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return purchaseDetails.Id;
            }
        }

        public async Task<IEnumerable<PurchaseDetails>> GetAll(PurchaseDetailsRequest request)
        {
            var procedureName = "GetAllPurchaseDetails";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<PurchaseDetails>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<PurchaseDetails> GetById(long id)
        {
            var procedureName = "GetPurchaseDetailsById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var purchaseDetails = await connection.QueryFirstOrDefaultAsync<PurchaseDetails>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return purchaseDetails;
            }
        }
 
    }
}
