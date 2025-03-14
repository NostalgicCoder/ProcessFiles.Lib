using ProcessFiles.Lib.Class;
using ProcessFiles.Lib.Models;

namespace TestHarness.Console
{
    public class Run
    {
        private PerformTasks _performTasks;

        public Run()
        {
            _performTasks = new PerformTasks();
        }

        /// <summary>
        /// Call the starting point on the 'ProcessFiles.Lib' class library project
        /// </summary>
        public void CallLibrary()
        {
            List<FileData> fileDataColl = _performTasks.ReturnLargestFilesInADir(@"D:\YOUR FILE PATH");

            _performTasks.OutputDataToATextFile(@"D:\test.txt", fileDataColl);

            System.Console.WriteLine("Task Complete!");

            System.Console.ReadLine();
        }
    }
}