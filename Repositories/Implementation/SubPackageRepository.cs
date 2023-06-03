using BusinessEntities.RequestDto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessEntities.ResponseDto;

namespace Repositories.Implementation
{
    public class SubPackageRepository : ISubPackageRepository
    {
        private readonly ApplicationDBContext _context;
        public SubPackageRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(SubPackageRequest viewModel)
        {
            var procedureName = "SaveSubPackage";
            var parameters = new DynamicParameters();
            parameters.Add("PackageId", viewModel.PackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Name", viewModel.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("TermsAndCondition", viewModel.TermsAndCondition, DbType.String, ParameterDirection.Input);
            parameters.Add("CreatedBy", viewModel.CreatedBy, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Ext", viewModel.Ext, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QuerySingleAsync<SubPackage>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages.Id;
            }
        }
        public async Task<long> Update(SubPackageRequest viewModel)
        {
            var procedureName = "SaveSubPackage";
            var parameters = new DynamicParameters();
            parameters.Add("Id", viewModel.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Name", viewModel.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("PackageId", viewModel.PackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("TermsAndCondition", viewModel.TermsAndCondition, DbType.String, ParameterDirection.Input);
            parameters.Add("ModifiedBy", viewModel.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Ext", viewModel.Ext, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QuerySingleAsync<SubPackage>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages.Id;
            }
        }
        public async Task<long> Delete(long id)
        {
            var procedureName = "DeleteSubPackage";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QuerySingleAsync<SubPackage>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages.Id;
            }
        }

        public async Task<IEnumerable<SubPackage>> GetAll(SubPackageRequest request)
        {
            var procedureName = "GetAllSubPackage";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<SubPackage>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages;
            }
        }
        public async Task<SubPackage> GetById(long id)
        {
            var procedureName = "GetSubPackageById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryFirstOrDefaultAsync<SubPackage>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages;
            }
        }
        public async Task<IList<SelectListItem>> GetSubPackageNames(long id)
        {
            List<SelectListItem> dataList = new List<SelectListItem>();
            var procedureName = "GetSubPackageNames";
            var parameters = new DynamicParameters();
            parameters.Add("PackageId", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<SubPackage>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);

                dataList.Add(new SelectListItem() { Text = "Select", Value = "" });
                foreach (var item in packages)
                {
                    dataList.Add(new SelectListItem { Text = item.Name.ToString(), Value = Convert.ToInt32(item.Id).ToString() });
                }
                return dataList;
            }
        }
        public async Task<IEnumerable<SubPackage>> GetSubPackageSection(long id)
        {
            var procedureName = "GetSubPackageSection";
            var parameters = new DynamicParameters();
            parameters.Add("PackageId", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<SubPackage>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
        public async Task<SubPackagePriceDetailsResponse> GetSubPackagePriceDetails(long id, string amcPeriod)
        {
            SubPackagePriceDetailsResponse objResp = new SubPackagePriceDetailsResponse();
            var procedureName = "GetSubPackagePriceDetails";
            var parameters = new DynamicParameters();
            parameters.Add("SubPackageId", id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("AMCPeriod", amcPeriod, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(procedureName, parameters, commandType: CommandType.StoredProcedure))
                {
                    objResp.SubPackages = await multi.ReadFirstOrDefaultAsync<SubPackageResponse>();
                    objResp.SubPackageServicePrices = await multi.ReadAsync<SubPackageServicePriceRequest>();
                }
                return objResp;
            }
        }
        public async Task<BillingAndCartDetailsResponse> GetBillingAndCartDetails(CartRequest request)
        {
            BillingAndCartDetailsResponse objResp = new BillingAndCartDetailsResponse();
            var procedureName = "GetBillingAndCartDetails";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", request.CreatedBy, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SubPackageId", request.SubPackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("AMCPeriod", request.AMCPeriod, DbType.String, ParameterDirection.Input);
            parameters.Add("ServicesIds", request.ServicesIds, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(procedureName, parameters, commandType: CommandType.StoredProcedure))
                {
                    objResp.User = await multi.ReadFirstOrDefaultAsync<UserResponse>();
                    objResp.SubPackageServicePrices = await multi.ReadAsync<SubPackageServicePriceRequest>();
                    objResp.TotalDiscount = objResp.SubPackageServicePrices.ToList().Sum(a => a.Discount); objResp.TotalPackageAmount = objResp.SubPackageServicePrices.ToList().Sum(a => a.PackageAmount);
                }
                return objResp;
            }
        }

       
    }
}
