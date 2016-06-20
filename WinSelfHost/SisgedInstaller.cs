using System;
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

            serviceInstaller.DisplayName = "Sisged Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            serviceInstaller.Description = "Caros amigos, a adoção de políticas descentralizadoras afeta positivamente a correta previsão das condições inegavelmente apropriadas.";

            serviceInstaller.ServiceName = "SisgedSvc";

            this.Installers.Add(serviceProcessInstaller);

            this.Installers.Add(serviceInstaller);
        }
    }
}