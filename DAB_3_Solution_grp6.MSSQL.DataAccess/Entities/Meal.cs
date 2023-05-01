namespace DAB_3_Solution_grp6.MSSQL.DataAccess.Entities
{
    public class Meal
    {
        public Guid MealId { get; }
        public string MealName { get; }
        public Guid CanteenId { get; }
        public Guid? ReservationId { get; }
        
        public Meal(Guid mealId, string mealName, Guid canteenId, Guid? reservationId)
        {
            MealId = mealId;
            MealName = mealName;
            CanteenId = canteenId;
            ReservationId = reservationId;
        }
    }
}
