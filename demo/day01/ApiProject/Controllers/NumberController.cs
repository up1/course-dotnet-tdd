using Microsoft.AspNetCore.Mvc;
using ApiProject.Services;

namespace ApiProject.Controllers;

[ApiController]
[Route("")]
public class NumberController : ControllerBase
{
    private readonly RandomService _randomService;

    public NumberController(RandomService randomService)
    {
        _randomService = randomService;
    }

    [HttpGet("data")]
    public IActionResult GetData()
    {
        var randomNumber = _randomService.Get();
        return Ok(new { data = $"ABC{randomNumber}" });
    }
}
