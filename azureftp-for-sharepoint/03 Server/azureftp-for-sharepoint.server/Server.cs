namespace azureftp_for_sharepoint.server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.IO;

    public partial class Server : ServiceBase
    {
        public Server()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            File.AppendAllText("log.server.log", string.Format("{0} Server Started", DateTime.Now.ToShortTimeString()));
        }

        protected override void OnStop()
        {
            File.AppendAllText("log.server.log", string.Format("{0} Server Stopped", DateTime.Now.ToShortTimeString()));
        }
    }
}
