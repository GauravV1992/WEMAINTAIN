using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;

namespace BusinessServices.Interface
{
    public interface ICategoryService
    {
        Task<ResultDto<long>> Add(CategoryRequest viewModel);
        Task<ResultDto<long>> Update(CategoryRequest viewModel);
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<CategoryResponse>>> GetAll();
        Task<ResultDto<CategoryResponse>> GetById(long Id);
    }
}
