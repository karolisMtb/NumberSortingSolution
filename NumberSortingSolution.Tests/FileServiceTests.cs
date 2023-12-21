using Moq;
using NumberSortingSolution.BusinessLogic.Interfaces;
using NumberSortingSolution.BusinessLogic.Services;
using Xunit;

namespace NumberSortingSolution.Tests
{
    public class FileServiceTests
    {
        [Fact]
        public async Task GetLatestSortingResultFile_ReturnsCorrectResult()
        {
            // Arrange
            var fileNameServiceMock = new Mock<IFileNameService>();
            var fileReaderMock = new Mock<IFileReaderService>();

            fileNameServiceMock.Setup(x => x.GetAllFileNames()).Returns(new List<string> { "Sorting-result-12124351.txt", "Sorting-result-12124351.txt", "Sorting-result-12124351.txt" });
            fileNameServiceMock.Setup(x => x.GetLatestFileName(It.IsAny<List<string>>())).Returns("Sorting-result-12124351.txt");

            fileReaderMock.Setup(x => x.ReadFileAsync(It.IsAny<string>())).ReturnsAsync("Test content");

            var fileService = new FileService(fileNameServiceMock.Object, fileReaderMock.Object);

            // Act
            var result = await fileService.GetLatestSortingResultFile();

            // Assert
            Assert.Equal("Sorting-result-12124351.txt", result.Name);
            Assert.Equal("Test content", result.Content);
        }
    }
}
