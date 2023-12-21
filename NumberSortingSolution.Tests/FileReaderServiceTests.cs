using NumberSortingSolution.BusinessLogic.Services;
using System.Text;
using Xunit;

namespace NumberSortingSolution.Tests
{
    public class FileReaderServiceTests
    {
        private readonly string filePath;

        public FileReaderServiceTests()
        {
            filePath = Path.Combine(AppContext.BaseDirectory, "testdata.txt");
        }

        [Fact]
        public async Task ReadFileAsync_ReturnsCorrectContent()
        {
            // Arrange
            var contentToWrite = "This is the file content.";
            var byteArray = Encoding.UTF8.GetBytes(contentToWrite);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await stream.WriteAsync(byteArray, 0, byteArray.Length);
                }

                var fileService = new FileReaderService();

                // Act
                var content = await fileService.ReadFileAsync(filePath);

                // Assert
                Assert.Equal(contentToWrite, content);
            }
            finally
            {
                File.Delete(filePath);
            }
        }

    }
}
