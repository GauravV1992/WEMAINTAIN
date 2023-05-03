using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BusinessServices.Interface
{
    public interface IBannerService
    {
        Task<ResultDto<long>> Add(BannerRequest viewModel);
       
        Task<ResultDto<long>> Delete(long Id);
        Task<ResultDto<IEnumerable<BannerResponse>>> GetAll(BannerRequest request);
      
        
    }
}
