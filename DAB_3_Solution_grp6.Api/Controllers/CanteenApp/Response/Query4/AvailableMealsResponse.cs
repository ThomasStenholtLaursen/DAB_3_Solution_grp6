namespace DAB_3_Solution_grp6.Api.Controllers.CanteenApp.Response.Query4
{
    public class AvailableMealsResponse
    {
        public List<SimpleMeal> CanceledMeals { get; set; } = new();
    }
    public class SimpleMeal
    {
        public string Name { get; set; } = null!;
    }
}
