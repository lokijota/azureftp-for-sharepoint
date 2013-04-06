namespace ServiceTester
{
    using ServiceTester.SharePointFtpService;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public partial class TesterMainForm : Form
    {
        public TesterMainForm()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SharePointFtpService.SharePointFtpServiceClient client = new SharePointFtpService.SharePointFtpServiceClient();

            try
            {
                OpenResponse result = client.Open(textUrl.Text, textUser.Text, textPassword.Text);

                textOutput.Text += string.Format("Status: [{0}] {1}. Session ID: {2}\n", result.StatusCode, result.StatusMessage, result.SessionId);
            }
            finally
            {
                if (client.State == System.ServiceModel.CommunicationState.Opened)
                {
                    client.Close();
                }
            }
        }
    }
}
