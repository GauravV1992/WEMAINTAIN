using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BusinessServices.Interface
{
    public interface ICategoryService
    {
        Task<ResultDto<long>> Add(CategoryRequest viewModel);
        Task<ResultDto<long>> Update(CategoryRequest viewModel);
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<CategoryResponse>>> GetAll(CategoryRequest request);
        Task<ResultDto<CategoryResponse>> GetById(long Id);
        Task<ResultDto<IList<SelectListItem>>> GetPackages();
        Task<ResultDto<IEnumerable<CategoryResponse>>> GetPackageSection();
        
    }
}
