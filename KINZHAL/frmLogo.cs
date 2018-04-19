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
    public partial class frmLogo : Form
    {
        public frmLogo()
        {
            InitializeComponent();
            this.TransparencyKey = Color.White;
            this.BackColor = Color.White;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += .005;
            if (this.Opacity == 1)
            {
                timer1.Stop();
            }
        }
    }
}
