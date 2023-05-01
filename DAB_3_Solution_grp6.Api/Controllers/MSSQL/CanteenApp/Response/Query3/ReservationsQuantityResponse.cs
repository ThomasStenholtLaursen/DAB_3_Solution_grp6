namespace DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query3
{
    public class ReservationsQuantityResponse
    {
        public WarmDish WarmDish { get; set; } = null!;
        public StreetFood StreetFood { get; set; } = null!;
    }

    public class WarmDish
    {
        public string Name { get; set; } = null!;
        public int? Amount { get; set; }
    }

    public class StreetFood
    {
        public string Name { get; set; } = null!;
        public int? Amount { get; set; }
    }
}
