using ProcessFiles.Lib.Models;

namespace ProcessFiles.Lib.Class
{
    public class PerformTasks
    {
        /// <summary>
        /// Return back all files in the user specified filepath from largest to smallest along with sizing data
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<FileData> ReturnLargestFilesInADir(string filePath)
        {
            List<string> fileColl = Directory.GetFiles(filePath, "*", SearchOption.AllDirectories).ToList();
            List<FileData> fileDataColl = new List<FileData>();

            foreach(string item in fileColl)
            {
                FileInfo fileInfo = new FileInfo(item);

                fileDataColl.Add(new FileData() {
                    FilePath = item,
                    FileSize = fileInfo.Length,
                    FriendlyFileSize = GetFriendlyFileSize(fileInfo.Length),
                });
            }

            return fileDataColl.OrderByDescending(x => x.FileSize).ToList();
        }

        /// <summary>
        /// Convert file size to a human friendly value in: bytes, kilobytes, megabytes or gigabytes
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        private string GetFriendlyFileSize(long fileSize)
        {
            double kb = Math.Round((double)fileSize / 1024, 0);
            double mb = Math.Round((double)fileSize / (1024 * 1024), 0);
            double gb = Math.Round((double)fileSize / (1024 * 1024 * 1024), 2);

            if (gb >= 1)
            {
                return string.Format("{0} GB", gb.ToString());
            }
            else if(gb < 1 && mb >= 1)
            {
                return string.Format("{0} MB", mb.ToString());
            }
            else if (mb < 1 && kb >= 1)
            {
                return string.Format("{0} KB", kb.ToString());
            }

            return string.Format("{0} Bytes", fileSize.ToString());
        }

        /// <summary>
        /// Output the file results to a text file, delete a copy if one already exists in the user specified location
        /// </summary>
        /// <param name="outputFilePath"></param>
        /// <param name="fileDataColl"></param>
        public void OutputDataToATextFile(string outputFilePath, List<FileData> fileDataColl)
        {
            try
            {
                if(File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }

                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    foreach (FileData file in fileDataColl)
                    {
                        writer.WriteLine(string.Format("{0} - {1}", file.FriendlyFileSize, file.FilePath));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}