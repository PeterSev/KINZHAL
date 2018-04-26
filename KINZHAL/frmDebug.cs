using System.Windows.Forms;

namespace KINZHAL
{
    public partial class frmDebug : Form
    {
        public frmMain _frmMain;
        public int iCnt1538 = 0, iCnt1546 = 0, iCnt1554 = 0, iCntZaprosBU = 0, iCntAckBU = 0, iCntZaprosSELECTED = 0, iCntAckSELECTED = 0, iCnt1540;
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
