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
            btnA5_14.Click += _frmMain.btnA5_14_Click;
            btnA5_16.Click += _frmMain.btnA5_16_Click;
            btnA5_17.Click += _frmMain.btnA5_17_Click;
            btnA5_15.Click += _frmMain.btnA5_15_Click;
            btnA5_12.Click += _frmMain.btnA5_12_Click;
            btnA5_11.Click += _frmMain.btnA5_11_Click;
            btnA5_18.Click += _frmMain.btnA5_18_Click;
            btnA5_19.Click += _frmMain.btnA5_19_Click;

            /*chkA5_20_Up.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Down.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Right.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Left.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Menu.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Obogrev.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Focus.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Usil.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Day.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Marka.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Uvel.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Umen.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Polar.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;
            chkA5_20_Svetofilter.CheckedChanged += _frmMain.chkA5_20_CheckedChanged;*/

            chkA5_20_Up.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Up.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Down.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Down.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Right.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Right.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Left.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Left.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Menu.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Menu.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Obogrev.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Obogrev.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Focus.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Focus.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Usil.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Usil.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Day.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Day.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Marka.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Marka.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Uvel.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Uvel.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Umen.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Umen.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Polar.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Polar.MouseUp += _frmMain.chkPU_MouseUp;
            chkA5_20_Svetofilter.MouseDown += _frmMain.chkPU_MouseDown;
            chkA5_20_Svetofilter.MouseUp += _frmMain.chkPU_MouseUp;

            radA5_20_0.Click += _frmMain.radA5_20_Click;
            radA5_20_Dezh.Click += _frmMain.radA5_20_Click;
            radA5_20_I.Click += _frmMain.radA5_20_Click;
        }

        private void chkPU_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.Checked = false;
        }

        private void btnMenuTehn_MouseDown(object sender, MouseEventArgs e)
        {
            chkA5_20_Down.Tag = true;
            chkA5_20_Menu.Tag = true;

            _frmMain.A5_20_MenuTehnAndInzh();
        }

        private void btnMenuTehn_MouseUp(object sender, MouseEventArgs e)
        {
            chkA5_20_Down.Tag = false;
            chkA5_20_Menu.Tag = false;

            _frmMain.A5_20_MenuTehnAndInzh();
        }

        private void btnMenuInzh_MouseDown(object sender, MouseEventArgs e)
        {
            chkA5_20_Menu.Tag = true;
            chkA5_20_Left.Tag = true;
            chkA5_20_Right.Tag = true;

            _frmMain.A5_20_MenuTehnAndInzh();
        }

        private void btnMenuInzh_MouseUp(object sender, MouseEventArgs e)
        {
            chkA5_20_Menu.Tag = false;
            chkA5_20_Left.Tag = false;
            chkA5_20_Right.Tag = false;

            _frmMain.A5_20_MenuTehnAndInzh();
        }

        private void frmPU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
