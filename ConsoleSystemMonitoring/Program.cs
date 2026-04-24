using System.Runtime.InteropServices;

namespace ConsoleSystemMonitoring
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!OperatingSystem.IsWindows())
            {
                Console.WriteLine("This application is only supported on Windows.");
                return;
            }
        }
    }
}
