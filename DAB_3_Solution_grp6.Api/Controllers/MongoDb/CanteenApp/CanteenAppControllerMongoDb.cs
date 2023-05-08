using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp
{
    [ApiController]
    [Route("api/[controller]")]
    public class MongoCanteensController : ControllerBase
    {
        private readonly MongoDbCanteenAppService _mongoDbCanteenAppService;

        public MongoCanteensController(MongoDbCanteenAppService mongoDbCanteenAppService) =>
            _mongoDbCanteenAppService = mongoDbCanteenAppService;

        [HttpPost]
        public async Task<IActionResult> Post(Canteen newBook)
        {
            await _mongoDbCanteenAppService.CreateAsync(newBook);

            return Ok();
        }

        [HttpGet("GetStaff")]
        public async Task<ActionResult<List<Staff>>> GetStaff(string canteenName)
        {
            var result = await _mongoDbCanteenAppService.GetCanteenStaff(canteenName);

            return Ok(result);
        }


        [HttpGet("GetMenu")]
        public async Task<ActionResult<Menu>> GetMenu(string canteenName)
        {
            var result = await _mongoDbCanteenAppService.GetCanteenMenu(canteenName);

            return Ok(result);
        }


        [HttpGet("GetReservation")]
        public async Task<ActionResult<Menu>> GetReservation(string AuID)
        {
            var result = await _mongoDbCanteenAppService.GetMeals(AuID);

            return Ok(result);
        }





    }
}
