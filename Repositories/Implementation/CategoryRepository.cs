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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        //public long Add(CategoryRequest viewModel)
        //{
        //    try
        //    {
        //        var response = _context.Database.ExecuteSqlRaw(" execute InsertColorMaster @Name,@Code,@CreatedBy",
        //            new SqlParameter("@Name", viewModel.Name),
        //            new SqlParameter("@CreatedBy", viewModel.CreatedBy)
        //            );

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return 0;
        //}
        //public long Update(CategoryRequest viewModel)
        //{
        //    try
        //    {
        //        var response = _context.Database.ExecuteSqlRaw(" execute UpdateColorMaster @Id,@Name,@Code,@ModifiedBy,@ModifiedOn",
        //            new SqlParameter("@Id", viewModel.Id),
        //            new SqlParameter("@Name", viewModel.Name),
        //            new SqlParameter("@ModifiedBy", viewModel.ModifiedBy),
        //            new SqlParameter("@ModifiedOn", viewModel.ModifiedOn)
        //            );

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return 0;
        //}
        //public long Delete(long Id)
        //{
        //    try
        //    {
        //        var response = _context.Database.ExecuteSqlRaw("Execute DeleteColorMaster @Id",
        //         new SqlParameter("@Id", Id)
        //        );

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return 0;
        //}

        public async Task<IEnumerable<Package>> GetAll()
        {
            var procedureName = "GetAllPackage";
            //var parameters = new DynamicParameters();
            //parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<Package>
           (procedureName, null, commandType: CommandType.StoredProcedure);
                return packages;
            }
        }

        //public Package GetById(long Id)
        //{
        //    Package response = _context.Packages.FromSqlRaw("GetColorMasterbyId @Id",
        //         new SqlParameter("@Id", Id)
        //        ).AsEnumerable().FirstOrDefault();

        //    return response;
        //}


    }
}
