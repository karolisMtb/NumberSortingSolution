using NumberSortingSolution.BusinessLogic.Interfaces;
using NumberSortingSolution.DataAccess.Interfaces;

namespace NumberSortingSolution.BusinessLogic.Services
{
    public class SortingService : ISortingService
    {
        private readonly IFileRepository _sortingRepository;
        public SortingService(IFileRepository sortingRepository)
        {
            _sortingRepository = sortingRepository;
        }

        public async Task<IEnumerable<int>> SplitSortAndWriteToFileAsync(List<int> numbers)
        {
            IEnumerable<int> sortedList = SplitSort(numbers);

            await _sortingRepository.SaveToFileAsync(sortedList);

            return sortedList;
        }

        public IEnumerable<int> SplitSort(List<int> numbers)
        {
            int[] array = numbers.ToArray();

            for (int interval = numbers.Count / 2; interval > 0; interval /= 2)
            {
                for (int index = interval; index < numbers.Count; index++)
                {
                    var currentElement = array[index];
                    var temp = index;

                    while (temp >= interval && array[temp - interval] > currentElement)
                    {
                        array[temp] = array[temp - interval];
                        temp -= interval;
                    }

                    array[temp] = currentElement;
                }
            }

            return array.ToList();
        }

        public async Task<IEnumerable<int>> BubbleSortAndWriteToFileAsync(List<int> numbers)
        {
            IEnumerable<int> sortedList = BubbleSort(numbers);

            await _sortingRepository.SaveToFileAsync(sortedList);

            return sortedList;
        }

        public IEnumerable<int> BubbleSort(List<int> numbers)
        {
            var n = numbers.Count;
            bool swapRequired;

            for (int i = 0; i < n - 1; i++)
            {
                swapRequired = false;

                for (int j = 0; j < n - i - 1; j++)

                    if (numbers[j] > numbers[j + 1])
                    {
                        var tempVar = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = tempVar;
                        swapRequired = true;
                    }

                if (swapRequired == false)
                    break;
            }

            return numbers.ToList();
        }
    }
}
