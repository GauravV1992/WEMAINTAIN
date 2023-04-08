using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;

namespace BusinessServices.Interface
{
    public interface IUserService
    {
        Task<ResultDto<long>> Add(UserRequest viewModel);
        Task<ResultDto<long>> Update(UserRequest viewModel);
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<UserResponse>>> GetAll();
        Task<ResultDto<UserResponse>> GetById(long Id);
    }
}
