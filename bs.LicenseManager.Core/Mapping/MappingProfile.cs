using AutoMapper;
using bs.LicensesManager.Core.Model;
using bs.LicensesManager.Core.ViewModel;
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
        }
    }
}
