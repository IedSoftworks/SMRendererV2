using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using SMRenderer.Core.Enums;

namespace SMRenderer.Core
{
    public class Log
    {
        private static Log Current;
        private static bool IsInitilized;

        public string Name = "Default";

        public static Dictionary<LogWriteTarget, string> LogPresets = new Dictionary<LogWriteTarget, string>() {
            { LogWriteTarget.LogFile, "<%date% %time%> [%type%] %value%" },
            { LogWriteTarget.Console, "[%type%] %value%" },
            { LogWriteTarget.VSDebugger, "[%type%] %value%" },
        };

        public static LogWriteTarget DefaultWriteTarget = LogWriteTarget.All;

        private StreamWriter writer;
        public Log(string path = "sm.log", string compressionFolder = "")
        {
            if (!IsInitilized) Initilize();

            if (File.Exists(path))
            {
                if (compressionFolder != "")
                {
                    StreamReader reader = new StreamReader(path);
                    string line = reader.ReadLine();
                    reader.Close();

                    string time = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

                    if (line != null && line.StartsWith("#createdAt="))
                        time = line.Split(new[] {'='}, 2)[1];

                    using (ZipArchive archive = ZipFile.Open($"{ compressionFolder }/{ Path.GetFileName(path) }_{ time }.zip", ZipArchiveMode.Create))
                    {
                        archive.CreateEntryFromFile(path, "Logfile.log");
                    }
                }

                File.Delete(path);
            }

            writer = new StreamWriter(path) {AutoFlush = true};
            writer.WriteLine("#createdAt="+DateTimeOffset.Now.ToUnixTimeMilliseconds());
            writer.WriteLine(ProcessPreset(LogWriteTarget.LogFile, LogWriteType.Info.ToString(), $"Opened file"));
            
            Write(LogWriteType.Info, $"Create log at file '{path}'");

            AppDomain.CurrentDomain.DomainUnload += (sender, args) => { Close(); };
        }

        public static void Initilize()
        {
            Write(LogWriteType.Info, "Initilize log system");

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Write(args.IsTerminating ? LogWriteType.UnexpectedCriticalError : LogWriteType.UnexpectedError,
                    args.ExceptionObject, LogWriteTarget.LogFile | LogWriteTarget.VSDebugger);
                if (args.IsTerminating)
                {
                    MessageBox.Show("A critical error occured.\n\n" + ((Exception)args.ExceptionObject).Message);
                    Environment.Exit(0);
                }
            };

            IsInitilized = true;
        }

        public void Enable()
        {
            Current?.Disable($"New log '{Name}' enabled.");
            Current = this;
            Write(LogWriteType.Info, $"Enabled log '{Name}'");
        }

        public void Disable(string reason = "Programer called")
        {
            Write(LogWriteType.Info, $"Disabled log '{Name}'. Reason: '{reason}'");
            Current = null;
        }

        public void Close()
        {
            writer.WriteLine("# Closed at: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            writer.Close();
        }

        public static void Write<T>(LogWriteType type, T value, LogWriteTarget target = LogWriteTarget.Default) =>
            Write(type.ToString(), value, target);

        public static void Write<T>(string type, T value, LogWriteTarget target = LogWriteTarget.Default)
        {
            if (target == LogWriteTarget.Default)
                target = DefaultWriteTarget;

            if (target.HasFlag(LogWriteTarget.Console))
                Console.WriteLine(ProcessPreset(LogWriteTarget.Console, type, value.ToString()));
            if (target.HasFlag(LogWriteTarget.VSDebugger))
                Debug.WriteLine(ProcessPreset(LogWriteTarget.VSDebugger, type, value.ToString()));
            if (target.HasFlag(LogWriteTarget.LogFile))
                Current?.writer.WriteLine(ProcessPreset(LogWriteTarget.LogFile, type, value.ToString()));
        }

        public static string ProcessPreset(LogWriteTarget target, string type, string value)
        {
            string preset = LogPresets[target];
            DateTime now = DateTime.Now;

            return preset.Replace("%date%", now.ToShortDateString())
                .Replace("%time%", now.ToShortTimeString())
                .Replace("%type%", type)
                .Replace("%value%", value);
        }
    }
}