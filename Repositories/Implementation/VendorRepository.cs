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
    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationDBContext _context;
        public VendorRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(VendorRequest viewModel)
        {
            var procedureName = "SaveVendor";
            var parameters = new DynamicParameters();
            parameters.Add("CompanyName", viewModel.CompanyName, DbType.String, ParameterDirection.Input);
            parameters.Add("FirstName", viewModel.FirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("LastName", viewModel.LastName, DbType.String, ParameterDirection.Input);
            parameters.Add("Address", viewModel.Address, DbType.String, ParameterDirection.Input);
            parameters.Add("Country", viewModel.Country, DbType.Int32, ParameterDirection.Input);
            parameters.Add("State", viewModel.State, DbType.Int32, ParameterDirection.Input);
            parameters.Add("City", viewModel.City, DbType.String, ParameterDirection.Input);
            parameters.Add("GST", viewModel.GST, DbType.String, ParameterDirection.Input);
            parameters.Add("Ext", viewModel.Ext, DbType.String, ParameterDirection.Input);
            parameters.Add("Pincode", viewModel.Pincode, DbType.String, ParameterDirection.Input);
            parameters.Add("EmailAddress", viewModel.EmailAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("Phone", viewModel.Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("CreatedBy", viewModel.CreatedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Vendors = await connection.QuerySingleAsync<Vendor>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Vendors.Id;
            }
        }
        public async Task<long> Update(VendorRequest viewModel)
        {
            var procedureName = "SaveVendor";
            var parameters = new DynamicParameters();
            parameters.Add("CompanyName", viewModel.CompanyName, DbType.String, ParameterDirection.Input);
            parameters.Add("FirstName", viewModel.FirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("LastName", viewModel.LastName, DbType.String, ParameterDirection.Input);
            parameters.Add("Address", viewModel.Address, DbType.String, ParameterDirection.Input);
            parameters.Add("Country", viewModel.Country, DbType.Int32, ParameterDirection.Input);
            parameters.Add("State", viewModel.State, DbType.Int32, ParameterDirection.Input);
            parameters.Add("City", viewModel.City, DbType.String, ParameterDirection.Input);
            parameters.Add("GST", viewModel.GST, DbType.String, ParameterDirection.Input);
            parameters.Add("Ext", viewModel.Ext, DbType.String, ParameterDirection.Input);
            parameters.Add("Pincode", viewModel.Pincode, DbType.String, ParameterDirection.Input);
            parameters.Add("EmailAddress", viewModel.EmailAddress, DbType.String, ParameterDirection.Input);
            parameters.Add("Phone", viewModel.Phone, DbType.String, ParameterDirection.Input);
            parameters.Add("Id", viewModel.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ModifiedBy", viewModel.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Vendors = await connection.QuerySingleAsync<Vendor>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Vendors.Id;
            }
        }
        public async Task<long> Delete(long id)
        {
            var procedureName = "DeleteVendor";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Vendors = await connection.QuerySingleAsync<Vendor>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Vendors.Id;
            }
        }
        public async Task<IEnumerable<Vendor>> GetAll(VendorRequest request)
        {
            var procedureName = "GetAllVendor";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<Vendor>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
        public async Task<Vendor> GetById(long id)
        {
            var procedureName = "GetVendorById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Vendors = await connection.QueryFirstOrDefaultAsync<Vendor>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Vendors;
            }
        }
    }
}
