using BusinessEntities.RequestDto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IEnumerable<SubPackage>> GetAll()
        {
            var procedureName = "GetAllSubPackage";
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<SubPackage>
           (procedureName, null, commandType: CommandType.StoredProcedure);
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


    }
}
