﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KINZHAL
{
    public partial class frmAbout : Form
    {
        public frmMain _frmMain;
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmAbout.Hide();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = _frmMain.version.ToString(CultureInfo.InvariantCulture); 
        }
    }
}
