using Application.Schedulers;

namespace Infrastructure.Schedulers;

public class ProductionScheduler: IProductionScheduler
{
    public Task ScheduleProduction(string componentType, int quantity)
    {
        throw new NotImplementedException();
    }
}