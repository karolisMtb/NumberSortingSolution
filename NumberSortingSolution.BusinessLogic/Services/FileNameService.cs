using NumberSortingSolution.BusinessLogic.Interfaces;

namespace NumberSortingSolution.BusinessLogic.Services
{
    public class FileNameService : IFileNameService
    {
        public string GetLatestFileName(List<string> fileNames)
        {
            DateTime latestDate = DateTime.MinValue;
            string latestFileName = null;

            foreach (var fileName in fileNames)
            {
                string datePart = fileName.Split('-')[2].Split('.')[0];

                if (DateTime.TryParseExact(datePart, "MHHmmss", null, System.Globalization.DateTimeStyles.None, out DateTime fileDate))
                {
                    if (fileDate > latestDate)
                    {
                        latestDate = fileDate;
                        latestFileName = fileName;
                    }
                }
            }
            //Assuming fileNames always has at least one file, latestFileName won't be null here.
            return latestFileName;
        }

        public List<string> GetAllFileNames()
        {
            string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            DirectoryInfo directoryInfo = new DirectoryInfo(downloadsFolder);
            FileInfo[] matchingFiles = directoryInfo.GetFiles("Sorting-result-*.txt", SearchOption.TopDirectoryOnly);

            if (matchingFiles.Length == 0)
                throw new FileNotFoundException("No matching files found.", "Sorting-result-*.txt");

            List<string> fileTitles = matchingFiles.Select(file => file.Name).ToList();

            return fileTitles;
        }
    }
}
