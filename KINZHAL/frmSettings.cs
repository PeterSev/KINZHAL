using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KINZHAL
{
    public partial class frmSettings : Form
    {
        public frmMain _frmMain;

        VideoDeviceInfo videoDeviceInfo;
        public frmSettings(frmMain frm)
        {
            InitializeComponent();

            _frmMain = frm;

            Init();
        }

        void Init()
        {
            cmbDevices.DataSource = _frmMain.sdi.FindDevices();

        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmSettings.Hide();
        }

        

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVideoDeviceConnect_Click(object sender, EventArgs e)
        {
            if (_frmMain.sdi.ConnectCapture(cmbDevices.SelectedIndex, lstBoxVideoInputs.SelectedIndex, lstBoxSizeVideo.SelectedIndex))
            {
                btnVideoDeviceConnect.Enabled = false;
                btnVideoDeviceDisconnect.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ошибка подключения к устройству", "МРО", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnVideoDeviceConnect.Enabled = true;
                btnVideoDeviceDisconnect.Enabled = false;
            }
        }

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstBoxVideoInputs.DataSource = null;
            lstBoxSizeVideo.DataSource = null;
            videoDeviceInfo = _frmMain.sdi.selectIndexChanged(cmbDevices.SelectedIndex);
            lstBoxVideoInputs.DataSource = videoDeviceInfo.lstPhysicalOuts;
            lstBoxSizeVideo.DataSource = videoDeviceInfo.lstSizeVideo;
        }

        private void btnVideoDeviceDisconnect_Click(object sender, EventArgs e)
        {
            if (!_frmMain.sdi.DisconnectCapture())
            {
                MessageBox.Show("Возникла ошибка в процессе отключения устройства!", "МРО", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnVideoDeviceConnect.Enabled = true;
            btnVideoDeviceDisconnect.Enabled = false;
        }

        private void chkFullMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullMode.Checked)
            {
                _frmMain.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                _frmMain.AutoScroll = false;
            }
            else
            {
                _frmMain.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                _frmMain.AutoScroll = true;
            }
        }

        private void frmSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
