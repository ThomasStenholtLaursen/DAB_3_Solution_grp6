using DAB_3_Solution_grp6.Api.Seed;
using DAB_3_Solution_grp6.MongoDb.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace DAB_3_Solution_grp6.Api.Controllers.MongoDb.Reset
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResetMongoDbController : ControllerBase
    {
        private readonly CanteenAppMongoDbService _canteenAppMongoDbService;
        private readonly MongoDbDataSeed _mongoDbSeed;

        public ResetMongoDbController(CanteenAppMongoDbService canteenAppMongoDbService, MongoDbDataSeed mongoDbSeed)
        {
            _canteenAppMongoDbService = canteenAppMongoDbService;
            _mongoDbSeed = mongoDbSeed;
        }

        /// <summary>
        /// Seed Database (if no data exists)
        /// </summary>
        [HttpPost("Seed")]
        public async Task<ActionResult> SeedDatabase()
        {
            await _mongoDbSeed.SeedDataMongoDb(_canteenAppMongoDbService);

            return Ok();
        }

        /// <summary>
        /// Clear MongoDb (REMOVE ALL DOCUMENTS IN COLLECTIONS)
        /// </summary>
        [HttpDelete("Clear")]
        public async Task<ActionResult> ClearDatabase()
        {
            await _canteenAppMongoDbService.RemoveAll();

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
