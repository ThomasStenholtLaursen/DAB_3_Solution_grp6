using AutoMapper;
using DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query2;
using DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query4;
using DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query5;
using DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query7;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query1;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanteenAppMongoDbController : ControllerBase
    {
        private readonly CanteenAppMongoDbService _canteenAppMongoDbService;
        private readonly IMapper _mapper;

        public CanteenAppMongoDbController(CanteenAppMongoDbService canteenAppMongoDbService, IMapper mapper)
        {
            _canteenAppMongoDbService = canteenAppMongoDbService;
            _mapper = mapper;
        }

        /// <summary>
        /// Query (1) Gets the day's menu options for a canteen given as input
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("query1/{canteenName}")]
        public async Task<ActionResult> GetDailyMenuOptions(string canteenName)
        {
            var menu = await _canteenAppMongoDbService.GetMenuForCanteen(canteenName);

            var response = _mapper.Map<Menu, DailyMenuResponse>(menu);

            return Ok(response);
        }

        /// <summary>
        /// Query (2) Get the reservations for a given customer (can be multiple)
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("query2/{auId}")]
        public async Task<ActionResult<List<Meal>>> GetReservation(string auId)
        {
            var meals = await _canteenAppMongoDbService.GetReservationsForAGivenCustomer(auId);

            var response = _mapper.Map<List<Meal>, List<ReservationForUserMongoDbResponse>>(meals);

            return Ok(response);
        }

        /// <summary>
        /// Query (4) get the available (canceled) meals from the daily menu for a canteen
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("query4/{canteenName}")]
        public async Task<ActionResult<List<Meal>>> GetAvailableMealsForCanteen(string canteenName)
        {
            var meals = await _canteenAppMongoDbService.GetCanceledMealsForCanteen(canteenName);

            var response = _mapper.Map<List<Meal>, List<AvailableMealMongoDbResponse>>(meals);

            return Ok(response);
        }

        /// <summary>
        /// Query (5) available (canceled) daily menu in the nearby canteens
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("query5/{canteenName}")]
        public async Task<ActionResult> GetAvailableMealsInNearbyCanteen(string canteenName)
        {
            var canceledMealsInNearbyCanteens =
                await _canteenAppMongoDbService.GetCanceledMealsInNearbyCanteenForCanteen(canteenName);

            var response =
                _mapper.Map<List<Meal>, List<AvailableNearbyMealsMongoDbResponse>>(canceledMealsInNearbyCanteens);

            return Ok(response);
        }

        /// <summary>
        /// Query (6) average ratings from all the canteens from top to bottom
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("query6")]
        public async Task<ActionResult> GetAverageRatingForCanteens()
        {
            var ratings = await _canteenAppMongoDbService.GetAllRatings();

            var canteenRatings = ratings
                .GroupBy(r => r.CanteenName)
                .Select(y => new
                {
                    CanteenName = y.Key,
                    AverageRating = Math.Round(y.Average(r => r.Stars), 1)
                })
                .OrderByDescending(r => r.AverageRating)
                .ToList();

            return Ok(canteenRatings);
        }

        /// <summary>
        /// Query (7) Payroll of staff for a canteen
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("query7/{canteenName}")]
        public async Task<ActionResult<List<StaffMongoDbResponse>>> GetStaff(string canteenName)
        {
            var canteens = await _canteenAppMongoDbService.GetCanteenStaff(canteenName);

            var response = _mapper.Map<List<Staff>, List<StaffMongoDbResponse>>(canteens);

            return Ok(response);
        }
    }
}
