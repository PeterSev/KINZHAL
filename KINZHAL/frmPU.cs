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
    public partial class frmPU : Form
    {
        public frmMain _frmMain;
        public frmPU()
        {
            InitializeComponent();
        }

        private void frmPU_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmPU.Hide();
        }

        private void frmPU_Load(object sender, EventArgs e)
        {

        }

        private void chkPU_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.Checked = false;
        }


        private void frmPU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            _frmMain.A5_23_ResetError();
        }

        private void buttonMouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            uint code = 0;
            switch (btn.Name.Substring(3))
            {
                case "F1":      code = 26; break;
                case "F2":      code = 27; break;
                case "F3":      code = 28; break;
                case "F4":      code = 29; break;
                case "F5":      code = 30; break;
                case "F6":      code = 31; break;
                case "F7":      code = 32; break;
                case "Left":    code = 21; break;
                case "Right":   code = 22; break;
                case "Up":      code = 23; break;
                case "Down":    code = 24; break;
                case "Menu":    code = 4; break;
            }

            _frmMain.A5_23_Button(code, true);
        }

        private void buttonMouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            uint code = 0;
            switch (btn.Name.Substring(3))
            {
                case "F1": code = 26; break;
                case "F2": code = 27; break;
                case "F3": code = 28; break;
                case "F4": code = 29; break;
                case "F5": code = 30; break;
                case "F6": code = 31; break;
                case "F7": code = 32; break;
                case "Left": code = 21; break;
                case "Right": code = 22; break;
                case "Up": code = 23; break;
                case "Down": code = 24; break;
                case "Menu": code = 4; break;
            }
            _frmMain.A5_23_Button(code, false);
        }

        private void btnWriteParametr_Click(object sender, EventArgs e)
        {
            _frmMain.A5_23_Write();
        }

        private void btnReadParametr_Click(object sender, EventArgs e)
        {
            _frmMain.A5_24_AskForParametres();
        }
    }
}
