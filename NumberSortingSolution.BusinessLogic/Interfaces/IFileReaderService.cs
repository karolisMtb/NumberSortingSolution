namespace NumberSortingSolution.BusinessLogic.Interfaces
{
    public interface IFileReaderService
    {
        Task<string> ReadFileAsync(string filePath);
    }
}
