namespace DAB_3_Solution_grp6.Api.Controllers.CanteenApp.Response.Query6
{
    public class CanteenRatingResponse
    {
        public List<CanteenRating> CanteenRatings { get; set; } = new();
    }

    public class CanteenRating
    {
        public string Name { get; set; } = null!;
        public decimal AvgRating { get; set; }
    }
}
