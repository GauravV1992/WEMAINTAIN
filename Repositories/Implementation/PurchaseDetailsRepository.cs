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
            using (var connection = _context.CreateConnection())
            {
                var purchaseDetails = await connection.QuerySingleAsync<PurchaseDetails>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return purchaseDetails.Id;
            }
        }

        public async Task<IEnumerable<PurchaseDetails>> GetAll()
        {
            var procedureName = "GetAllPurchaseDetails";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var purchaseDetails = await connection.QueryAsync<PurchaseDetails>
           (procedureName, null, commandType: CommandType.StoredProcedure);
                return purchaseDetails;
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
