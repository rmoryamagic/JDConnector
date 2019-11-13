using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using ConnectorLib.API;
using System.Linq;
using ConnectorEntity;

namespace ConnectorUI
{
    public partial class frmMain : Form
    {
        public bool isEnableNotiication = true;
        private bool allowshowdisplay = false;
        private LocalDbApi localDbApi;
        private List<ConnectorConfiguration> listSageConfiguration;
        public List<QueryRequest> queryRequests = new List<QueryRequest>();
        public frmMain()
        {
            InitializeComponent();
        }
        private void ODBCConnectorControl_btnRemoveClick(object sender, EventArgs e)
        {
            ConnectorControl ctr = sender as ConnectorControl;
            DialogResult dialogResult = MessageBox.Show(string.Format("Are you sure want to remove {0}?", ctr.jdeConfiguration.company), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ctr.AutoRun = false;
                pnlContainer.Controls.Remove(ctr);
                Save();
                btnAddCompany.Enabled = true;
            }
        }
        private void notificationPopupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewConfiguration();
        }
        private void frmODBC_Load(object sender, EventArgs e)
        {
            localDbApi = new LocalDbApi();
            listSageConfiguration = localDbApi.Get<ConnectorConfiguration>("configuration").ToList();
            foreach (ConnectorConfiguration sageConfiguration in listSageConfiguration)
            {

                ConnectorControl oDBCConnectorControl = new ConnectorControl();
                oDBCConnectorControl.mainForm = this;
                oDBCConnectorControl.Width = 889;
                oDBCConnectorControl.jdeConfiguration = sageConfiguration;
                oDBCConnectorControl.SageCompanyName = sageConfiguration.dataBase;
                oDBCConnectorControl.SelectCheckbox = sageConfiguration.selectCheckbox;
                oDBCConnectorControl.AutoRun = sageConfiguration.autoRun;
                oDBCConnectorControl.EveryMinutes = sageConfiguration.everyMinutes;
                oDBCConnectorControl.LastRun = sageConfiguration.lastRun;
                oDBCConnectorControl.LastResult = sageConfiguration.lastResult;
                oDBCConnectorControl.lblLastResult.ForeColor = sageConfiguration.lastResult == "Failed" ? Color.Red : Color.Green;
                oDBCConnectorControl.id = sageConfiguration.id;
                oDBCConnectorControl.btnRemoveEventClick += ODBCConnectorControl_btnRemoveClick;
                oDBCConnectorControl.btnEditEventClick += ODBCConnectorControl_btnEditEventClick;
                oDBCConnectorControl.chkAutoSelectEventStateChanged += ODBCConnectorControl_chkAutoSelectEventStateChanged;
                oDBCConnectorControl.chkSelectEventStateChanged += ODBCConnectorControl_chkSelectEventStateChanged;
                oDBCConnectorControl.everyMinEventValueChanged += ODBCConnectorControl_everyMinEventValueChanged;
                oDBCConnectorControl.Dock = DockStyle.Top;
                pnlContainer.Controls.Add(oDBCConnectorControl);
                oDBCConnectorControl.UpdateSageConfiguration();
            }
            if (listSageConfiguration.Count > 0)
            {
                btnAddCompany.Enabled = false;
            }
        }
        private void ODBCConnectorControl_everyMinEventValueChanged(object sender, EventArgs e)
        {

        }
        private void ODBCConnectorControl_chkSelectEventStateChanged(object sender, EventArgs e)
        {

        }
        private void ODBCConnectorControl_chkAutoSelectEventStateChanged(object sender, EventArgs e)
        {

        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmNotificationSettings f = new frmNotificationSettings();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        private void ddlInterval_ValueChanged(object sender, EventArgs e)
        {
            // timer1.Interval = (60000 * 1);
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {

        }
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var control in pnlContainer.Controls)
            {
                if (control is ConnectorControl)
                {
                    ConnectorControl connectorControl = control as ConnectorControl;
                    connectorControl.SelectCheckbox = true;

                }
            }
        }
        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (var control in pnlContainer.Controls)
            {
                if (control is ConnectorControl)
                {
                    ConnectorControl connectorControl = control as ConnectorControl;
                    connectorControl.SelectCheckbox = false;

                }
            }
        }
        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            AddNewConfiguration();
            listSageConfiguration = localDbApi.Get<ConnectorConfiguration>("configuration").ToList();
            if(listSageConfiguration.Count>0)
            {
                btnAddCompany.Enabled = false;
            }
        }
        private void AddNewConfiguration()
        {
            try
            {
                frmAddCompany frmAddCompany = new frmAddCompany();
                frmAddCompany.StartPosition = FormStartPosition.CenterScreen;
                frmAddCompany.ShowDialog();
                if (frmAddCompany.isOK)
                {
                    ConnectorControl oDBCConnectorControl = new ConnectorControl();
                    oDBCConnectorControl.mainForm = this;
                    oDBCConnectorControl.Width = 889;
                    oDBCConnectorControl.jdeConfiguration = frmAddCompany.jdeConfiguration;
                    oDBCConnectorControl.SageCompanyName = frmAddCompany.jdeConfiguration.company;
                    oDBCConnectorControl.SelectCheckbox = false;
                    oDBCConnectorControl.id = frmAddCompany.jdeConfiguration.id;
                    oDBCConnectorControl.btnRemoveEventClick += ODBCConnectorControl_btnRemoveClick;
                    oDBCConnectorControl.btnEditEventClick += ODBCConnectorControl_btnEditEventClick;
                    pnlContainer.Controls.Add(oDBCConnectorControl);
                    oDBCConnectorControl.UpdateSageConfiguration();
                    Save();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void ODBCConnectorControl_btnEditEventClick(object sender, EventArgs e)
        {

            ConnectorControl oDBCConnectorControl = sender as ConnectorControl;
            ConnectorConfiguration sageConfiguration = oDBCConnectorControl.jdeConfiguration;
            frmAddCompany frmAddCompany = new frmAddCompany(sageConfiguration);
            frmAddCompany.StartPosition = FormStartPosition.CenterScreen;
            frmAddCompany.ShowDialog();
            if (frmAddCompany.isOK)
            {
                oDBCConnectorControl.jdeConfiguration = frmAddCompany.jdeConfiguration;
                oDBCConnectorControl.SageCompanyName = frmAddCompany.jdeConfiguration.company;
                Save();
            }
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to exit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        public void Save()
        {

            LocalDbApi LocalDbApi = new LocalDbApi();
            LocalDbApi.drop("configuration");
            foreach (var control in pnlContainer.Controls)
            {
                if (control is ConnectorControl)
                {
                    ConnectorControl connectorControl = control as ConnectorControl;
                    LocalDbApi.Save<ConnectorConfiguration>("configuration", connectorControl.jdeConfiguration);
                }
            }
        }
        private async void syncSelected_Click(object sender, EventArgs e)
        {

            frmLoader frm = new frmLoader();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            if (!IsAnySelected() && (pnlContainer.Controls.Count > 0))
            {
                Util.ShowMessage("Please select at least one database", this.Text);
                return;
            }
            frm.Close();
            await SyncSelected();

        }
        private async Task SyncSelected()
        {
            string selectedCompany = string.Empty;

            foreach (var control in pnlContainer.Controls)
            {
                if (control is ConnectorControl)
                {
                    ConnectorControl connectorControl = control as ConnectorControl;
                    ConnectorConfiguration sageConfiguration = connectorControl.jdeConfiguration;
                    Connector.mainForm = this;
                    LogRecord logRecord = await Connector.start(sageConfiguration);
                    connectorControl.SetConnectorLogDetails(logRecord);

                }
            }

        }
        private bool IsAnySelected()
        {
            foreach (var control in pnlContainer.Controls)
            {
                if (control is ConnectorControl)
                {
                    ConnectorControl connectorControl = control as ConnectorControl;
                    if (connectorControl.chkSelect.Checked)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void btnViewLog_Click(object sender, EventArgs e)
        {
            string logPath = Path.Combine(Application.StartupPath, "Logs", "log.txt");
            if (File.Exists(logPath))
            {
                Process.Start(logPath);
            }
            else
            {
                MessageBox.Show("Log file does not exit");
            }
        }
        private void menuEnableNotification_Click(object sender, EventArgs e)
        {
            if (isEnableNotiication)
            {
                MessageBox.Show("Notification has been disabled.");
            }
            else
            {
                MessageBox.Show("Notification has been enabled.");
            }
            isEnableNotiication = !isEnableNotiication;


        }
        private void ShowNotification(string title, string description)
        {
            
            popupNotifier1.ShowCloseButton = true;
            popupNotifier1.ShowOptionsButton = false;
            popupNotifier1.ShowGrip = false;
            popupNotifier1.Delay = 3000;
            popupNotifier1.AnimationInterval = 10;
            popupNotifier1.AnimationDuration = 1000;
            popupNotifier1.Scroll = true;
            popupNotifier1.IsRightToLeft = false;
            popupNotifier1.TitleText = title;
            popupNotifier1.ContentText = description;
            popupNotifier1.Popup();
        }
        private void menuExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to exit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.allowshowdisplay = true;
            this.Visible = !this.Visible;

        }

        private void menuClearLog_Click(object sender, EventArgs e)
        {
            string logPath = Path.Combine(Application.StartupPath, "Logs", "log.txt");
            if (File.Exists(logPath))
            {
                File.Delete(logPath);


            }
        }
    }

}
