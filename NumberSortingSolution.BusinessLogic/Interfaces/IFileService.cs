using NumberSortingSolution.BusinessLogic.Models;

namespace NumberSortingSolution.BusinessLogic.Interfaces
{
    public interface IFileService
    {
        Task<LastSortedListFile> GetLatestSortingResultFile();
    }
}
