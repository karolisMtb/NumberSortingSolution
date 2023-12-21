namespace NumberSortingSolution.DataAccess.Interfaces
{
    public interface IFileRepository
    {
        Task SaveToFileAsync(IEnumerable<int> sortedList);
    }
}
