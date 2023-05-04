using BusinessEntities.RequestDto;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Repositories.Interface
{
    public interface ICountryRepository
    {
        Task<IList<SelectListItem>> GetCountryNames();
    }
}
