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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(ProductRequest viewModel)
        {
            var procedureName = "SaveProduct";
            var parameters = new DynamicParameters();
            parameters.Add("VendorId", viewModel.VendorId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ProductName", viewModel.ProductName, DbType.String, ParameterDirection.Input);
            parameters.Add("ProductCode", viewModel.ProductCode, DbType.String, ParameterDirection.Input);
            parameters.Add("PackageId", viewModel.PackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SubPackageId", viewModel.SubPackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ProductDescription", viewModel.ProductDescription, DbType.String, ParameterDirection.Input);
            parameters.Add("Price", viewModel.Price, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("GST", viewModel.GST, DbType.String, ParameterDirection.Input);
            parameters.Add("Ext", viewModel.Ext, DbType.String, ParameterDirection.Input);
            parameters.Add("ShowOnHomePage", viewModel.ShowOnHomePage, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("Discount", viewModel.Discount, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("GoldColor", viewModel.GoldColor, DbType.String, ParameterDirection.Input);
            parameters.Add("GoldKT", viewModel.GoldKT, DbType.String, ParameterDirection.Input);
            parameters.Add("GoldWeight", viewModel.GoldWeight, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondClarity", viewModel.DiamondClarity, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondColor", viewModel.DiamondColor, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondWeight", viewModel.DiamondWeight, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondShape", viewModel.DiamondShape, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondSize", viewModel.DiamondSize, DbType.String, ParameterDirection.Input);
            parameters.Add("MakeCountry", viewModel.MakeCountry, DbType.String, ParameterDirection.Input);
            parameters.Add("MaleFemale", viewModel.MaleFemale, DbType.Int16, ParameterDirection.Input);
            parameters.Add("IsActive", viewModel.IsActive, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("CreatedBy", viewModel.CreatedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Products = await connection.QuerySingleAsync<Product>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Products.Id;
            }
        }
        public async Task<long> Update(ProductRequest viewModel)
        {
            var procedureName = "SaveProduct";
            var parameters = new DynamicParameters();
            parameters.Add("Id", viewModel.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("VendorId", viewModel.VendorId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ProductName", viewModel.ProductName, DbType.String, ParameterDirection.Input);
            parameters.Add("ProductCode", viewModel.ProductCode, DbType.String, ParameterDirection.Input);
            parameters.Add("PackageId", viewModel.PackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SubPackageId", viewModel.SubPackageId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ProductDescription", viewModel.ProductDescription, DbType.String, ParameterDirection.Input);
            parameters.Add("Price", viewModel.Price, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("GST", viewModel.GST, DbType.String, ParameterDirection.Input);
            parameters.Add("Ext", viewModel.Ext, DbType.String, ParameterDirection.Input);
            parameters.Add("ShowOnHomePage", viewModel.ShowOnHomePage, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("Discount", viewModel.Discount, DbType.Decimal, ParameterDirection.Input);
            parameters.Add("GoldColor", viewModel.GoldColor, DbType.String, ParameterDirection.Input);
            parameters.Add("GoldKT", viewModel.GoldKT, DbType.String, ParameterDirection.Input);
            parameters.Add("GoldWeight", viewModel.GoldWeight, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondClarity", viewModel.DiamondClarity, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondColor", viewModel.DiamondColor, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondWeight", viewModel.DiamondWeight, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondShape", viewModel.DiamondShape, DbType.String, ParameterDirection.Input);
            parameters.Add("DiamondSize", viewModel.DiamondSize, DbType.String, ParameterDirection.Input);
            parameters.Add("MakeCountry", viewModel.MakeCountry, DbType.String, ParameterDirection.Input);
            parameters.Add("MaleFemale", viewModel.MaleFemale, DbType.Int16, ParameterDirection.Input);
            parameters.Add("IsActive", viewModel.IsActive, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("ModifiedBy", viewModel.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Products = await connection.QuerySingleAsync<Product>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Products.Id;
            }
        }
        public async Task<long> Delete(long id)
        {
            var procedureName = "DeleteProduct";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Products = await connection.QuerySingleAsync<Product>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Products.Id;
            }
        }
        public async Task<IEnumerable<Product>> GetAll(ProductRequest request)
        {
            var procedureName = "GetAllProduct";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<Product>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
        public async Task<Product> GetById(long id)
        {
            var procedureName = "GetProductById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Products = await connection.QueryFirstOrDefaultAsync<Product>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Products;
            }
        }
        public async Task<IList<SelectListItem>> GetVendorName()
        {
            List<SelectListItem> dataList = new List<SelectListItem>();
            var procedureName = "GetVendorNames";
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<Vendor>
           (procedureName, null, commandType: CommandType.StoredProcedure);

                dataList.Add(new SelectListItem() { Text = "Select", Value = "" });
                foreach (var item in packages)
                {
                    dataList.Add(new SelectListItem { Text = item.CompanyName.ToString(), Value = Convert.ToInt32(item.Id).ToString() });
                }
                return dataList;
            }
        }
    }
}
