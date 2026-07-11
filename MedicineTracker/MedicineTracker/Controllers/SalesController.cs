using Microsoft.AspNetCore.Mvc;
using MedicineTracker.Models;
using MedicineTracker.Services;
namespace MedicineTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly SaleService ss;
    private readonly MedicineService ms;
    public SalesController(SaleService a, MedicineService b)
    {
        ss = a;
        ms = b;
    }
    [HttpGet]
    public IActionResult Get() => Ok(ss.GetAll());
    [HttpPost]
    public IActionResult CreateSale([FromBody] SaleRecord r)
    {
        r.SoldDate = DateTime.Now;
        ss.Add(r);
        ms.UpdateQuantity(r.MedicineId, r.QuantitySold);
        return Ok(r);
    }
}
