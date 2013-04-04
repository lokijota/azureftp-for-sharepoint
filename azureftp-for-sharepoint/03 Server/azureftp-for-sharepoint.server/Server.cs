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
    using log4net;

    public partial class Server : ServiceBase
    {
        /// <summary>
        /// Create a logger for use in this class 
        /// </summary>
        private static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

            _logger.Info("Server has started and the services are listening for requests");
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

            _logger.Info("Server has shutdown");
        }
    }
}
