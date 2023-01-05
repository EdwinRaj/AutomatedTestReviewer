using GroupDocs.Comparison;
using GroupDocs.Comparison.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AutomatedTestReviewer
{
    public class SnapshotGenerator
    {

        public SnapshotGenerator(string testName, int snapShotFrequencyInSeconds, int testDurationInMinutes, string outputPathDirectory)
        {
            if (string.IsNullOrEmpty(testName))
            {
                throw new ArgumentException($"'{nameof(testName)}' cannot be null or empty.", nameof(testName));
            }

            if (string.IsNullOrEmpty(outputPathDirectory))
            {
                throw new ArgumentException($"'{nameof(outputPathDirectory)}' cannot be null or empty.", nameof(outputPathDirectory));
            }

            TestName = testName;
            SnapShotFrequencyInSeconds = snapShotFrequencyInSeconds;
            TestDurationInMinutes = testDurationInMinutes;
            OutputPathDirectory = outputPathDirectory;
        }

        public string TestName { get; }
        public int SnapShotFrequencyInSeconds { get; }
        public int TestDurationInMinutes { get; }
        public string OutputPathDirectory { get; }

        public void Initialize()
        {
            var testOutputDirectory = Path.Combine(OutputPathDirectory,
                String.Format("{0}_{1}", TestName, DateTime.Now.ToString("yyyyMMdd")));

            if (!Directory.Exists(testOutputDirectory))
            {
                Console.WriteLine("Output directory doesn't exist.Creating a new one");
                var directory = Directory.CreateDirectory(testOutputDirectory);
                Console.WriteLine("Output directory successfully created");
            }
            else
            {
                Console.WriteLine("Output directory exists");
            }
            System.Timers.Timer snapshotTimer = new System.Timers.Timer();
            snapshotTimer.Elapsed += new ElapsedEventHandler(OnSnapShotTrigger);
            snapshotTimer.Interval = SnapShotFrequencyInSeconds * 1000;
            snapshotTimer.Enabled = true;

            DateTime endTime = DateTime.Now.AddMinutes(TestDurationInMinutes);

            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q' || DateTime.Now > endTime) ;
        }
        
        bool isRunning = false;
        string lastFileName = null;

        private void OnSnapShotTrigger(object? sender, ElapsedEventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                    }

                    var fileName = GetFileName();
                    bitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                    lastFileName = fileName;

                }
                isRunning = false;
            }
        }
        private string GetFileName()
        {
            return Path.Combine(OutputPathDirectory, 
                String.Format("{0}_{1}", TestName, DateTime.Now.ToString("yyyyMMdd")),
                String.Format("snapshot_{0}.png",DateTime.Now.ToString("HHmmddss"))
                );
        }
    }
}
