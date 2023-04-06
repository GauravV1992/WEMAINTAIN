using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;

namespace BusinessServices.Interface
{
    public interface ICategoryService
    {
        //ResultDto<long> Add(CategoryRequest viewModel);
        //ResultDto<long> Update(CategoryRequest viewModel);
        //ResultDto<long> Delete(long Id);
        Task<ResultDto<IEnumerable<CategoryResponse>>> GetAll();
        //ResultDto<CategoryResponse> GetById(long Id);
    }
}
