using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KINZHAL
{
    public class DoubleBufferedListBox : ListBox
    {
        public DoubleBufferedListBox()
        {
            base.DrawMode = System.Windows.Forms.DrawMode.Normal;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            UpdateStyles();
        }

        public override DrawMode DrawMode
        {
            get { return DrawMode.Normal; }
            set { }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                DrawItemState state = DrawItemState.Default;
                if (i == SelectedIndex)
                    state = DrawItemState.Selected;
                DrawItemEventArgs args = new DrawItemEventArgs(e.Graphics, Font, GetItemRectangle(i), i, state);
                OnDrawItem(args);
            }
        }
    }
}
