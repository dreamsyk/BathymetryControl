namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开TerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开dat文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ter文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xls文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dat文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.插入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.行ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.列ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.黏贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除选择单元格ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adfaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.drawToolStripMenuItem,
            this.adfaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(891, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开TerToolStripMenuItem,
            this.wToolStripMenuItem,
            this.打开dat文件ToolStripMenuItem});
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 打开TerToolStripMenuItem
            // 
            this.打开TerToolStripMenuItem.Name = "打开TerToolStripMenuItem";
            this.打开TerToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.打开TerToolStripMenuItem.Text = "Ter文件";
            this.打开TerToolStripMenuItem.Click += new System.EventHandler(this.打开TerToolStripMenuItem_Click);
            // 
            // wToolStripMenuItem
            // 
            this.wToolStripMenuItem.Name = "wToolStripMenuItem";
            this.wToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.wToolStripMenuItem.Text = "xls文件";
            this.wToolStripMenuItem.Click += new System.EventHandler(this.wToolStripMenuItem_Click);
            // 
            // 打开dat文件ToolStripMenuItem
            // 
            this.打开dat文件ToolStripMenuItem.Name = "打开dat文件ToolStripMenuItem";
            this.打开dat文件ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.打开dat文件ToolStripMenuItem.Text = "dat文件";
            this.打开dat文件ToolStripMenuItem.Click += new System.EventHandler(this.打开dat文件ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ter文件ToolStripMenuItem,
            this.xls文件ToolStripMenuItem,
            this.dat文件ToolStripMenuItem});
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.保存ToolStripMenuItem.Text = "另存为";
            // 
            // ter文件ToolStripMenuItem
            // 
            this.ter文件ToolStripMenuItem.Name = "ter文件ToolStripMenuItem";
            this.ter文件ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.ter文件ToolStripMenuItem.Text = "ter文件";
            this.ter文件ToolStripMenuItem.Click += new System.EventHandler(this.ter文件ToolStripMenuItem_Click);
            // 
            // xls文件ToolStripMenuItem
            // 
            this.xls文件ToolStripMenuItem.Name = "xls文件ToolStripMenuItem";
            this.xls文件ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.xls文件ToolStripMenuItem.Text = "xls文件";
            this.xls文件ToolStripMenuItem.Click += new System.EventHandler(this.xls文件ToolStripMenuItem_Click);
            // 
            // dat文件ToolStripMenuItem
            // 
            this.dat文件ToolStripMenuItem.Name = "dat文件ToolStripMenuItem";
            this.dat文件ToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.dat文件ToolStripMenuItem.Text = "dat文件";
            this.dat文件ToolStripMenuItem.Click += new System.EventHandler(this.dat文件ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.复制ToolStripMenuItem,
            this.剪切ToolStripMenuItem,
            this.黏贴ToolStripMenuItem,
            this.清空ToolStripMenuItem,
            this.清除选择单元格ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 插入ToolStripMenuItem
            // 
            this.插入ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.行ToolStripMenuItem,
            this.列ToolStripMenuItem});
            this.插入ToolStripMenuItem.Name = "插入ToolStripMenuItem";
            this.插入ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.插入ToolStripMenuItem.Text = "插入";
            // 
            // 行ToolStripMenuItem
            // 
            this.行ToolStripMenuItem.Name = "行ToolStripMenuItem";
            this.行ToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.行ToolStripMenuItem.Text = "行";
            this.行ToolStripMenuItem.Click += new System.EventHandler(this.行ToolStripMenuItem_Click);
            // 
            // 列ToolStripMenuItem
            // 
            this.列ToolStripMenuItem.Name = "列ToolStripMenuItem";
            this.列ToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.列ToolStripMenuItem.Text = "列";
            this.列ToolStripMenuItem.Click += new System.EventHandler(this.列ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.行ToolStripMenuItem1,
            this.列ToolStripMenuItem1});
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // 行ToolStripMenuItem1
            // 
            this.行ToolStripMenuItem1.Name = "行ToolStripMenuItem1";
            this.行ToolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
            this.行ToolStripMenuItem1.Text = "行";
            this.行ToolStripMenuItem1.Click += new System.EventHandler(this.行ToolStripMenuItem1_Click);
            // 
            // 列ToolStripMenuItem1
            // 
            this.列ToolStripMenuItem1.Name = "列ToolStripMenuItem1";
            this.列ToolStripMenuItem1.Size = new System.Drawing.Size(88, 22);
            this.列ToolStripMenuItem1.Text = "列";
            this.列ToolStripMenuItem1.Click += new System.EventHandler(this.列ToolStripMenuItem1_Click);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 剪切ToolStripMenuItem
            // 
            this.剪切ToolStripMenuItem.Name = "剪切ToolStripMenuItem";
            this.剪切ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.剪切ToolStripMenuItem.Text = "剪切";
            this.剪切ToolStripMenuItem.Click += new System.EventHandler(this.剪切ToolStripMenuItem_Click);
            // 
            // 黏贴ToolStripMenuItem
            // 
            this.黏贴ToolStripMenuItem.Name = "黏贴ToolStripMenuItem";
            this.黏贴ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.黏贴ToolStripMenuItem.Text = "黏贴";
            this.黏贴ToolStripMenuItem.Click += new System.EventHandler(this.黏贴ToolStripMenuItem_Click);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // 清除选择单元格ToolStripMenuItem
            // 
            this.清除选择单元格ToolStripMenuItem.Name = "清除选择单元格ToolStripMenuItem";
            this.清除选择单元格ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.清除选择单元格ToolStripMenuItem.Text = "清除选择单元格";
            this.清除选择单元格ToolStripMenuItem.Click += new System.EventHandler(this.清除选择单元格ToolStripMenuItem_Click);
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.drawToolStripMenuItem.Text = "全部重绘";
            this.drawToolStripMenuItem.Click += new System.EventHandler(this.drawToolStripMenuItem_Click);
            // 
            // adfaToolStripMenuItem
            // 
            this.adfaToolStripMenuItem.Name = "adfaToolStripMenuItem";
            this.adfaToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.adfaToolStripMenuItem.Text = "清空主画板";
            this.adfaToolStripMenuItem.Click += new System.EventHandler(this.adfaToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(891, 573);
            this.splitContainer1.SplitterDistance = 424;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 573);
            this.panel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 573);
            this.panel1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 598);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LandSlide    数据编辑模块 Beta 3.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开TerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开dat文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ter文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xls文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dat文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 插入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 列ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 行ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 列ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 黏贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除选择单元格ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adfaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}

