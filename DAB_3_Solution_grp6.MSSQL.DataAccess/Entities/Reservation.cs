namespace DAB_3_Solution_grp6.MSSQL.DataAccess.Entities
{
    public class Reservation
    {
        public Guid ReservationId { get; }
        public int? WarmQuantity { get; }
        public int? StreetQuantity { get; }
        public DateTime Created { get; }
        public string AuId { get; }
        public Guid MenuId { get; }
        public List<Meal>? Meals { get; private set; } = new();

        public Reservation(Guid reservationId, int? warmQuantity, int? streetQuantity, DateTime created, string auId, Guid menuId)
        {
            ReservationId = reservationId;
            WarmQuantity = warmQuantity;
            StreetQuantity = streetQuantity;
            Created = created;
            AuId = auId;
            MenuId = menuId;
        }
    }
}
