namespace Application.Schedulers;

public interface IProductionScheduler
{
    Task ScheduleProduction(string componentType, int quantity);
}