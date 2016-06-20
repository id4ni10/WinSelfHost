using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;

using System.Threading.Tasks;

namespace WinSelfHost
{
    class SisgedSvc : ServiceBase
    {
        public SisgedSvc()
        {
            this.ServiceName = "SisgedSvc";
        }

        static void Main()
        {
            ServiceBase.Run(new SisgedSvc());
        }

        public IDisposable App { get; set; }

        protected override void OnStart(string[] args)
        {
            App = WebApp.Start<Startup>("http://localhost:1234");

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            App.Dispose();

            base.OnStop();
        }
    }
}
