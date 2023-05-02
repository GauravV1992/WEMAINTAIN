using BusinessEntities.RequestDto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dapper;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Repositories.Implementation
{
    public class JWTAuthenticaitonManagerRepository : IJWTAuthenticaitonManagerRepository
    {
        private readonly ApplicationDBContext _context;
       
        public JWTAuthenticaitonManagerRepository(ApplicationDBContext context)
        {
            _context = context;
            
        }
        public async Task<User> Authentiate(string username, string password)
        {
            var procedureName = "CheckUserLogin";
            var parameters = new DynamicParameters();
            parameters.Add("Username", username, DbType.String, ParameterDirection.Input);
            parameters.Add("password", password, DbType.String, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>
           (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
    }
}
