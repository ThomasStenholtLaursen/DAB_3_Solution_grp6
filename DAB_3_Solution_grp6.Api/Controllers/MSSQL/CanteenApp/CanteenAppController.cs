using AutoMapper;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query1;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query2;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query3;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query4;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query5;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query6;
using DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp.Response.Query7;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Entities;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Exceptions;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Repositories.Canteen;
using DAB_3_Solution_grp6.MSSQL.DataAccess.Repositories.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace DAB_3_Solution_grp6.Api.Controllers.MSSQL.CanteenApp
{
    [ApiController]
    [Route("api")]
    public class CanteenAppController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICanteenRepository _canteenRepository;

        public CanteenAppController(
            IMapper mapper,
            IReservationRepository reservationRepository,
            ICanteenRepository canteenRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _canteenRepository = canteenRepository;
        }

        /// <summary>
        /// Query (1) Gets the day's menu options for a canteen given as input
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("query1/{canteenName}")]
        public async Task<ActionResult> GetDailyMenuOptions(string canteenName)
        {
            try
            {
                var canteen = await _canteenRepository.GetCanteenWithMenusByNameAsync(canteenName);

                var menu = canteen.Menus!.FirstOrDefault(menu => menu.Created.Date == DateTime.Today);

                var response = _mapper.Map<DailyMenuResponse>(menu);

                return Ok(response);
            }
            catch (CanteenNotFoundException)
            {
                return NotFound(canteenName);
            }
        }

        /// <summary>
        /// Query (2) Get the reservation for a given customer
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("query2/{auId}")]
        public async Task<ActionResult> GetReservationById(string auId)
        {
            try
            {
                var reservations = await _reservationRepository.GetReservationsById(auId);

                var canteens = await _canteenRepository.GetAllCanteens();

                var response = new List<ReservationForUserResponse>();

                foreach (var reservation in reservations)
                {
                    var mealReservationDescriptions = reservation.Meals?.Select(x => new MealReservationDescription
                    {
                        MealId = x.MealId,
                        MealName = x.MealName,
                    }).ToList();

                    var canteen = canteens.FirstOrDefault(x => x.CanteenId == reservation.Meals![0].CanteenId);

                    response.Add(new ReservationForUserResponse
                    {
                        CanteenName = canteen!.Name,
                        MealReservations = mealReservationDescriptions
                    });
                }

                return Ok(response);
            }
            catch (ReservationNotFoundException)
            {
                return NotFound(auId);
            }
        }

        /// <summary>
        /// Query (3) Number of reservations for each of the daily menu options for a canteen
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("query3/{canteenName}")]
        public async Task<ActionResult> GetReservationsQuantity(string canteenName)
        {
            try
            {
                var canteen = await _canteenRepository.GetCanteenWithMenusAndReservationsByNameAsync(canteenName);

                if (canteen.Menus == null)
                    return NotFound($"Could not find any menus for '{canteenName}'");

                var menu = canteen.Menus.FirstOrDefault();

                var response = new ReservationsQuantityResponse
                {
                    WarmDish = new WarmDish
                    {
                        Amount = menu!.Reservations!.Sum(reservation => reservation.WarmQuantity ?? 0),
                        Name = menu!.WarmDishName
                    },
                    StreetFood = new StreetFood
                    {
                        Amount = menu!.Reservations!.Sum(reservation => reservation.StreetQuantity ?? 0),
                        Name = menu!.StreetFoodName
                    }
                };

                return Ok(response);
            }
            catch (CanteenNotFoundException)
            {
                return NotFound(canteenName);
            }
        }

        /// <summary>
        /// Query (4) get the available (canceled) meals from the daily menu for a canteen
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("query4/{canteenName}")]
        public async Task<ActionResult> GetAvailableMealsForCanteen(string canteenName)
        {
            try
            {
                var canteen = await _canteenRepository.GetCanteenWithMealsByNameAsync(canteenName);

                var canceledMeals = canteen.Meals!.Where(meal => meal.ReservationId == null).ToList();

                var response = new AvailableMealsResponse
                {
                    CanceledMeals = _mapper.Map<List<Meal>, List<SimpleMeal>>(canceledMeals),
                };

                return Ok(response);
            }
            catch (CanteenNotFoundException)
            {
                return BadRequest(canteenName);
            }
        }

        /// <summary>
        /// Query (5) available (canceled) daily menu in the nearby canteens
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("query5/{canteenName}")]
        public async Task<ActionResult> GetAvailableMealsInNearbyCanteen(string canteenName)
        {
            try
            {
                var nearbyCanteens = await _canteenRepository.GetNearbyCanteenMealsByNameAsync(canteenName);

                var nearbyCanceledMeals = nearbyCanteens.SelectMany(canteen => canteen.Meals!).Where(meal => meal.ReservationId == null).ToList();

                var response = new AvailableNearbyMealsResponse
                {
                    NearbyMeals = nearbyCanceledMeals.Select(meal =>
                        new NearbyMeal
                        {
                            CanteenName = nearbyCanteens.FirstOrDefault(canteen => canteen.CanteenId == meal.CanteenId)?.Name!,
                            MealName = meal.MealName
                        }).ToList()
                };

                return Ok(response);
            }
            catch (CanteenNotFoundException)
            {
                return BadRequest(canteenName);
            }
        }

        /// <summary>
        /// Query (6) average ratings from all the canteens from top to bottom
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("query6")]
        public async Task<ActionResult> GetAverageRatingForCanteens()
        {
            try
            {
                var canteens = await _canteenRepository.GetAllCanteensWithRatingsAsync();

                var canteenRatingResponse = new CanteenRatingResponse
                {
                    CanteenRatings = canteens.Select(canteen => new CanteenRating
                    {
                        Name = canteen.Name,
                        AvgRating = canteen.Ratings?.Any() == true ? Math.Round(canteen.Ratings.Average(rating => rating.Stars), 1) : 0
                    })
                        .OrderByDescending(canteenRating => canteenRating.AvgRating)
                        .ToList()
                };

                return Ok(canteenRatingResponse);
            }
            catch (CanteenNotFoundException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Query (7) Payroll of staff for a canteen
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("query7/{canteenName}")]
        public async Task<ActionResult> GetStaffForCanteen(string canteenName)
        {
            try
            {
                var canteens = await _canteenRepository.GetAllCanteenWithStaffByNameAsync(canteenName);

                var response = _mapper.Map<List<Staff>, List<StaffResponse>>(canteens.Staff);

                return Ok(response);
            }
            catch (CanteenNotFoundException)
            {
                return BadRequest();
            }
        }
    }
}
