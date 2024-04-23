using Domain.Enums;

namespace Domain.Entities;

public class VehicleAssembly
{
    public int Id { get; set; }
    public List<AssemblyItem> AssemblyItems { get; set; }
    public AssemblyStatus Status { get; set; }
}