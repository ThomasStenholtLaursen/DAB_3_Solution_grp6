using AutoMapper;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query7;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Entities;

namespace DAB_3_Solution_grp6.Api.MapperProfiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<Staff, StaffResponse>();
        }
    }
}
