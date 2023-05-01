namespace DAB_3_Solution_grp6.MSSQL.DataAccess.Entities
{
    public class Customer
    {
        public string AuId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public List<Rating>? Ratings { get; private set; } = new();
        public List<Reservation>? Reservations { get; private set; } = new();

        public Customer(string auId, string firstName, string lastName)
        {
            AuId = auId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
