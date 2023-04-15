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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<long> Add(UserRequest viewModel)
        {
            var procedureName = "SaveUser";
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", viewModel.FirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("LastName", viewModel.LastName, DbType.String, ParameterDirection.Input);
            parameters.Add("Address", viewModel.Address, DbType.String, ParameterDirection.Input);
            parameters.Add("Email", viewModel.Email, DbType.String, ParameterDirection.Input);
            parameters.Add("MobileNo", viewModel.MobileNo, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleAsync<User>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user.Id;
            }
        }
        public async Task<long> Update(UserRequest viewModel)
        {
            var procedureName = "SaveUser";
            var parameters = new DynamicParameters();
            parameters.Add("Id", viewModel.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("FirstName", viewModel.FirstName, DbType.String, ParameterDirection.Input);
            parameters.Add("LastName", viewModel.LastName, DbType.String, ParameterDirection.Input);
            parameters.Add("Address", viewModel.Address, DbType.String, ParameterDirection.Input);
            parameters.Add("Email", viewModel.Email, DbType.String, ParameterDirection.Input);
            parameters.Add("MobileNo", viewModel.MobileNo, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleAsync<User>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user.Id;
            }
        }
        public async Task<long> Delete(long id)
        {
            var procedureName = "DeleteUser";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleAsync<User>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user.Id;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var procedureName = "GetAllUser";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<User>
           (procedureName, null, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<User> GetById(long id)
        {
            var procedureName = "GetUserById";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<long> CheckUserLogin(UserRequest viewModel)
        {
            var procedureName = "GetUserDetailByMobilenoPassword";
            var parameters = new DynamicParameters();
            parameters.Add("MobileNo", viewModel.MobileNo, DbType.String, ParameterDirection.Input);
            parameters.Add("Password", viewModel.Password, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleAsync<User>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user.Id;
            }
        }

        public async Task<long> ForgetPassword(UserRequest viewModel)
        {
            var procedureName = "UserDetailForgetPassword";
            var parameters = new DynamicParameters();
            parameters.Add("MobileNo", viewModel.MobileNo, DbType.String, ParameterDirection.Input);
            parameters.Add("Password", viewModel.Password, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleAsync<User>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user.Id;
            }
        }

    }
}
