namespace ServiceTester
{
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
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SharePointFtpService.SharePointFtpServiceClient client = new SharePointFtpService.SharePointFtpServiceClient();

            try
            {
                bool result = client.Open(textUrl.Text, textUser.Text, textPassword.Text);

                textOutput.Text += "Result of call is: " + result.ToString();
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
