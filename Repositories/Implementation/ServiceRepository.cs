using BusinessEntities.RequestDto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Repositories.Implementation
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDBContext _context;
        public ServiceRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(ServiceRequest viewModel)
        {
            var procedureName = "SaveService";
            var parameters = new DynamicParameters();
            parameters.Add("Name", viewModel.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("CreatedBy", viewModel.CreatedBy, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Services = await connection.QuerySingleAsync<Service>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Services.Id;
            }
        }
        public async Task<long> Update(ServiceRequest viewModel)
        {
            var procedureName = "SaveService";
            var parameters = new DynamicParameters();
            parameters.Add("Name", viewModel.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("Id", viewModel.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ModifiedBy", viewModel.ModifiedBy, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Name", viewModel.Name, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Services = await connection.QuerySingleAsync<Service>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Services.Id;
            }
        }
        public async Task<long> Delete(long id)
        {
            var procedureName = "DeleteService";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Services = await connection.QuerySingleAsync<Service>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Services.Id;
            }
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            var procedureName = "GetAllService";
            using (var connection = _context.CreateConnection())
            {
                var Services = await connection.QueryAsync<Service>
           (procedureName, null, commandType: CommandType.StoredProcedure);
                return Services;
            }
        }
        public async Task<Service> GetById(long id)
        {
            var procedureName = "GetServiceById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var Services = await connection.QueryFirstOrDefaultAsync<Service>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return Services;
            }
        }
        public async Task<IList<SelectListItem>> GetServiceNames(long id)
        {
            List<SelectListItem> dataList = new List<SelectListItem>();
            var procedureName = "GetServiceNames";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<Service>
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
