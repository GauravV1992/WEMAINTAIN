using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;

namespace BusinessServices.Interface
{
    public interface IRequestLabourService
    {
        Task<ResultDto<IEnumerable<RequestLabourResponse>>> GetAll(RequestLabourRequest request);
        Task<ResultDto<RequestLabourResponse>> GetById(long Id);
    }
}
