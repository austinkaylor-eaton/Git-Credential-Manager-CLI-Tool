// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("Enter the path to the main folder:");

string mainFolderPath = Console.ReadLine() ?? throw new InvalidOperationException("No main folder path provided");

// Check if the main folder exists
if (!Directory.Exists(mainFolderPath))
{
    throw new InvalidOperationException("The main folder does not exist.");
}

// Get the GitHub username
Console.WriteLine("Enter the GitHub username:");
string githubUsername = Console.ReadLine() ?? throw new InvalidOperationException("No GitHub username provided");

// Get all child folders in the main folder
string[] childFolders = Directory.GetDirectories(mainFolderPath);

// Iterate through each child folder
foreach (string folder in childFolders)
{
    // Get the folder name
    string folderName = new DirectoryInfo(folder).Name;

    // Construct the remote URL
    var remoteUrl = $"https://github.com/etn-ccis/{folderName}.git";

    // Run the git config command
    ProcessStartInfo processInfo = new("git", $"config --global credential.{remoteUrl}.username {githubUsername}")
    {
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    Process? process = Process.Start(processInfo);
    process.WaitForExit();
    Console.WriteLine($"Git Credential Manager username for {folderName} repository set to {githubUsername}");
}

Console.WriteLine("Git config command | git config --global credential.<URL>.username <USERNAME> | executed for all repositories.");