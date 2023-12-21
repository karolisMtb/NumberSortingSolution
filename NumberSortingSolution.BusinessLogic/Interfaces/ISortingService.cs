
namespace NumberSortingSolution.BusinessLogic.Interfaces
{
    public interface ISortingService
    {
        Task<IEnumerable<int>> SplitSortAndWriteToFileAsync(List<int> numbers);
        Task<IEnumerable<int>> BubbleSortAndWriteToFileAsync(List<int> numbers);
        IEnumerable<int> SplitSort(List<int> numbers);
        IEnumerable<int> BubbleSort(List<int> numbers);
    }
}
