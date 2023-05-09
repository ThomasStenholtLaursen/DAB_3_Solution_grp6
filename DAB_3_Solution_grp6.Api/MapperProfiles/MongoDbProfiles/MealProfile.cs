using AutoMapper;
using DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query2;
using DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query4;
using DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query5;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;

namespace DAB_3_Solution_grp6.Api.MapperProfiles.MongoDbProfiles
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<Meal, ReservationForUserMongoDbResponse>();
            CreateMap<Meal, AvailableMealMongoDbResponse>();
            CreateMap<Meal, AvailableNearbyMealsMongoDbResponse>();
        }
    }
}
