using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace StoplichtService
{
    public class SystemInfo
    {
        public static string[] CurrentActiveUsers() 
        {
            List<string> users = new List<string>();
            ConnectionOptions oConn = new ConnectionOptions();
            System.Management.ManagementScope oMs = new System.Management.ManagementScope("\\\\localhost", oConn);


            System.Management.ObjectQuery oQuery = new System.Management.ObjectQuery("select * from Win32_ComputerSystem");
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection oReturnCollection = oSearcher.Get();

            foreach (ManagementObject oReturn in oReturnCollection)
            {
                users.Add(oReturn["UserName"].ToString().ToLower());
            }

            return users.ToArray();
        }
    }
}
