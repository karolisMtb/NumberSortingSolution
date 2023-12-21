using NumberSortingSolution.BusinessLogic.Services;
using Xunit;

namespace NumberSortingSolution.Tests
{
    public class FileNameServiceTests
    {
            private readonly FileNameService _fileNameService;

            public FileNameServiceTests()
            {
                _fileNameService = new FileNameService();
            }

            [Fact]
            public void GetLatestFileName_ReturnsCorrectFileName()
            {
                // Arrange
                var fileNames = new List<string>()
            {
                "Sorting-result-12124351.txt",
                "Sorting-result-12124348.txt",
                "Sorting-result-12124333.txt"
            };
                var expectedFileName = "Sorting-result-12124351.txt";

                // Act
                var actualFileName = _fileNameService.GetLatestFileName(fileNames);

                // Assert
                Assert.Equal(expectedFileName, actualFileName);
            }
        }
}
