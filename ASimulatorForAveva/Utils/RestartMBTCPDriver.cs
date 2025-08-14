using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASimulatorForAveva.Utils
{
    public static class RestartMBTCPDriver
    {
        public static void restart()
        {
            string targetProcessName = "MBTCP"; // Process name without .exe
            string targetExePath = @"C:\Program Files (x86)\Wonderware\OI-Server\OI-MBTCP\Bin\MBTCP.exe";

            bool killed = false;

            try
            {
                var processes = Process.GetProcessesByName(targetProcessName);

                foreach (var process in processes)
                {
                    try
                    {
                        string exePath = process.MainModule.FileName;

                        if (string.Equals(exePath, targetExePath, StringComparison.OrdinalIgnoreCase))
                        {
                            process.Kill();
                            process.WaitForExit();
                            killed = true;
                            Debug.WriteLine("MBTCP.exe has been terminated.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error accessing or killing process: {ex.Message}");
                    }
                }

                if (!killed)
                {
                    Debug.WriteLine("No running instance of MBTCP.exe found to terminate.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while enumerating processes: {ex.Message}");
            }

            // Optional short delay before restarting
            Thread.Sleep(1000);

            // Start the application again
            try
            {
                Process.Start(targetExePath);
                Debug.WriteLine("MBTCP.exe has been restarted.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to start MBTCP.exe: {ex.Message}");
            }
        }
    }
}