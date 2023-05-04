﻿using BusinessEntities.Common;
using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BusinessServices.Interface
{
    public interface ICountryService
    {
        Task<ResultDto<IList<SelectListItem>>> GetCountryNames();
        
    }
}
