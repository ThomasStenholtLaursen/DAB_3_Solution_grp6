using AutoMapper;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query4;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Entities;

namespace DAB_3_Solution_grp6.Api.MapperProfiles
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<Meal, SimpleMeal>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.MealName));
        }
    }
}
