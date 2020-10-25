using AutoMapper;
using bs.LicensesManager.Core.Model;
using bs.LicensesManager.Core.ViewModel;
using Standard.Licensing;
using System;
using System.Collections.Generic;
using System.Text;

namespace bs.LicensesManager.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerEntity, CustomerViewModel>().ReverseMap();
            CreateMap<ProductEntity, ProductViewModel>().ReverseMap();
            CreateMap<ProductVersionEntity, VersionViewModel>().ReverseMap();
            CreateMap<ProductFeatureEntity, FeatureViewModel>().ReverseMap();
            CreateMap<LicenseTokenFeatureEntity, LicenseTokenFeatureViewModel>().ReverseMap();
            CreateMap<LicenseTokenEntity, LicenseTokenViewModel>().ReverseMap();
            CreateMap<LicenseType, LicenseTypeViewModel>();
                
        }
    }
}
