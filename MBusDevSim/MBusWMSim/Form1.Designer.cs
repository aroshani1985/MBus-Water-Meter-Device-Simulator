namespace MBusWMSim
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tlb_main = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtr_main = new System.Windows.Forms.RichTextBox();
            this.tlb_sp = new System.Windows.Forms.TableLayoutPanel();
            this.cbx_spx = new System.Windows.Forms.ComboBox();
            this.btn_find_sp = new System.Windows.Forms.Button();
            this.btn_sp_con = new System.Windows.Forms.Button();
            this.btn_sp_discon = new System.Windows.Forms.Button();
            this.tlb_main.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tlb_sp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlb_main
            // 
            this.tlb_main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlb_main.ColumnCount = 1;
            this.tlb_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlb_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlb_main.Controls.Add(this.menuStrip1, 0, 0);
            this.tlb_main.Controls.Add(this.txtr_main, 0, 3);
            this.tlb_main.Controls.Add(this.tlb_sp, 0, 1);
            this.tlb_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlb_main.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tlb_main.Location = new System.Drawing.Point(0, 0);
            this.tlb_main.Name = "tlb_main";
            this.tlb_main.RowCount = 4;
            this.tlb_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlb_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlb_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlb_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlb_main.Size = new System.Drawing.Size(928, 550);
            this.tlb_main.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(1, 1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(926, 20);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 16);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // txtr_main
            // 
            this.txtr_main.BackColor = System.Drawing.Color.Black;
            this.txtr_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtr_main.ForeColor = System.Drawing.Color.Lime;
            this.txtr_main.Location = new System.Drawing.Point(4, 358);
            this.txtr_main.Name = "txtr_main";
            this.txtr_main.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtr_main.Size = new System.Drawing.Size(920, 188);
            this.txtr_main.TabIndex = 1;
            this.txtr_main.Text = "";
            // 
            // tlb_sp
            // 
            this.tlb_sp.ColumnCount = 4;
            this.tlb_sp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlb_sp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlb_sp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlb_sp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlb_sp.Controls.Add(this.cbx_spx, 0, 0);
            this.tlb_sp.Controls.Add(this.btn_find_sp, 1, 0);
            this.tlb_sp.Controls.Add(this.btn_sp_con, 2, 0);
            this.tlb_sp.Controls.Add(this.btn_sp_discon, 3, 0);
            this.tlb_sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlb_sp.Location = new System.Drawing.Point(4, 25);
            this.tlb_sp.Name = "tlb_sp";
            this.tlb_sp.RowCount = 1;
            this.tlb_sp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlb_sp.Size = new System.Drawing.Size(920, 34);
            this.tlb_sp.TabIndex = 2;
            // 
            // cbx_spx
            // 
            this.cbx_spx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbx_spx.FormattingEnabled = true;
            this.cbx_spx.Location = new System.Drawing.Point(3, 3);
            this.cbx_spx.Name = "cbx_spx";
            this.cbx_spx.Size = new System.Drawing.Size(362, 29);
            this.cbx_spx.TabIndex = 0;
            // 
            // btn_find_sp
            // 
            this.btn_find_sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_find_sp.Location = new System.Drawing.Point(371, 3);
            this.btn_find_sp.Name = "btn_find_sp";
            this.btn_find_sp.Size = new System.Drawing.Size(178, 28);
            this.btn_find_sp.TabIndex = 1;
            this.btn_find_sp.Text = "Find COM Ports";
            this.btn_find_sp.UseVisualStyleBackColor = true;
            // 
            // btn_sp_con
            // 
            this.btn_sp_con.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_sp_con.Location = new System.Drawing.Point(555, 3);
            this.btn_sp_con.Name = "btn_sp_con";
            this.btn_sp_con.Size = new System.Drawing.Size(178, 28);
            this.btn_sp_con.TabIndex = 1;
            this.btn_sp_con.Text = "Connect";
            this.btn_sp_con.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_sp_con.UseVisualStyleBackColor = true;
            // 
            // btn_sp_discon
            // 
            this.btn_sp_discon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_sp_discon.Location = new System.Drawing.Point(739, 3);
            this.btn_sp_discon.Name = "btn_sp_discon";
            this.btn_sp_discon.Size = new System.Drawing.Size(178, 28);
            this.btn_sp_discon.TabIndex = 1;
            this.btn_sp_discon.Text = "Disconnect";
            this.btn_sp_discon.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 550);
            this.Controls.Add(this.tlb_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "M-Bus Water Meter Device Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tlb_main.ResumeLayout(false);
            this.tlb_main.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tlb_sp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlb_main;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.RichTextBox txtr_main;
        private System.Windows.Forms.TableLayoutPanel tlb_sp;
        private System.Windows.Forms.ComboBox cbx_spx;
        private System.Windows.Forms.Button btn_find_sp;
        private System.Windows.Forms.Button btn_sp_con;
        private System.Windows.Forms.Button btn_sp_discon;
    }
}
