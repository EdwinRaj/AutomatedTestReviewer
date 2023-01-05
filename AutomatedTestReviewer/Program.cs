// See https://aka.ms/new-console-template for more information
using AutomatedTestReviewer;
using CommandLine;

CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args)
   .WithParsed(RunOptions)
   .WithNotParsed(HandleParseError);

void HandleParseError(IEnumerable<Error> obj)
{
    Console.WriteLine("Error while generating the command");
}

void RunOptions(CommandLineOptions obj)
{
    var testName = obj.TestName;
    var snapShotFrequency = obj.snapshotFrequencyInSeconds == 0 ? 10 : obj.snapshotFrequencyInSeconds;
    var testDurationInMinutes = obj.TestDurationInMinutes == 0 ? 60 : obj.TestDurationInMinutes;
    var outputPath = obj.OutputFilePath ?? @"c:\Edwin\AutomatedTestRevier";
    SnapshotGenerator generator = new SnapshotGenerator(testName, snapShotFrequency, testDurationInMinutes, outputPath);
    generator.Initialize();

}