using NumberSortingSolution.BusinessLogic.Interfaces;

namespace NumberSortingSolution.BusinessLogic.Services
{
    public class FileReaderService : IFileReaderService
    {
        public async Task<string> ReadFileAsync(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(stream))
            {
                var content = await reader.ReadToEndAsync();
                return content;
            }
        }
    }
}
