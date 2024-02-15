

using Drivers.Api.Models;
using Drivers.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    private readonly ILogger<DriversController> _logger;
    private readonly DriverServices _driverService;

    public DriversController(
        ILogger<DriversController> logger,
        DriverServices driverServices)
        {
            _logger = logger;
            _driverService= driverServices;
        }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var drivers = await _driverService.GetAsync();
        return Ok(drivers);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetDriversById(string id)
    {
        return Ok(await _driverService.GetDriverId(id));
    }

    [HttpPost]

    public async Task<IActionResult> CreateDriver([FromBody] Driver driver)//CHECAR EL DRIVER SI ES ASI O EL DIVE
    {
        if(driver == null)
        {
            return BadRequest();
        }
        if(driver.name == string.Empty)
        {
            ModelState.AddModelError("Name", "El Driver no debe estar vacio");
        }

        await _driverService.InsertDriver(driver);
        return Created("Created", true);

    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDriver([FromBody] Driver driver, string id)
    {
        if (driver==null)
        {
            return BadRequest();
        }
        if (driver.name == string.Empty)
        {
            ModelState.AddModelError("Name","El driver no deberia estar vacio");
        }
        //OBTIENES EL VALOR DEL ID
        driver.Id = id;

        await _driverService.UpdateDriver(driver);
        return Created("Created",true);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriver(string id)
    {
        await _driverService.DeleteDriver(id);

        return NoContent(); //TODO SALIO FAIN
    }

  


}