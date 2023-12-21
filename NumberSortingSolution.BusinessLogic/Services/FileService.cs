using NumberSortingSolution.BusinessLogic.Interfaces;
using NumberSortingSolution.BusinessLogic.Models;
namespace NumberSortingSolution.BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private readonly IFileNameService _fileNameService;
        private readonly IFileReaderService _fileReaderService;

        public FileService(IFileNameService fileNameService, IFileReaderService fileReaderService)
        {
            _fileNameService = fileNameService;
            _fileReaderService = fileReaderService;
        }

        public async Task<LastSortedListFile> GetLatestSortingResultFile()
        {
            var sortedListFiles = _fileNameService.GetAllFileNames();
            var latestFile = _fileNameService.GetLatestFileName(sortedListFiles);

            var fileContent = await _fileReaderService.ReadFileAsync(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", latestFile));

            LastSortedListFile sortedListFile = new LastSortedListFile() { Name = latestFile, Content = fileContent };
            return sortedListFile;
        }
    }
}
