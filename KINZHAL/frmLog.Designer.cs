namespace KINZHAL
{
    partial class frmLog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.очиститьЛогToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.скопироватьВБуферToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.ContextMenuStrip = this.contextMenuStrip1;
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lstLog.ItemHeight = 16;
            this.lstLog.Location = new System.Drawing.Point(0, 0);
            this.lstLog.Name = "lstLog";
            this.lstLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstLog.Size = new System.Drawing.Size(1442, 346);
            this.lstLog.TabIndex = 0;
            this.lstLog.SelectedIndexChanged += new System.EventHandler(this.lstLog_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьЛогToolStripMenuItem,
            this.скопироватьВБуферToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(194, 48);
            // 
            // очиститьЛогToolStripMenuItem
            // 
            this.очиститьЛогToolStripMenuItem.Name = "очиститьЛогToolStripMenuItem";
            this.очиститьЛогToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.очиститьЛогToolStripMenuItem.Text = "Очистить лог";
            this.очиститьЛогToolStripMenuItem.Click += new System.EventHandler(this.очиститьЛогToolStripMenuItem_Click);
            // 
            // скопироватьВБуферToolStripMenuItem
            // 
            this.скопироватьВБуферToolStripMenuItem.Name = "скопироватьВБуферToolStripMenuItem";
            this.скопироватьВБуферToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.скопироватьВБуферToolStripMenuItem.Text = "Скопировать в буфер";
            this.скопироватьВБуферToolStripMenuItem.Click += new System.EventHandler(this.скопироватьВБуферToolStripMenuItem_Click);
            // 
            // frmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1442, 346);
            this.Controls.Add(this.lstLog);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmLog";
            this.Text = "Лог";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLog_FormClosing);
            this.Load += new System.EventHandler(this.frmLog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLog_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem очиститьЛогToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem скопироватьВБуферToolStripMenuItem;

    }
}