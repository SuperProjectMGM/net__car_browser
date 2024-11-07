using Microsoft.AspNetCore.Mvc;
using search.api.Interfaces;
using search.api.Mappers;

namespace search.api.Controllers
{
    [Route("search.api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchInterface _service;

        public SearchController(ISearchInterface service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDefault()
        {
            SearchInfo info = await _service.Search();
            var cars = info.Vehicles;
            var dto = cars.Select(veh => veh.VehicletoVehicleOurDto());
            return Ok(dto);
        }
    }
}