using System.IO;
using System.IO.Pipes;

namespace StreamFeedstock
{
    public static class Logger
    {
        private static ILogWindow? ms_LogWindow = null;
        private static readonly Dictionary<string, List<string>> ms_Logs = new();
        private static bool ms_LogInFile = true;
        private static readonly object ms_Lock = new();

        public static void StopLogger()
        {
            lock (ms_Lock)
            {
                ms_LogWindow?.LogWindow_Close();
                ms_LogInFile = false;
            }
        }

        public static void SetLogWindow(ILogWindow? logWindow) => ms_LogWindow = logWindow;

        public static IReadOnlyList<string> GetCategories()
        {
            lock (ms_Lock)
            {
                List<string> categories = new();
                foreach (string category in ms_Logs.Keys)
                    categories.Add(category);
                return categories.AsReadOnly();
            }
        }

        public static IReadOnlyList<string> GetLogs(string category)
        {
            lock (ms_Lock)
            {
                if (ms_Logs.TryGetValue(category, out List<string>? logs))
                    return logs.AsReadOnly();
                return new List<string>().AsReadOnly();
            }
        }

        public static void Log(string category, string log)
        {
            lock (ms_Lock)
            {
                if (ms_Logs.TryGetValue(category, out List<string>? logs))
                    logs.Add(log);
                else
                    ms_Logs.Add(category, new() { log });
                if (ms_LogInFile)
                {
                    string filename = string.Format("./log/{0}.log", category);
                    StreamWriter fileStream;
                    if (!File.Exists(filename))
                    {
                        Directory.CreateDirectory("./log");
                        fileStream = new(File.Create(filename));
                    }
                    else
                        fileStream = File.AppendText(filename);
                    fileStream.WriteLine(log);
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
            ms_LogWindow?.LogWindow_Update();
        }
    }
}
