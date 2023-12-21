using Moq;
using NumberSortingSolution.BusinessLogic.Interfaces;
using NumberSortingSolution.BusinessLogic.Services;
using Xunit;

namespace NumberSortingSolution.Tests
{
    public class PerformanceServiceTests
    {
        [Fact]
        public void MeasureAlgorithmPerformance_ReturnsCorrectPerformanceResults()
        {
            // Arrange
            var sortingServiceMock = new Mock<ISortingService>();

            sortingServiceMock.Setup(service => service.SplitSort(It.IsAny<List<int>>()))
                .Returns(new List<int> { 1, 2, 3 });

            sortingServiceMock.Setup(service => service.BubbleSort(It.IsAny<List<int>>()))
                .Returns(new List<int> { 1, 2, 3 });

            var performanceService = new PerformanceService(sortingServiceMock.Object);

            // Act
            var result = performanceService.MeasureAlgorithmPerformance(new List<int> { 3, 2, 1 });

            // Assert
            Assert.Collection(result,
                item => Assert.Contains("Split sorting algorithm finished sorting", item),
                item => Assert.Contains("Bubble sorting algorithm finished sorting", item));
        }
    }
}
