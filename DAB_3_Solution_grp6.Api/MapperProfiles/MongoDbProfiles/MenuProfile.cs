using AutoMapper;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query1;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;

namespace DAB_3_Solution_grp6.Api.MapperProfiles.MongoDbProfiles
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
