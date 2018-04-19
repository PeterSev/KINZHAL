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
    public partial class frmDebug : Form
    {
        public frmMain _frmMain;
        public int iCnt1538 = 0, iCnt1539 = 0, iCnt1366 = 0, iCntZaprosBU = 0, iCntAckBU = 0, iCntZaprosSELECTED = 0, iCntAckSELECTED = 0, iCnt1540;
        public frmDebug()
        {
            InitializeComponent();
        }

        private void frmDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmDebug.Hide();
        }

        private void frmDebug_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
