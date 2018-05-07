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
    public partial class frmCoord : Form
    {
        public frmMain _frmMain;
        public frmCoord()
        {
            InitializeComponent();
        }

        private void frmCoord_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmCoord.Hide();
        }

        private void btnZaprosCoord_Click(object sender, EventArgs e)
        {
            _frmMain.ZaprosCoord();
        }
    }
}
