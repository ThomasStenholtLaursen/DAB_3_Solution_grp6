namespace DAB_3_Solution_grp6.MSSQL.DataAccess.Entities
{
    public class Staff
    {
        public Guid StaffId { get; }
        public Guid CanteenId { get; }
        public string Name { get; }
        public string Title { get; }
        public int Salary { get; }

        public Staff(Guid staffId, Guid canteenId, string name, string title, int salary)
        {
            StaffId = staffId;
            CanteenId = canteenId;
            Name = name;
            Title = title;
            Salary = salary;
        }
    }
}
