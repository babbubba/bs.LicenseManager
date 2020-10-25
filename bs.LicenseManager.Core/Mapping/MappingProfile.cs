using AutoMapper;
using bs.LicenseManager.Core.Model;
using bs.LicenseManager.Core.ViewModel;
using Standard.Licensing;
using System;
using System.Collections.Generic;
using System.Text;

namespace bs.LicenseManager.Core.Mapping
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
