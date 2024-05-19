using System.Diagnostics;
using System.Management;

namespace FenixLauncher.Functions
{
    public class SysProcesses
    {
        public void KillProcessesByPID(int pid)
        {
            ManagementObjectSearcher ps = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection po = ps.Get();

            if (po != null)
            {
                foreach (ManagementObject mo in po)
                {
                    int process_id = Convert.ToInt32(mo["ProcessID"]);
                    KillProcessesByPID(process_id);
                }
            }

            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited)
                {
                    proc.Kill();
                }
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
