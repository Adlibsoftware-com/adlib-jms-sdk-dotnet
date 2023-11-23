using Adlib.Integration.Client.ConsoleSample;
using Adlib.Integration.Client.Library;
using Adlib.Integration.Client.Library.Settings;

try
{
    string applicationSettingsFile = System.IO.Path.GetFullPath(".\\ApplicationSettings.xml");
    if (args.Length > 0)
    {
        applicationSettingsFile = System.IO.Path.GetFullPath(args[0]);
    }
    var applicationSettings = ApplicationSettings.LoadSettings(applicationSettingsFile);
    // Sanity check
    if (applicationSettings.AdlibJobManagementServiceMachineName.Equals("ADLIB_JOB_MANAGEMENT_SERVICE_MACHINE"))
    {
        throw new ArgumentException($"Please change <AdlibJobManagementServiceMachineName> value to match your Adlib Service machine name in: {applicationSettingsFile}");
    }
    Utilities.GetPassword(applicationSettings);

    var clientFlow = new ClientFlow(applicationSettings, true);

    var completedJobIds = clientFlow.ExecuteSampleJobFlow();

    if (clientFlow.ApplicationSettings.CreateProblemSubmission && completedJobIds.Count > 0)
    {
        clientFlow.CreateProblemSubmission(completedJobIds.First());
    }
    System.Console.WriteLine("Sample client completed successfully");
}
catch (ArgumentException arg)
{
    System.Console.WriteLine($"Error occurred: {arg.Message}");
}
catch (Exception ex)
{
    System.Console.WriteLine($"Error occurred: {ex.ToString()}");
}
System.Console.WriteLine("Press any key...");
Console.ReadKey();
return;


