using System.Diagnostics;

/// <summary>
/// Hacky Workaround Source:
/// https://stackoverflow.com/questions/24000793/application-started-by-process-start-isnt-getting-arguments
/// </summary>

namespace Arcturus.ExternalSystems
{
    public static class ExternalSystemsUtil
    {
        /// <summary>
        /// Dynamic var, only affects process following value change.<br/><br/>
        /// If your app is opening but the args aren't going through turn this on. (Default = true)<br/>
        /// Has something to do with C# string formatting or something...
        /// </summary>
        public static bool UseHackyWorkaround { get; set; } = true;

        #region Helper Funcs
        private static string FilterPath(this string path)
        {
            path = path.Replace(@"/", @"\\").Replace(@"\", @"\\");
            return path;
        }

        private static string CompileArgs(params string[] args)
        {
            if (UseHackyWorkaround)
                return "/C " + string.Join(" ", args).FilterPath();
            else
                return string.Join(" ", args).FilterPath();
        }
        #endregion

        #region Openform Funcs
        /// <summary>
        /// Opens an external application then runs it with the provided arguments.<br/><br/>
        /// Example:<br/>
        /// RunExternalApp("cmd.exe", @$"{Application.streamingAssetsPath}", "ExampleBatch.bat");
        /// </summary>
        /// <param name="appPath"></param>
        /// <param name="workingDirectory"></param>
        /// <param name="args"></param>
        public static void RunExternalApp(string appPath, string workingDirectory, params string[] args)
        {
            RunExternalApp(appPath, workingDirectory, true, args);
        }

        public static void RunExternalApp(string appPath, string workingDirectory, bool useShell, params string[] args)
        {
            var startInfo = new ProcessStartInfo(appPath);
            startInfo.WorkingDirectory = workingDirectory.FilterPath();
            startInfo.Arguments = CompileArgs(args);
            startInfo.UseShellExecute = useShell;

            var process = Process.Start(startInfo);
            process.WaitForExit();
            process.Close();
        }

        /// <summary>
        /// Opens an external application then runs it.<br/>
        /// Only use if you know what you're doing!<br/><br/>
        /// Requires:<br/> using System.Diagnostics;
        /// </summary>
        /// <param name="startInfo"></param>
        public static void RunExternalApp(ProcessStartInfo startInfo)
        {
            var process = Process.Start(startInfo);
            process.WaitForExit();
            process.Close();
        }
        #endregion

        #region Accessibility Funcs
        /// <summary>
        /// Opens the console (cmd.exe) with the working dir and args.
        /// </summary>
        /// <param name="workingDirectory"></param>
        /// <param name="args"></param>
        public static void OpenCmd(string workingDirectory, params string[] args)
        {
            RunExternalApp("cmd.exe", workingDirectory, args);
        }
        #endregion
    }
}
