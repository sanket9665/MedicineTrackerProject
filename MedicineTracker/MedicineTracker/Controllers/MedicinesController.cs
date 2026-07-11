using Microsoft.AspNetCore.Mvc;
using MedicineTracker.Models;
using MedicineTracker.Services;
namespace MedicineTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicinesController : ControllerBase
{
    private readonly MedicineService s;
    public MedicinesController(MedicineService service)
    {
        s = service;
    }
    [HttpGet]
    public IActionResult Get() => Ok(s.GetAll());
    [HttpPost]
    public IActionResult Add([FromBody] Medicine m)
    {
        s.Add(m);
        return Ok(m);
    }
}