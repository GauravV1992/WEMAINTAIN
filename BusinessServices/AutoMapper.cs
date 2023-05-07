using BusinessEntities.RequestDto;
using BusinessEntities.ResponseDto;
using BusinessServices.Interface;
using Repositories.Interface;
using AutoMapper.Mappers;
using Repositories;
using BusinessEntities.Common;
using AutoMapper;
//using AutoMapper;

namespace BusinessServices.Automapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //CreateMap<CategoryDto, Category>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name)).ForMember(dest => dest.CategoryId, opt => opt.Ignore());
            CreateMap<Package, CategoryResponse>();
            CreateMap<Service, ServiceResponse>();
            CreateMap<SubPackage, SubPackageResponse>();
            CreateMap<PackageRate, PackageRateResponse>();
            CreateMap<User, LoginResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<Vendor, VendorResponse>();
            CreateMap<Product, ProductResponse>();
        }
    }
}