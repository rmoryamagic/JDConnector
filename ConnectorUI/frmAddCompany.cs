using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using ODBC;
//using Sage50;
using ConnectorLib.API;
using WebClientUtility;
using WebClientUtility.API;
using WebClientUtility.Core;
using EBS;

namespace ConnectorUI
{
    public partial class frmAddCompany : Form
    {

        public ConnectorConfiguration jdeConfiguration;
        private List<ConnectorConfiguration> sageConfigurationsList { get; set; }
        public bool isOK = false;
        public frmAddCompany(ConnectorConfiguration configuration)
        {
            InitializeComponent();

            jdeConfiguration = configuration;
            txtUserName.Text = jdeConfiguration.userName;
            txtPassword.Text = jdeConfiguration.password;
            txtConnectorKey.Text = jdeConfiguration.connectorKey;
            txtConnectorSecret.Text = jdeConfiguration.connectorSecret;
            txtServerName.Text = jdeConfiguration.serverName;
            txtDatabaseName.Text = jdeConfiguration.dataBase;
            txtport.Text = jdeConfiguration.port;

        }
        public frmAddCompany()
        {
            InitializeComponent();
            jdeConfiguration = new ConnectorConfiguration();
            Guid guid = Guid.NewGuid();
            Random random = new Random();
            int iGuid = random.Next();
            jdeConfiguration.id = iGuid;

        }
        private async void btnSaveClose_Click(object sender, EventArgs e)
        {

            try
            {
                await AddCompany();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private async Task AddCompany()
        {
            frmLoader loaderForm = new frmLoader();

            try
            {

                if (!isValid()) return;
                string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", txtServerName.Text, txtDatabaseName.Text, txtUserName.Text, txtPassword.Text);
                loaderForm.StartPosition = FormStartPosition.CenterScreen;
                loaderForm.Show();

                WebClientUtility.API.JDEConnectorApi jdeConnectorApi = new WebClientUtility.API.JDEConnectorApi(new WebClientHttpUtility(), null, txtConnectorKey.Text);

                if (!jdeConnectorApi.Handshake())
                {
                    Util.ShowMessage("Please enter a valid connector Key", this.Text);
                    loaderForm.Close();
                    return;
                }
                if (!Util.IsValidConnection(connectionString))
                {
                    Util.ShowMessage("Please enter valid connection details.", this.Text);
                    loaderForm.Close();
                    return;
                }

                loaderForm.Close();

                jdeConfiguration.company = txtDatabaseName.Text;
                jdeConfiguration.userName = txtUserName.Text;
                jdeConfiguration.password = txtPassword.Text;
                jdeConfiguration.connectorKey = txtConnectorKey.Text;
                jdeConfiguration.connectorSecret = txtConnectorSecret.Text;
                jdeConfiguration.dataBase = txtDatabaseName.Text;
                jdeConfiguration.serverName = txtServerName.Text;
                jdeConfiguration.port = txtport.Text;

                isOK = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Util.ShowMessage("Please enter valid connection details.", this.Text);
                loaderForm.Close();
                return;
            }
        }

        private bool isValid()
        {

            if (IsAlreadyExistsConnectorKey(txtConnectorKey.Text))
            {
                txtConnectorKey.Focus();
                Util.ShowMessage(string.Format("This connector key '{0}' already exists.", txtConnectorKey.Text), this.Text);
                return false;
            }
            if (string.IsNullOrEmpty(txtConnectorKey.Text))
            {
                txtConnectorKey.Focus();
                Util.ShowMessage("Please enter Connector Key", this.Text);
                return false;
            }
            if (string.IsNullOrEmpty(txtConnectorSecret.Text))
            {
                txtConnectorSecret.Focus();
                Util.ShowMessage("Please enter Connector Secret", this.Text);
                return false;
            }
            if (string.IsNullOrEmpty(txtServerName.Text))
            {
                Util.ShowMessage("Please fill Server Name.", this.Text);
                return false;
            }
            if (string.IsNullOrEmpty(txtDatabaseName.Text))
            {
                Util.ShowMessage("Please fill Database Name.", this.Text);
                return false;
            }
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                txtUserName.Focus();
                Util.ShowMessage("Please enter User Name", this.Text);
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.Focus();
                Util.ShowMessage("Please enter Password", this.Text);
                return false;
            }
            return true;
        }

        private bool IsAlreadyExistsConnectorKey(string connectorKey)
        {
            if (sageConfigurationsList != null)
            {
                return sageConfigurationsList.Where(c => c.connectorKey == connectorKey && string.IsNullOrEmpty(jdeConfiguration.company)).Count() > 0;
            }
            return false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            isOK = false;
            this.Close();
        }
        private void frmAddCompany_Load(object sender, EventArgs e)
        {

            LocalDbApi localDbApi = new LocalDbApi();
            sageConfigurationsList = localDbApi.Get<ConnectorConfiguration>("configuration").ToList();

        }
    }
}
