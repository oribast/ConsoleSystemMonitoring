using System.Text;

namespace ConsoleSystemMonitoring.Utils
{
    public static class StringBuilderExtension
    {
        private static string GetTimestamp(char finalSymbol)
            => $"[{DateTime.UtcNow:dd.MM.yyyy HH:mm:ss:ffff}]{finalSymbol}";

        public static void AppendLineWithTimestamp(this StringBuilder strBuilder, string str, bool useTimestamp = false)
        {
            if (useTimestamp)
                strBuilder.Append(GetTimestamp(' '));
            strBuilder.AppendLine(str);
        }
    }
}
