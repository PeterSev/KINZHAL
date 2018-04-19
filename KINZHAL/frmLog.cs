using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using UcanDotNET;

namespace KINZHAL
{
    public partial class frmLog : Form
    {
        public frmMain _frmMain;
        Int16 ListCount;
        //DoubleBufferedListBox dlstLog;
        public frmLog()
        {
            InitializeComponent();
            ListCount = 0;

            //lstLog.GetType().InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, lstLog, new object[] { true });

            /*dlstLog = new DoubleBufferedListBox();
            dlstLog.Dock = DockStyle.Fill;
            dlstLog.Font = new Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dlstLog.SelectionMode = SelectionMode.MultiExtended;
            dlstLog.ForeColor = Color.Black;*/

            //this.Controls.Add(dlstLog);

            //dlstLog.Parent = this;
        }

        private void frmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _frmMain._frmLog.Hide();
        }

        private void очиститьЛогToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
            ListCount = 0;
        }

        public void AddToList(USBcanServer.tCanMsgStruct canMsg, bool b)
        {

            ListCount++;

            if (ListCount > 9999)
            {
                Invoke((MethodInvoker)delegate
                {
                    lstLog.Items.Clear();
                    ListCount = 1;
                });
            }

            byte sAddr = (byte)(canMsg.m_dwID & 0x3F);
            byte dAddr = (byte)((canMsg.m_dwID >> 6) & 0x3F);
            ushort descr = (ushort)((canMsg.m_dwID >> 12) & 0xFFF);
            byte priority = (byte)((canMsg.m_dwID >> 24) & 0x1F);

            if (descr == 1280) _frmMain.iCnt1280_++;
            Invoke((MethodInvoker)delegate
            {
                _frmMain._frmDebug.lblcnt1280_.Text = _frmMain.iCnt1280_.ToString();
            });

            string time = canMsg.m_dwTime.ToString();

            /*for (int i = time.Length; i > 0; i-=3)
            {
                if (i == time.Length) continue;
                time = time.Insert(i, " ");
            }*/

            Array.Resize(ref canMsg.m_bData, canMsg.m_bDLC);
            

            string str = String.Format("{0}{1}{2}{3}{4}{5}{6}Pr - {7}DLC - {8}ID - {9}T = {10}{11}",
                ((DEVICE_ADDR)sAddr).ToString().PadRight(8),
                ("("+sAddr.ToString()+")").PadRight(7),
                b?">>>".PadRight(5):"<<<".PadRight(5),
                ((DEVICE_ADDR)dAddr).ToString().PadRight(8),
                ("("+dAddr.ToString()+")").PadRight(7),
                ((DESCRIPTORS)descr).ToString().PadRight(50),
                ("("+descr.ToString()+")").PadRight(10),
                priority.ToString().PadRight(5),
                canMsg.m_bDLC.ToString().PadRight(5),
                canMsg.m_dwID.ToString("X").PadRight(10),
                time.PadRight(10),
                BitConverter.ToString(canMsg.m_bData));
            
            string date = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            string outstr = string.Format("{0:0000}:  {1}  {2}", ListCount, date, str);

            if (InvokeRequired)
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        lstLog.BeginUpdate();
                        lstLog.Items.Add(outstr);
                        lstLog.EndUpdate();
                        lstLog.ClearSelected();
                        lstLog.SelectedIndex = lstLog.Items.Count - 1;

                        //lstLog.ValueMember = canMsg.m_dwTime.ToString();
                        
                    });
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try
                {
                    lstLog.BeginUpdate();
                    lstLog.Items.Add(outstr);
                    lstLog.EndUpdate();
                    lstLog.ClearSelected();
                    lstLog.SelectedIndex = lstLog.Items.Count - 1;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        public void AddToList(string str)
        {
            ListCount++;

            if (ListCount > 9999)
            {
                lstLog.Items.Clear();
                ListCount = 1;
            }

            string date = String.Format("{0:00}:{1:00}:{2:00}:{3:000}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            string outstr = string.Format("{0:0000}:  {1}  {2}", ListCount, date, str);

            if (InvokeRequired)
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        lstLog.BeginUpdate();
                        lstLog.Items.Add(outstr);
                        lstLog.EndUpdate();
                        lstLog.ClearSelected();
                        lstLog.SelectedIndex = lstLog.Items.Count - 1;
                    });
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                lstLog.BeginUpdate();
                lstLog.Items.Add(outstr);
                lstLog.EndUpdate();
                lstLog.ClearSelected();
                lstLog.SelectedIndex = lstLog.Items.Count - 1;
            }
        }

        private void скопироватьВБуферToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            foreach (object o in lstLog.SelectedItems) s += o.ToString() + "\r\n";
            Clipboard.SetText(s);
        }

        private void frmLog_Load(object sender, EventArgs e)
        {
            /*this.SetStyle(ControlStyles.DoubleBuffer |
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint,
              true);
            this.UpdateStyles();*/
        }

        private void lstLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Считаем время между двумя выбранными строками в логе. Ищет строку после символа "=", конвертирует в число и вычитает по модулю. Трай используем для контроля правильности выбранных строк.
            string s1 = "", s2 = "";
            if (lstLog.SelectedIndices.Count == 2)
            {
                try
                {
                    s1 = lstLog.GetItemText(lstLog.SelectedItems[0]);
                    s1 = s1.Substring(s1.IndexOf('=') + 2);
                    s1 = s1.Substring(0, s1.IndexOf(' '));

                    s2 = lstLog.GetItemText(lstLog.SelectedItems[1]);
                    s2 = s2.Substring(s2.IndexOf('=') + 2);
                    s2 = s2.Substring(0, s2.IndexOf(' '));

                    this.Text = "Время между выбранными сигналами: " + (Math.Abs((Convert.ToInt32(s2) - Convert.ToInt32(s1)))).ToString() + " мс";
                }
                catch { };
            }
            else
                this.Text = "Лог";
        }

        private void frmLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
