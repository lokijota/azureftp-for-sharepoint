namespace ServiceTester
{
    using ServiceTester.SharePointFtpService;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.ServiceModel;
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
                OpenRequest request = new OpenRequest()
                {
                    Url = textUrl.Text,
                    Username = textUser.Text,
                    Password = textPassword.Text
                };

                OpenResponse result = client.Open(request);

                textOutput.Text += string.Format("SUCCESS. Web URL: {0}, Folder URL: {1}\n\r", result.WebUrl, result.FolderUrl);
            }
            catch (FaultException<InvalidUrlFault> fault)
            {
                textOutput.Text += string.Format("FAILURE. The URL {0} is not valid.", fault.Detail.Url);
            }
            catch (FaultException<Exception> fault)
            {
                textOutput.Text += string.Format("Unexpected Error: {0}.", fault.Detail.Message);
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
