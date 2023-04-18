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
using BusinessEntities.ResponseDto;

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
            //parameters.Add("ServiceId", viewModel.ServiceId, DbType.Int32, ParameterDirection.Input);
            //parameters.Add("Rate", viewModel.Rate, DbType.Decimal, ParameterDirection.Input);
            //parameters.Add("Discount", viewModel.Discount, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("PackageAmount", viewModel.PackageAmount, DbType.Decimal, ParameterDirection.Input);
            //parameters.Add("AMCPeriod", viewModel.AMCPeriod, DbType.String, ParameterDirection.Input);
            parameters.Add("UserId", viewModel.CreatedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        var purchaseDetails = await connection.QuerySingleAsync<PurchaseDetails>
           (procedureName, parameters, tran, commandType: CommandType.StoredProcedure);
                        tran.Commit();
                        return purchaseDetails.Id;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<PurchaseDetailsWithServicesResponse> GetAll(PurchaseDetailsRequest request)
        {
            var procedureName = "GetAllPurchaseDetails1";
            var parameters = new DynamicParameters();
            PurchaseDetailsWithServicesResponse objRes = new PurchaseDetailsWithServicesResponse();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(procedureName, parameters, commandType: CommandType.StoredProcedure))
                {
                    objRes.PurchaseDetails = await multi.ReadAsync<PurchaseDetailsResponse>();
                    objRes.PurchaseServices = await multi.ReadAsync<PurchaseServicesResponse>();
                }
                //     var user = await connection.QueryMultipleAsync<PurchaseDetails>
                //(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return objRes;
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

        public async Task<IEnumerable<PurchaseServices>> PurchaseServicesById(long id)
        {
            var procedureName = "GetPurchaseServicesById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var purchaseDetails = await connection.QueryAsync<PurchaseServices>
          (procedureName, parameters, commandType: CommandType.StoredProcedure);

                return purchaseDetails;
            }
        }
    }
}
