using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Native
{
    public static class Advapi
    {

        [DllImport("advapi32.dll")]
        public static extern bool LogonUserA(
        string lpszUsername,
        string lpszDomain,
        string lpszPassword,
        LogonProvider dwLogonType,
        LogonUserProvider dwLogonProvider,
        ref IntPtr phToken);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool ImpersonateLoggedOnUser(IntPtr hToken);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool RevertToSelf();


        public enum LogonProvider
        {
            /// <summary>
            /// https://github.com/dahall/Vanara/
            /// </summary>

            LOGON32_LOGON_INTERACTIVE = 2,
			LOGON32_LOGON_NETWORK = 3,
			LOGON32_LOGON_BATCH = 4,
			LOGON32_LOGON_SERVICE = 5,
			LOGON32_LOGON_UNLOCK = 7,
			LOGON32_LOGON_NETWORK_CLEARTEXT = 8,
			LOGON32_LOGON_NEW_CREDENTIALS = 9
		}

		public enum LogonUserProvider
		{

			LOGON32_PROVIDER_DEFAULT = 0,
			LOGON32_PROVIDER_WINNT35 = 1,
			LOGON32_PROVIDER_WINNT40 = 2,
			LOGON32_PROVIDER_WINNT50 = 3,
			LOGON32_PROVIDER_VIRTUAL = 4
		}

	}
}
