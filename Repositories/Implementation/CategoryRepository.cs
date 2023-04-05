using BusinessEntities.RequestDto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public long Add(CategoryRequest viewModel)
        {
            try
            {
                var response = _context.Database.ExecuteSqlRaw(" execute InsertColorMaster @Name,@Code,@CreatedBy",
                    new SqlParameter("@Name", viewModel.Name),
                    new SqlParameter("@CreatedBy", viewModel.CreatedBy)
                    );

                return response;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }
        public long Update(CategoryRequest viewModel)
        {
            try
            {
                var response = _context.Database.ExecuteSqlRaw(" execute UpdateColorMaster @Id,@Name,@Code,@ModifiedBy,@ModifiedOn",
                    new SqlParameter("@Id", viewModel.Id),
                    new SqlParameter("@Name", viewModel.Name),
                    new SqlParameter("@ModifiedBy", viewModel.ModifiedBy),
                    new SqlParameter("@ModifiedOn", viewModel.ModifiedOn)
                    );

                return response;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }
        public long Delete(long Id)
        {
            try
            {
                var response = _context.Database.ExecuteSqlRaw("Execute DeleteColorMaster @Id",
                 new SqlParameter("@Id", Id)
                );

                return response;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public IEnumerable<DBCategory> GetAll()
        {
            IEnumerable<DBCategory> response = _context.Categories.FromSqlRaw("GetAllColorMaster");
            return response;
        }

        public DBCategory GetById(long Id)
        {
            DBCategory response = _context.Categories.FromSqlRaw("GetColorMasterbyId @Id",
                 new SqlParameter("@Id", Id)
                ).AsEnumerable().FirstOrDefault();

            return response;
        }


    }
}
