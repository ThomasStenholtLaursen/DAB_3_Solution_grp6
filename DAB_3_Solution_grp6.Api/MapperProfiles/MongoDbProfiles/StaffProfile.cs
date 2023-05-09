using AutoMapper;
using DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query7;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;

namespace DAB_3_Solution_grp6.Api.MapperProfiles.MongoDbProfiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<Staff, StaffMongoDbResponse>()
                .ForMember(x => x.StaffId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
