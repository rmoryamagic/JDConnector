using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Sage50;
//using ConnectorLib.Models;
using ConnectorLib.API;
using ConnectorEntity;

namespace ConnectorUI
{
    public partial class ConnectorControl : UserControl
    {
        public event EventHandler btnRemoveEventClick;
        public event EventHandler btnEditEventClick;
        public event EventHandler chkAutoSelectEventStateChanged;
        public event EventHandler chkSelectEventStateChanged;
        public event EventHandler everyMinEventValueChanged;
        public event EventHandler linkMoreInformationClick;
        private string description { get; set; }
        public frmMain mainForm;
        private bool _selectCheckbox;
        private string _sageCompanyName;
        private bool _autoRun;
        private int _everyMinutes;
        private string _lastRun;
        private string _lastResult;
        private string _linkMoreInformation;
        public ConnectorConfiguration jdeConfiguration { get; set; }
        public int id { get; set; }
        public string SageCompanyName { get => _sageCompanyName; set => lblCompany.Text = value; }
        public bool SelectCheckbox { get => _selectCheckbox; set => chkSelect.Checked = value; }
        public bool AutoRun { get => _autoRun; set => chkAutoRun.Checked = value; }
        public int EveryMinutes { get => _everyMinutes; set => numericEveryMin.Value = value; }
        public string LastRun { get => _lastRun; set => lblLastRun.Text = value; }
        public string LastResult { get => _lastResult; set => lblLastResult.Text = value; }
        public string LinkMoreInformation { get => _linkMoreInformation; set => linkMoreInformation.Text = value; }
        public ConnectorControl()
        {
            InitializeComponent();
        }
        public void UpdateSageConfiguration()
        {

            jdeConfiguration.everyMinutes = (int)numericEveryMin.Value;
            jdeConfiguration.autoRun = chkAutoRun.Checked;
            jdeConfiguration.selectCheckbox = chkSelect.Checked;
            jdeConfiguration.lastRun = lblLastRun.Text;
            jdeConfiguration.lastResult = lblLastResult.Text;

        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (btnRemoveEventClick != null)
            {
                btnRemoveEventClick(this, e);

            }
        }
        private void chkAutoRun_CheckStateChanged(object sender, EventArgs e)
        {
            timer1.Enabled = chkAutoRun.Checked;
            if (!chkAutoRun.Checked)
            {
                ShowNotification(mainForm.Text, lblCompany.Text + " Service Stopped.");
            }
            if (chkAutoSelectEventStateChanged != null)
            {
                UpdateSageConfiguration();
                mainForm.Save();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEditEventClick != null)
            {
                btnEditEventClick(this, e);
            }
        }
        private void chkSelect_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkSelectEventStateChanged != null)
            {
                UpdateSageConfiguration();
                mainForm.Save();
            }

        }
        private void numericEveryMin_ValueChanged_1(object sender, EventArgs e)
        {
            if (everyMinEventValueChanged != null)
            {
                timer1.Interval = (60000 * (int)numericEveryMin.Value);
                UpdateSageConfiguration();
                mainForm.Save();
            }
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = (60000 * (int)numericEveryMin.Value);
                await Start();
        }
        private async Task Start()
        {
            Connector connector = new Connector();
            Connector.mainForm = mainForm;
          
            LogRecord logRecord = await Connector.start(jdeConfiguration);
            SetConnectorLogDetails(logRecord);
            if (mainForm.isEnableNotiication)
            {
                string message = logRecord.Status == "Failed" ?
                string.Format("{0}: Sync completed with some error," +
                " Application has been notified of the error accordingly. " +
                "See {1} log for further information.", jdeConfiguration.company, jdeConfiguration.company)
                : string.Format("{0}: Sync completed.", jdeConfiguration.company);

                ShowNotification(mainForm.Text, string.Format("{0}", message));
            }
            SetProgress(100, string.Format("\n{0} Sync Completed", jdeConfiguration.company));
            await Task.Delay(500);
            SetProgress(0, "");
        }

        public void SetConnectorLogDetails(LogRecord logRecord)
        {

            lblLastRun.Text = logRecord.Date.ToString();
            lblLastResult.Text = logRecord.Status;
            lblLastResult.ForeColor = logRecord.Status == "Failed" ? Color.Red : Color.Green;
            jdeConfiguration.lastResult = logRecord.Status;
            jdeConfiguration.lastRun = logRecord.Date.ToString();
            jdeConfiguration.moreInformation = "Last Result Status: " + logRecord.Status + " \n \n" +
            "Message: " + logRecord.Message + " \n";


        }

        public void SetProgress(int value, string message)
        {
            mainForm.applicationProgress.Value = value;
            mainForm.lblApplicationProgress.Text = value.ToString() + "%";
            mainForm.lblProgressLog.Text = string.Format("{0} : {1}", value == 0 ? "" : jdeConfiguration.company, message);
        }
        private void linkMoreInformation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            frmMoreInformation frm = new frmMoreInformation();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.description = jdeConfiguration.moreInformation;
            frm.ShowDialog();

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
            popupNotifier1.Image = Properties.Resources.mt_notification;
            popupNotifier1.TitleText = title;
            popupNotifier1.ContentText = description;
            popupNotifier1.Popup();


        }
    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://support.mineraltree.com");

    }
  }
}
