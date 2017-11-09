using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace WinSelfHost
{
    [RunInstaller(true)]
    public class SisgedInstaller : Installer
    {
        public SisgedInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();

            ServiceInstaller serviceInstaller = new ServiceInstaller();

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            serviceInstaller.DisplayName = "SelfMonitorSvc";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            serviceInstaller.Description = "Serviço de log para registro de logon (Espigão)";

            serviceInstaller.ServiceName = "SelfMonitorSvc";

            this.Installers.Add(serviceProcessInstaller);

            this.Installers.Add(serviceInstaller);
        }
    }
}