using System.Text.Json;
using MedicineTracker.Models;
namespace MedicineTracker.Services;

public class MedicineService
{
    private readonly string p = Path.Combine("Data", "medicines.json");
    public List<Medicine> GetAll()
    {
        if (!File.Exists(p))
            return new();
        return JsonSerializer.Deserialize<List<Medicine>>(File.ReadAllText(p)) ??
               new();
    }
    public void Add(Medicine m)
    {
        var list = GetAll();
        m.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
        list.Add(m);
        Save(list);
    }
    public void UpdateQuantity(int id, int qty)
    {
        var list = GetAll();
        var med = list.FirstOrDefault(x => x.Id == id);
        if (med == null)
            return;
        med.Quantity = Math.Max(0, med.Quantity - qty);
        Save(list);
    }
    private void Save(List<Medicine> list)
    {
        File.WriteAllText(
            p, JsonSerializer.Serialize(
                   list, new JsonSerializerOptions { WriteIndented = true }));
    }
}