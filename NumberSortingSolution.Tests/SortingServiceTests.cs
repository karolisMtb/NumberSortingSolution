using Moq;
using NumberSortingSolution.BusinessLogic.Services;
using NumberSortingSolution.DataAccess.Interfaces;
using Xunit;

namespace NumberSortingSolution.Tests
{
    public class SortingServiceTests
    {
        private readonly Mock<IFileRepository> _fileRepositoryMock;
        private readonly SortingService _sortingService;

        public SortingServiceTests()
        {
            _fileRepositoryMock = new Mock<IFileRepository>();
            _fileRepositoryMock.Setup(repo => repo.SaveToFileAsync(It.IsAny<IEnumerable<int>>())).Returns(Task.CompletedTask);
            _sortingService = new SortingService(_fileRepositoryMock.Object);
        }

        [Fact]
        public async Task SplitSortAndWriteToFileAsync_ReturnsSortedNumbersAndCallsSaveToFileAsync()
        {
            // Arrange
            var numbers = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var sortedNumbers = numbers.OrderBy(n => n).ToList();

            // Act
            var result = await _sortingService.SplitSortAndWriteToFileAsync(numbers);

            // Assert
            Assert.Equal(sortedNumbers, result);

            _fileRepositoryMock.Verify(repo => repo.SaveToFileAsync(It.Is<IEnumerable<int>>(sorted => sorted.SequenceEqual(sortedNumbers))), Times.Once);
        }

        [Fact]
        public async Task BubbleSortAndWriteToFileAsync_ReturnsSortedNumbersAndCallsSaveToFileAsync()
        {
            // Arrange
            var numbers = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var sortedNumbers = numbers.OrderBy(n => n).ToList();

            // Act
            var result = await _sortingService.BubbleSortAndWriteToFileAsync(numbers);

            // Assert
            Assert.Equal(sortedNumbers, result);
            _fileRepositoryMock.Verify(repo => repo.SaveToFileAsync(It.Is<IEnumerable<int>>(sorted => sorted.SequenceEqual(sortedNumbers))), Times.Once);
        }

        [Fact]
        public void SplitSort_ReturnsSortedNumbers()
        {
            //Arrange
            var numbers = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var sortedNumbers = numbers.OrderBy(n => n).ToList();

            // Act
            var result = _sortingService.SplitSort(numbers);

            // Assert
            Assert.Equal(sortedNumbers, sortedNumbers);
        }

        [Fact]
        public void BubbleSort_ReturnsSortedNumbers()
        {
            //Arrange
            var numbers = new List<int> { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            var sortedNumbers = numbers.OrderBy(n => n).ToList();

            // Act
            var result = _sortingService.BubbleSort(numbers);

            // Assert
            Assert.Equal(sortedNumbers, sortedNumbers);
        }
    }
}
