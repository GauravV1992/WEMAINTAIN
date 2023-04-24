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
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDBContext _context;
        public CouponRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(CouponRequest viewModel)
        {
            var procedureName = "SaveCoupon";
            var parameters = new DynamicParameters();
            parameters.Add("CouponCode", viewModel.CouponCode, DbType.String, ParameterDirection.Input);
            parameters.Add("UserId", viewModel.UserId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("StartDate", viewModel.StartDate, DbType.String, ParameterDirection.Input);
            parameters.Add("EndDate", viewModel.EndDate, DbType.String, ParameterDirection.Input);
            parameters.Add("DiscountPercentage", viewModel.DiscountPercentage, DbType.String, ParameterDirection.Input);
            parameters.Add("CreatedBy", viewModel.CreatedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QuerySingleAsync<Package>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages.Id;
            }
        }
        public async Task<long> Update(CouponRequest viewModel)
        {
            var procedureName = "SaveCoupon";
            var parameters = new DynamicParameters();
            parameters.Add("Id", viewModel.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("CouponCode", viewModel.CouponCode, DbType.String, ParameterDirection.Input);
            parameters.Add("UserId", viewModel.UserId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("StartDate", viewModel.StartDate, DbType.String, ParameterDirection.Input);
            parameters.Add("EndDate", viewModel.EndDate, DbType.String, ParameterDirection.Input);
            parameters.Add("ModifiedBy", viewModel.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            parameters.Add("DiscountPercentage", viewModel.DiscountPercentage, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QuerySingleAsync<Package>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages.Id;
            }
        }
        public async Task<long> Delete(long id)
        {
            var procedureName = "DeleteCoupon";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QuerySingleAsync<Coupon>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return packages.Id;
            }
        }
        public async Task<IEnumerable<Coupon>> GetAll(CouponRequest request)
        {
            var procedureName = "GetAllCoupon";
            var parameters = new DynamicParameters();
            parameters.Add("@PageIndex", request.PageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", request.Length, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var coupons = await connection.QueryAsync<Coupon>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return coupons;
            }
        }
        public async Task<Coupon> GetById(long id)
        {
            var procedureName = "GetCouponById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var coupons = await connection.QueryFirstOrDefaultAsync<Coupon>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return coupons;
            }
        }

        public async Task<IList<SelectListItem>> GetUserNames()
        {
            List<SelectListItem> dataList = new List<SelectListItem>();
            var procedureName = "GetUserName";
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<User>
           (procedureName, null, commandType: CommandType.StoredProcedure);

                dataList.Add(new SelectListItem() { Text = "Select", Value = "" });
                dataList.Add(new SelectListItem() { Text = "All", Value = "0" });
                foreach (var item in packages)
                {
                    dataList.Add(new SelectListItem { Text = item.FirstName + " " + item.LastName + "(" + item.MobileNo + ")", Value = Convert.ToInt32(item.Id).ToString() });
                }
                return dataList;
            }
        }
    }
}
