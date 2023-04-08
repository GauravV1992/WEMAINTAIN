using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessServices.Interface
{
    public interface IServiceService
    {
        Task<ResultDto<long>> Add(ServiceRequest viewModel);
        Task<ResultDto<long>> Update(ServiceRequest viewModel);
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<ServiceResponse>>> GetAll();
        Task<ResultDto<ServiceResponse>> GetById(long Id);

        Task<ResultDto<IList<SelectListItem>>> GetServiceNames(long id);
    }
}
