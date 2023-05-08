using AutoMapper;
using DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp.Response.Query7;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanteenAppMongoDbController : ControllerBase
    {
        private readonly MongoDbCanteenAppService _mongoDbCanteenAppService;
        private readonly IMapper _mapper;

        public CanteenAppMongoDbController(MongoDbCanteenAppService mongoDbCanteenAppService, IMapper mapper)
        {
            _mongoDbCanteenAppService = mongoDbCanteenAppService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Canteen newBook)
        {
            await _mongoDbCanteenAppService.CreateAsync(newBook);

            return Ok();
        }


        [HttpGet("GetMenu")]
        public async Task<ActionResult<Menu>> GetMenu(string canteenName)
        {
            var result = await _mongoDbCanteenAppService.GetCanteenMenu(canteenName);

            return Ok(result);
        }


        [HttpGet("Query 2 - Get the reservation for a given customer")]
        public async Task<ActionResult<Meal>> GetReservation(string AuID)
        {
            var result = await _mongoDbCanteenAppService.GetReservationForAGivenCustomer(AuID);

            return Ok(result);
        }

        /// <summary>
        /// Query (7) Payroll of staff for a canteen
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("query7/{canteenName}")]
        public async Task<ActionResult<List<Staff>>> GetStaff(string canteenName)
        {
            var canteens = await _mongoDbCanteenAppService.GetCanteenStaff(canteenName);

            var response = _mapper.Map<List<Staff>, List<StaffResponseMongoDb>>(canteens);
            
            return Ok(response);
        }
    }
}
