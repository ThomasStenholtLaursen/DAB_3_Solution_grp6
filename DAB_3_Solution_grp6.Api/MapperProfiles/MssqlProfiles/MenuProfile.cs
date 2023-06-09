﻿using AutoMapper;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query1;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Entities;

namespace DAB_3_Solution_grp6.Api.MapperProfiles.MssqlProfiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, DailyMenuResponse>()
                .ForMember(x => x.Warm, opt => opt.MapFrom(src => src.WarmDishName))
                .ForMember(x => x.Street, opt => opt.MapFrom(src => src.StreetFoodName));
        }
    }
}
