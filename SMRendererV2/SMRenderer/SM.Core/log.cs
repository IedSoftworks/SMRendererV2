using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using SM.Core.Enums;

namespace SM.Core
{
    /// <summary>
    /// Logging
    /// </summary>
    public class Log
    {
        private static Log Current;
        private static bool IsInitilized;

        /// <summary>
        /// This prevents the crash-handler to prepare itself.
        /// <para>Default: true (To enable VSDebugger, etc. to work properly.)</para>
        /// <para>It is recommended to disable it at release.</para>
        /// </summary>
        public static bool UseOwnCrashEvent = true;

        /// <summary>
        /// This allows to set preset, depending on what target the log is written.
        /// </summary>
        public static Dictionary<LogWriteTarget, string> LogPresets = new Dictionary<LogWriteTarget, string>() {
            { LogWriteTarget.LogFile, "<%date% %time%> [%type%] %value%" },
            { LogWriteTarget.Console, "[%type%] %value%" },
            { LogWriteTarget.VSDebugger, "[%type%] %value%" },
        };

        /// <summary>
        /// This sets the default write target.
        /// <para>Default: All</para>
        /// </summary>
        public static LogWriteTarget DefaultWriteTarget = LogWriteTarget.All;

        private StreamWriter writer;

        /// <summary>
        /// The internal name of the log file.
        /// </summary>
        public string Name = "Default";

        /// <summary>
        /// Creates a new log file.
        /// </summary>
        /// <param name="path">Path for the log file</param>
        /// <param name="compressionFolder">Folder to store compressed log files, if the file already exists. If none set, it just override the file.</param>
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
        /// <summary>
        /// Set this log as current.
        /// </summary>
        public void Enable()
        {
            Current?.Disable($"New log '{Name}' enabled.");
            Current = this;
            Write(LogWriteType.Info, $"Enabled log '{Name}'");
        }
        /// <summary>
        /// Disables the log file.
        /// </summary>
        /// <param name="reason"></param>
        public void Disable(string reason = "Programer called")
        {
            Write(LogWriteType.Info, $"Disabled log '{Name}'. Reason: '{reason}'");
            Current = null;
        }
        /// <summary>
        /// Closes the log file.
        /// </summary>
        public void Close()
        {
            writer.WriteLine("# Closed at: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            writer.Close();
        }

        /// <summary>
        /// Setup the log system.
        /// </summary>
        public static void Initilize()
        {
            Write(LogWriteType.Info, "Initilize log system");

            if (!UseOwnCrashEvent) AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Write(args.IsTerminating ? LogWriteType.UnexpectedCriticalError : LogWriteType.UnexpectedError,
                    GLDebug.AdvDebugging ? args.ExceptionObject : ((Exception)args.ExceptionObject).Message, LogWriteTarget.LogFile | LogWriteTarget.VSDebugger);
                if (args.IsTerminating)
                {
                    MessageBox.Show("A critical error occured.\n\n" + ((Exception)args.ExceptionObject).Message);
                    Environment.Exit(0);
                }
            };

            IsInitilized = true;
        }

        /// <summary>
        /// Write multiple logs with the default write target
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="type">Type of log</param>
        /// <param name="values">values</param>
        public static void Write<T>(LogWriteType type, params T[] values) => Write(type.ToString(), values);
        /// <summary>
        /// Write multiple logs with the default write target
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="type">Type of log</param>
        /// <param name="values">values</param>
        public static void Write<T>(string type, params T[] values)
        {
            for (int i = 0; i < values.Length; i++) 
                Write(type, values[i]);
        }

        /// <summary>
        /// Write a log
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="type">Type of log</param>
        /// <param name="value">value</param>
        /// <param name="target">Log target</param>
        public static void Write<T>(LogWriteType type, T value, LogWriteTarget target = LogWriteTarget.Default) =>
            Write(type.ToString(), value, target);

        /// <summary>
        /// Write a log
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="type">Type of log</param>
        /// <param name="value">value</param>
        /// <param name="target">Log target</param>
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

        /// <summary>
        /// Processes a preset.
        /// </summary>
        /// <param name="target">The log target</param>
        /// <param name="type">The type of a log</param>
        /// <param name="value">The value</param>
        /// <returns></returns>
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