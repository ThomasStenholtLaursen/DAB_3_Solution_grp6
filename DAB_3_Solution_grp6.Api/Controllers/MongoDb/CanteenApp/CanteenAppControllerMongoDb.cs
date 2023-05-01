using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly MongoDbCanteenAppService _mongoDbCanteenAppService;

        public BooksController(MongoDbCanteenAppService mongoDbCanteenAppService) =>
            _mongoDbCanteenAppService = mongoDbCanteenAppService;

        [HttpPost]
        public async Task<IActionResult> Post(Canteen newBook)
        {
            await _mongoDbCanteenAppService.CreateAsync(newBook);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Canteen>> Post(string canteenName)
        {
            var result = await _mongoDbCanteenAppService.GetCanteenStaff(canteenName);

            return Ok(result);
        }
    }
}
