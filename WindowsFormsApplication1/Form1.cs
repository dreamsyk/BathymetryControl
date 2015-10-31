using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maye;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// The right side of the table area
        /// </summary>
        DataGridViewControl _rightTable;
        BathymetryControl aa;

        public Form1()
        {
            InitializeComponent();
            _rightTable = new DataGridViewControl(this.panel1, DockStyle.Fill);
            aa = new BathymetryControl(_rightTable, this.panel2, DockStyle.Fill);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        #region Events
        private void 打开TerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.DataImportText();
        }

        private void wToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.DataImportExcel();
        }

        private void 打开dat文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.DataImportDatFile(); 
        }

        private void ter文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.ExportTextFormat();
            
        }

        private void xls文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.ExportExcelFormat();
             
        }

        private void dat文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.ExportDatFormat();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.InsertRow();
        }

        private void 列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.InsertColumn();
  
        }

        private void 行ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            aa.DeleteRow();
        }

        private void 列ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            aa.DeleteColumn();  
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.Copy();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.Cut();
            
        }

        private void 黏贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.Paste();
             
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.EmptyAll();
         }

        private void 清除选择单元格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.Empty();      
        }

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult re = MessageBox.Show("你确定要退出吗？", "退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (re == DialogResult.Yes)
            {
                File.Delete("cursor.jpg");
                e.Cancel = false;
            }
            else e.Cancel = true;
        }

        private void adfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aa.MainPattern.Clear();
        }

    
        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {

            aa.LoadPattern();

        }

 
        private void Form1_Shown(object sender, EventArgs e)
        {
 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

     }
 

 
}
