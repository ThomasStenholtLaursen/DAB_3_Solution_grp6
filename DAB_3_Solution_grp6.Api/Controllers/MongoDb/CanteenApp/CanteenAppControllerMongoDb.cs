using DAB_3_Solution_grp6.MongoDb.DataAccess.Models;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace DAB_3_Solution_grp6.Api.Controllers.MongoDb.CanteenApp
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly CanteenService _canteenService;

        public BooksController(CanteenService canteenService) =>
            _canteenService = canteenService;

        [HttpPost]
        public async Task<IActionResult> Post(Canteen newBook)
        {
            await _canteenService.CreateAsync(newBook);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Canteen>> Post(string canteenName)
        {
            var result = await _canteenService.GetCanteenStaff(canteenName);

            return Ok(result);
        }
    }
}
