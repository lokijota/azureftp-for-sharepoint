namespace azureftp_for_sharepoint.server
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceProcess;
    using System.Text;
    using AzureFtpForSharePoint.Server.Implementation;

    public partial class Server : ServiceBase
    {
        /// <summary>
        /// Server service constructor
        /// </summary>
        public Server()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Host for the WCF services
        /// </summary>
        public ServiceHost ServiceHost { get; set; }

        /// <summary>
        /// Method run when the service is started. Starts up the services that respond to the client.
        /// </summary>
        /// <param name="args">Parameters for service execution start</param>
        protected override void OnStart(string[] args)
        {
            if (ServiceHost != null)
            {
                ServiceHost.Close();
            }

            // Create a ServiceHost for the SharePointFTPService type and provide the base address.
            ServiceHost = new ServiceHost(typeof(SharePointFtpService));

            // Open the ServiceHostBase to create listeners and start listening for messages.
            ServiceHost.Open();

            File.AppendAllText(@"c:\azureftpforsharepoint\log.server.log", string.Format("{0} Server Started", DateTime.Now.ToShortTimeString()));
        }

        /// <summary>
        /// Method run when service is stopped. Shuts down SharePointFtp services.
        /// </summary>
        protected override void OnStop()
        {
            if (ServiceHost != null)
            {
                ServiceHost.Close();
                ServiceHost = null;
            }

            File.AppendAllText(@"c:\azureftpforsharepoint\log.server.log", string.Format("{0} Server Stopped", DateTime.Now.ToShortTimeString()));
        }
    }
}
