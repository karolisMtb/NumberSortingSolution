using NumberSortingSolution.BusinessLogic.Interfaces;
using System.Diagnostics;

namespace NumberSortingSolution.BusinessLogic.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly ISortingService _sortingService;
        public PerformanceService(ISortingService sortingService)
        {
            _sortingService = sortingService;
        }

        public IEnumerable<string> MeasureAlgorithmPerformance(List<int> randomNumbers) 
        {
            List<string> performanceResult = new List<string>();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            _sortingService.SplitSort(randomNumbers);
            stopwatch.Stop();
            TimeSpan splitExecutionTime = stopwatch.Elapsed;

            stopwatch.Start();
            _sortingService.BubbleSort(randomNumbers);
            stopwatch.Stop();
            TimeSpan bubbleExecutionTime = stopwatch.Elapsed;

            performanceResult.Add($"Split sorting algorithm finished sorting in {splitExecutionTime} seconds.");
            performanceResult.Add($"Bubble sorting algorithm finished sorting in {bubbleExecutionTime} seconds.");

            return performanceResult;
        }
    }
}
