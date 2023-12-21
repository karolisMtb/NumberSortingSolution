using NumberSortingSolution.DataAccess.Interfaces;

namespace NumberSortingSolution.DataAccess.Repositories
{
    public class FileRepository : IFileRepository
    {
        public async Task SaveToFileAsync(IEnumerable<int> sortedList)
        {
            string fileName = $"Sorting-result-{DateTime.Now.ToString("MHHmmss")}.txt";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", fileName);

            try
            {
                await Task.Run(() => File.WriteAllLines(filePath, sortedList.Select(x => x.ToString())));
            }
            catch (IOException ex)
            {
                throw new IOException($"Error saving to file: {ex.Message}", ex);
            }
        }
    }
}
