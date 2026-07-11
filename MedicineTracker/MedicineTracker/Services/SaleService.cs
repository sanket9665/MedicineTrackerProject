using System.Text.Json;
using MedicineTracker.Models;
namespace MedicineTracker.Services;

public class SaleService
{
    private readonly string p = Path.Combine("Data", "sales.json");
    public List<SaleRecord> GetAll()
    {
        if (!File.Exists(p))
            return new();
        return JsonSerializer.Deserialize<List<SaleRecord>>(File.ReadAllText(p)) ??
               new();
    }
    public void Add(SaleRecord r)
    {
        var list = GetAll();
        r.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
        list.Add(r);
        File.WriteAllText(
            p, JsonSerializer.Serialize(
                   list, new JsonSerializerOptions { WriteIndented = true }));
    }
}