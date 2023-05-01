namespace DAB_3_Solution_grp6.MSSQL.DataAccess.Entities
{
    public class Canteen
    {
        public Guid CanteenId { get; }
        public string Name { get; }
        public string Address { get; }
        public string PostalCode { get; }
        public List<Staff> Staff { get; private set; } = new();
        public List<Rating>? Ratings { get; private set; } = new();
        public List<Meal>? Meals { get; private set; } = new();
        public List<Menu>? Menus { get; private set; } = new();

        public Canteen(Guid canteenId, string name, string address, string postalCode)
        {
            CanteenId = canteenId;
            Name = name;
            Address = address;
            PostalCode = postalCode;
        }
    }
}
