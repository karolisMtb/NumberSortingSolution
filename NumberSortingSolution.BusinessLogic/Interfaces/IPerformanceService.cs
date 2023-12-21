
namespace NumberSortingSolution.BusinessLogic.Interfaces
{
    public interface IPerformanceService
    {
        IEnumerable<string> MeasureAlgorithmPerformance(List<int> randomNumbers);
    }
}
