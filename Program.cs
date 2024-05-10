using System;
using System.IO;
using System.Diagnostics;

namespace FirmwareVersion_1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is the file path that the cmd output gets put into temporarily for testing
            string path = "Output Path;

            //Creates and starts the process to accces the local client 
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            //Starts up command prompt and inputs the biosversion command
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C wmic bios get biosversion";
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            process.StartInfo = startInfo;

            //Starts the above process by running cmd.exe and inputting the command, then waits for user input ot exit
            process.Start();
            process.WaitForExit();

            //Outputs the result of the above command, use for sending to SQL server in the future
            string firmwareoutput = process.StandardOutput.ReadToEnd();
            firmwareoutput = firmwareoutput.Remove(0, 12);

            firmwareoutput = firmwareoutput.Replace('{', ' ');
            firmwareoutput = firmwareoutput.Replace('}', ' ');
            firmwareoutput = firmwareoutput.Replace('"', ' ');

            firmwareoutput = firmwareoutput.TrimStart();
            firmwareoutput = firmwareoutput.TrimEnd();

            File.AppendAllText(path, firmwareoutput);

            /* Next Steps:
             * 1. Find better way to remove unwanted spaces and characters
             * 2. Connect program to existing program to pull information from all computers
             * 3. Send information pulled to the database and insert it onto the existing entries
             * 4. Pull the firmwareversion column from database to DomainAudit table under inventory
             * 
             */



        }
    }
}
