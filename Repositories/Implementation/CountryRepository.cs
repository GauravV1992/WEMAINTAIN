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
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDBContext _context;
        public CountryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IList<SelectListItem>> GetCountryNames()
        {
            List<SelectListItem> dataList = new List<SelectListItem>();
            var procedureName = "GetCountryNames";
            using (var connection = _context.CreateConnection())
            {
                var packages = await connection.QueryAsync<Country>
           (procedureName, null, commandType: CommandType.StoredProcedure);

                dataList.Add(new SelectListItem() { Text = "Select", Value = "" });
                foreach (var item in packages)
                {
                    dataList.Add(new SelectListItem { Text = item.CountryName.ToString(), Value = Convert.ToInt32(item.Id).ToString() });
                }
                return dataList;
            }
        }
        
    }
}
