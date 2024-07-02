using HahnTestSol.Server.Services.Legacy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HahnTestSol.Server.Controllers.Legacy
{
    [ApiController]
    [Route("[controller]")]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _cargoService;

        public CargoController(CargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpGet("nodes")]
        public async Task<IActionResult> GetNodes()
        {
            var nodes = await _cargoService.GetNodesAsync();
            return Ok(nodes);
        }

        // Other endpoints...
    }

}
