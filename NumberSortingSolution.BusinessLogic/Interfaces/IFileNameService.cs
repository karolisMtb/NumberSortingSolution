
namespace NumberSortingSolution.BusinessLogic.Interfaces
{
    public interface IFileNameService
    {
        string GetLatestFileName(List<string> fileNames);
        List<string> GetAllFileNames();
    }
}
