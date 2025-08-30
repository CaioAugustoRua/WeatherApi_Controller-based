using Microsoft.AspNetCore.Mvc;

namespace Weather_Api_Proj.Controllers;

/// <summary>
/// Controller responsável por buscar a os dados do clima na API
/// </summary>
[ApiController, Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    [HttpGet]
    public IEnumerable<object> Get()
    {
        throw new NotImplementedException();
    }
}
