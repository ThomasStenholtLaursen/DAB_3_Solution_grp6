namespace DAB_3_Solution_grp6.MSSQL.DataAccess.Entities
{
    public class Menu
    {
        public Guid MenuId { get; }
        public string WarmDishName { get; }
        public string StreetFoodName { get; }
        public DateTime Created { get; }
        public Guid CanteenId { get; }
        public List<Reservation>? Reservations { get; private set; } = new();

        public Menu(Guid menuId, string warmDishName, string streetFoodName, DateTime created, Guid canteenId)
        {
            MenuId = menuId;
            WarmDishName = warmDishName;
            StreetFoodName = streetFoodName;
            Created = created;
            CanteenId = canteenId;
        }
    }
}
