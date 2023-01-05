using CommandLine.Text;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestReviewer
{
    public class CommandLineOptions
    {
        [Option('p', "path", Required = true, HelpText = "Set the output file path. C:\\StudentName\\TestName")]
        public string OutputFilePath { get; set; }

        [Option('n', "name", Required = true, HelpText = "Set the Name of the exam")]
        public string TestName { get; set; }

        [Option('m', "testduration", Required = false, HelpText = "Set the test duration.Default is 60 minutes")]
        public int TestDurationInMinutes { get; set; }

        [Option('f', "snapshotfrequency", Required = false, HelpText = "Sets the snapshot frequency In seconds. Default is 10 seconds")]
        public int snapshotFrequencyInSeconds { get; set; }
    }
}
