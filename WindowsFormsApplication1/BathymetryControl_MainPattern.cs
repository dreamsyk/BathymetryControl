using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
 

namespace WindowsFormsApplication1
{
    class DataGridViewControl : DataGridView
    {
        #region Prpperties
        /// <summary>
        /// DataGridView Data storage array in the table
        /// </summary>
        private double[,] _data;
        public double[,] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// DataGridView table columns 
        /// </summary>
        private int ncols;
        public int Ncols
        {
            get { return ncols; }
            set { ncols = value; this.ColumnCount = value; }
        }

        /// <summary>
        ///DataGridView table rows
        /// </summary>
        private int nrows;
        public int Nrows
        {
            get { return nrows; }
            set { nrows = value; this.RowCount = value; }
        }

        /// <summary>
        /// The lower left corner coordinates
        /// </summary>
        private double xllcorner;
        public double Xllcorner
        {
            get { return xllcorner; }
            set { xllcorner = value; }
        }

        /// <summary>
        /// The lower left corner of the ordinate
        /// </summary>
        private double yllcorner;
        public double Yllcorner
        {
            get { return yllcorner; }
            set { yllcorner = value; }
        }

        /// <summary>
        /// Cell Size
        /// </summary>
        private double cellsize;
        public double Cellsize
        {
            get { return cellsize; }
            set { cellsize = value; }
        }

        /// <summary>
        /// NODATA value
        /// </summary>
        private long NODATA_value;
        public long NODATA_Value
        {
            get { return NODATA_value; }
            set { NODATA_value = value; }
        }



        /// <summary>
        /// Context Menu Strip
        /// </summary>
        private ContextMenuStrip cms;
        public ContextMenuStrip Cms
        {
            get { return cms; }
            set { cms = value; }
        }

        /// <summary>
        /// Data Refresh
        /// </summary>
        private Timer timer1;


        #region File Type
        private const int _TXT = 1;
        private const int _TER = 2;
        private const int _XLS = 3;
        private const int _DAT = 4;
        #endregion

        #endregion

        #region Constructor

        public DataGridViewControl()
        {
            try
            {

                Screen screen = Screen.PrimaryScreen;
                _Init(screen.Bounds.Width, screen.Bounds.Height);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// According to the parent container and DockStyle to instantiate the object
        /// </summary>
        /// <param name="Controlled">Name of the parent container</param>
        /// <param name="Style">DockStyle</param>
        public DataGridViewControl(Control Controlled, DockStyle Style)
        {
            try
            {

                this.Parent = Controlled;
                this.Dock = Style;

                Screen screen = Screen.PrimaryScreen;
                _Init(screen.Bounds.Width, screen.Bounds.Height);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Member initialization function
        /// </summary>
        /// <param name="width">The width of the screen</param>
        /// <param name="height">The height of the screen</param>
        private void _Init(int width, int height)
        {
            try
            {

                Xllcorner = 1.25;
                Yllcorner = 1.25;
                Cellsize = 2.5;
                NODATA_Value = -9999;


                #region  DataGridView 表格相关属性初始化

                Ncols = (int)width / 80;
                Nrows = (int)height / 20;

                _data = new double[Nrows, Ncols];

                #region 设置列标题居中显示

                DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
                headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                this.ColumnHeadersDefaultCellStyle = headerStyle;

                #endregion


                for (int i = 0; i < Ncols; i++)
                {
                    this.Columns[i].HeaderText = Convert.ToString(i + 1);
                    this.Columns[i].Width = 80;
                    this.Columns[i].ReadOnly = false;

                }



                for (int i = 0; i < Nrows; i++)
                {
                    this.Rows[i].Height = 20;
                    this.Rows[i].ReadOnly = false;

                }

                for (int i = 0; i < Ncols; i++)
                    this.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                #endregion

                #region DataGridView 右击菜单的初始化

                cms = new ContextMenuStrip();
                cms.Items.Add("Cut"); //1
                cms.Items.Add("Copy");//2
                cms.Items.Add("Paste");//3
                cms.Items.Add("Empty");//4
                cms.Items.Add("Empty All");//5
                cms.Items.Add("Insert Row");//6
                cms.Items.Add("Delete Row");//7
                cms.Items.Add("Insert Column");//8
                cms.Items.Add("Delete Column");//9
                cms.Items.Add("Data Import Text");
                cms.Items.Add("Data Import Excel");
                cms.Items.Add("Data Import Dat");
                cms.Items.Add("Export Text Format");
                cms.Items.Add("Export Excel Format");
                cms.Items.Add("Export Dat Format");


                this.MouseClick += new MouseEventHandler(DataGridViewControl_MouseClick);
                this.cms.ItemClicked += new ToolStripItemClickedEventHandler(cms_ItemClicked);

                #endregion


                timer1 = new Timer();


                timer1.Start();
                timer1.Interval = 1500;
                timer1.Tick += new EventHandler(_Timer1_Tick);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        #endregion

        #region Public Function

        #region Cut ,Copy ,Paste,Empty


        /// <summary>
        /// Cut Selected Cell Data
        /// </summary>
        public void Cut()
        {
            try
            {

                Copy();
                Empty();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Copy Selected Cell Data
        /// </summary>
        public void Copy()
        {
            try
            {

                cms.Visible = false;


                long selectedNums = 0;
                nums = 0;
                foreach (DataGridViewCell every in this.SelectedCells)
                {
                    selectedNums++;
                }

                DataIndex = new Point[selectedNums];
                DataTemp = new double[selectedNums];


                foreach (DataGridViewCell every in this.SelectedCells)
                {
                    DataIndex[nums] = new Point(every.RowIndex, every.ColumnIndex);
                    if (every.Value != null)
                    {

                        DataTemp[nums++] = Convert.ToDouble(every.Value.ToString());

                    }
                    else
                    {

                        DataTemp[nums++] = -999999999999999;

                    }

                }


            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        /// <summary>
        /// Paste copyed or cuted Data
        /// </summary>                                                                                          
        public void Paste()
        {
            try
            {

                cms.Visible = false;
                _IndexSort(DataIndex, DataTemp, nums);

                Point basedpoint = _GetBasedPoint();

                Point[] away = new Point[nums];

                _GetPointsAway(away);

                for (int i = 0; i < nums; i++)
                {
                    if (DataTemp[i] == -999999999999999) continue;
                    this.Rows[basedpoint.X + away[i].X].Cells[basedpoint.Y + away[i].Y].Value = DataTemp[i];
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Empty Data

        /// <summary>
        /// Empty Selected Cell
        /// </summary>
        public void Empty()
        {
            cms.Visible = false;
            try
            {
                foreach (DataGridViewCell every in this.SelectedCells)
                {
                    every.Value = null;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Clear all lattice
        /// </summary>
        public void EmptyAll()
        {
            cms.Visible = false;
            try
            {
                for (int i = 0; i < Nrows; i++)
                    for (int j = 0; j < Ncols; j++)
                    {
                        this.Rows[i].Cells[j].Value = null;
                    }


            }
            catch (Exception ee)
            {
                cms.Visible = false;
                MessageBox.Show(ee.Message);
            }

        }

        #endregion



        #endregion

        #region Rows and columns add or delete operation

        /// <summary>
        /// Insert  Row
        /// </summary>
        public void InsertRow()
        {
            cms.Visible = false;
            try
            {
                cms.Visible = false;
                this.Rows.Insert(this.CurrentCell.RowIndex + 1, new DataGridViewRow());
                this.Rows[this.CurrentCell.RowIndex + 1].Height = 20;

                Nrows++;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Delete Row
        /// </summary>
        public void DeleteRow()
        {
            cms.Visible = false;
            try
            {
                cms.Visible = false;
                this.Rows.RemoveAt(this.CurrentCell.RowIndex);
                Nrows--;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Insert Column
        /// </summary>
        public void InsertColumn()
        {
            cms.Visible = false;
            try
            {
                cms.Visible = false;
                this.Columns.Insert(this.CurrentCell.ColumnIndex + 1, new DataGridViewTextBoxColumn());
                this.Columns[this.CurrentCell.ColumnIndex + 1].Width = 80;
                Ncols++;

                _OnColumnPostPaint();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Delete _DeleteRow
        /// </summary>
        public void DeleteColumn()
        {
            cms.Visible = false;
            try
            {
                cms.Visible = false;
                this.Columns.RemoveAt(this.CurrentCell.ColumnIndex);

                Ncols--;

                _OnColumnPostPaint();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region  Data Import Form Files

        #region Import Text

        /// <summary>
        /// Data Import Text
        /// </summary>
        public void DataImportText()
        {
            try
            {
                string filename = "";
                if (!_GetOpenFileName(ref filename, _TER)) return;
                _LoadTXTData(filename);
                _OnColumnPostPaint();
                _DisplayData();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        #endregion

        #region Import Excel
        /// <summary>
        /// Data Import Excel
        /// </summary>
        public void DataImportExcel()
        {
            try
            {
                string filename = "";
                if (!_GetOpenFileName(ref filename, _XLS)) return;
                _LoadXLSData(filename);
                _OnColumnPostPaint();
                _DisplayData();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Import Dat File

        /// <summary>
        /// Data Import Dat File
        /// </summary>
        public void DataImportDatFile()
        {
            try
            {

                string filename = "";
                if (!_GetOpenFileName(ref filename, _DAT)) return;
                _LoadDatData(filename);
                _OnColumnPostPaint();
                _DisplayData();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        #endregion

        #endregion

        #region Export File Format

        #region XLS Format

        /// <summary>
        /// Export Excel Format
        /// </summary>
        public void ExportExcelFormat()
        {
            try
            {
                string filename = "";

                if (!_GetSaveFileName(ref filename, _XLS)) return;

                FileStream fs = File.OpenWrite(filename);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine("#This is terrain data file");
                sw.WriteLine(string.Format("ncols({0})\t{1}", '\u694D', Ncols));
                sw.WriteLine(string.Format("nrows({0})\t{1}", '\u5CF4', Nrows));
                sw.WriteLine("xllcorner\t" + Xllcorner);
                sw.WriteLine("yllcorner\t" + Yllcorner);
                sw.WriteLine("cellsize\t" + Cellsize);
                sw.WriteLine("NODATA_value\t" + NODATA_Value);

                for (int j = 0; j < Nrows; j++)
                {
                    for (int i = 0; i < Ncols; i++)
                    {
                        sw.Write("{0}\t", Data[j, i]);
                    }
                    sw.WriteLine("");
                }

                sw.Close();
                fs.Close();

                MessageBox.Show("The data in XLS format saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        #region Text Format
        /// <summary>
        /// Export Text Format
        /// </summary>
        public void ExportTextFormat()
        {
            try
            {
                string filename = "";

                if (!_GetSaveFileName(ref filename, _TER)) return;

                FileStream fs = File.OpenWrite(filename);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine("#This is terrain data file");
                sw.WriteLine(" ncols(楍)          " + Ncols);
                sw.WriteLine(" nrows(峴)          " + Nrows);
                sw.WriteLine(" xllcorner          " + Xllcorner);
                sw.WriteLine(" yllcorner          " + Yllcorner);
                sw.WriteLine(" cellsize           " + Cellsize);
                sw.WriteLine("NODATA_value      " + NODATA_Value);



                for (int j = 0; j < Nrows; j++)
                {
                    for (int i = 0; i < Ncols; i++)
                    {
                        sw.Write("  {0}", Data[j, i]);
                    }
                    sw.WriteLine("");
                }

                sw.Close();
                fs.Close();

                MessageBox.Show("The data in TXT format saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region  Dat Format

        /// <summary>
        /// Export Dat Format
        /// </summary>
        public void ExportDatFormat()
        {
            try
            {
                string filename = "";

                if (!_GetSaveFileName(ref filename, _DAT)) return;

                FileStream fs = File.OpenWrite(filename);
                StreamWriter sw = new StreamWriter(fs);


                for (int i = 0; i < Nrows; i++)
                {
                    for (int j = 0; j < Ncols; j++)
                        sw.Write(String.Format("{0} ", Data[i, j]));

                    sw.WriteLine();
                }

                sw.Close();
                fs.Close();

                MessageBox.Show("The data in Dat format saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Private or Protected Function


        #region Events

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void _Timer1_Tick(object sender, EventArgs e)
        {
            int i = 0, j = 0;

            try
            {

                Ncols = this.ColumnCount;
                Nrows = this.RowCount;
                _data = new double[Nrows, Ncols];

                for (i = 0; i < Nrows; i++)
                {
                    for (j = 0; j < Ncols; j++)
                    {
                        if (this.Rows[i].Cells[j].Value != null)
                            Data[i, j] = Convert.ToDouble(this.Rows[i].Cells[j].Value.ToString());
                        else Data[i, j] = 0;
                    }

                }
            }
            catch (System.Exception ex)
            {
                this.Rows[i].Cells[j].Value = DBNull.Value;
            }
        }

        /// <summary>
        ///   Draw  Row HeaderText Function
        /// </summary>
        /// <param name="e">DataGridView Row Post Paint EventArgs</param>
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            try
            {

                base.OnRowPostPaint(e);
                Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, this.RowHeadersWidth, e.RowBounds.Height);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), this.RowHeadersDefaultCellStyle.Font, rectangle, this.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.HorizontalCenter);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///The column headings refresh Function
        /// </summary>
        private void _OnColumnPostPaint()
        {
            try
            {
                for (int i = 0; i < Ncols; i++)
                    this.Columns[i].HeaderText = Convert.ToString(i + 1);

                for (int i = 0; i < Ncols; i++)
                    this.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }


        }


        /// <summary>
        ///  The mouse to click the menu to the right the eject key events
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void DataGridViewControl_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                Point mouselocation = new Point(e.X, e.Y);

                if (e.Button == MouseButtons.Right)
                {
                    cms.Show(this, mouselocation);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Right click menu click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        void cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {

                if (e.ClickedItem == cms.Items[0])
                {
                    //Cut
                    Cut();
                }
                else if (e.ClickedItem == cms.Items[1])
                {
                    //Copy
                    Copy();
                }
                else if (e.ClickedItem == cms.Items[2])
                {
                    //Paste
                    Paste();
                }
                else if (e.ClickedItem == cms.Items[3])
                {
                    //Empty
                    Empty();

                }
                else if (e.ClickedItem == cms.Items[4])
                {
                    //Empty All
                    EmptyAll();
                }
                else if (e.ClickedItem == cms.Items[5])
                {
                    //Insert Row
                    InsertRow();
                }
                else if (e.ClickedItem == cms.Items[6])
                {

                    //Delete Row
                    DeleteRow();
                }
                else if (e.ClickedItem == cms.Items[7])
                {
                    //Insert Column
                    InsertColumn();

                }
                else if (e.ClickedItem == cms.Items[8])
                {
                    //Delete Column
                    DeleteColumn();
                }
                else if (e.ClickedItem == cms.Items[9])
                {
                    // Data Import Text
                    DataImportText();
                }
                else if (e.ClickedItem == cms.Items[10])
                {
                    //Data Import Excel
                    DataImportExcel();
                }
                else if (e.ClickedItem == cms.Items[11])
                {
                    //Data Import DatFile
                    DataImportDatFile();
                }
                else if (e.ClickedItem == cms.Items[12])
                {
                    //Export Text Format
                    ExportTextFormat();
                }
                else if (e.ClickedItem == cms.Items[13])
                {
                    //Export Excel Format
                    ExportExcelFormat();

                }
                else if (e.ClickedItem == cms.Items[14])
                {
                    //Export Dat Format
                    ExportDatFormat();
                }


            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        #endregion

        #region  Menu Strip

        #region File Name Related

        /// <summary>
        /// Get Open File Name
        /// </summary>
        /// <param name="filename">File Name</param>
        /// <returns>Whether has selected files</returns>
        private bool _GetOpenFileName(ref string filename, int FileType)
        {
            try
            {
                cms.Visible = false;
                OpenFileDialog f1 = new OpenFileDialog();
                f1.FileName = "";

                #region File Filter Select

                switch (FileType)
                {
                    case 1:
                        f1.Filter = "TXT File(*.txt)|*.txt"; f1.Title = "TXT File Import";
                        break;

                    case 2:
                        f1.Filter = "TER File(*.ter)|*.ter"; f1.Title = "TER File Import";
                        break;

                    case 3:
                        f1.Filter = "XLS File(*.xls)|*.xls"; f1.Title = "XLS File Import";
                        break;
                    case 4:
                        f1.Filter = "DAT File(*.dat)|*.dat"; f1.Title = "DAT File Import";
                        break;
                }

                #endregion

                f1.ShowDialog();
                if (f1.FileName != "")
                {
                    filename = f1.FileName;
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Get Save File Name
        /// </summary>
        /// <param name="filename">File Name</param>
        /// <returns>Whether they have been input file name</returns>
        private bool _GetSaveFileName(ref string filename, int FileType)
        {
            try
            {
                cms.Visible = false;
                SaveFileDialog f1 = new SaveFileDialog();
                f1.FileName = "";

                #region File Filter Select

                switch (FileType)
                {
                    case 1:
                        f1.Filter = "TXT File(*.txt)|*.txt"; f1.Title = "Export TXT File Format";
                        break;

                    case 2:
                        f1.Filter = "TER File(*.ter)|*.ter"; f1.Title = "Export TER File Format";
                        break;

                    case 3:
                        f1.Filter = "XLS File(*.xls)|*.xls"; f1.Title = "Export XLS File Format";
                        break;
                    case 4:
                        f1.Filter = "DAT File(*.dat)|*.dat"; f1.Title = "DAT File Import";
                        break;
                }

                #endregion

                f1.ShowDialog();
                if (f1.FileName != "")
                {
                    filename = f1.FileName;
                    return true;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        #endregion

        #region Load Data

        #region Load TXT Data

        /// <summary>
        /// Import data from TXT file
        /// </summary>
        /// <param name="datafilename">File Name</param>
        private void _LoadTXTData(string datafilename)
        {
            try
            {
                string path = datafilename;
                string mystr = "";
                string[] alldata;
                FileStream fs = File.OpenRead(path);
                StreamReader sr = new StreamReader(fs, Encoding.ASCII);
                fs.Seek(0, SeekOrigin.Begin);

                sr.ReadLine(); //The first line of comments to skip

                #region The number of columns to read

                mystr = sr.ReadLine();
                alldata = mystr.Split(' ');
                Ncols = int.Parse(alldata[11]);

                #endregion

                #region  The number of rows read

                mystr = sr.ReadLine();
                alldata = mystr.Split(' ');
                Nrows = int.Parse(alldata[11]);

                #endregion

                _data = new double[Nrows, Ncols];

                #region The lower left corner abscissa read

                mystr = sr.ReadLine();
                alldata = mystr.Split(' ');
                Xllcorner = double.Parse(alldata[11]);

                #endregion

                #region The lower left corner ordinate read

                mystr = sr.ReadLine();
                alldata = mystr.Split(' ');
                Yllcorner = double.Parse(alldata[11]);

                #endregion

                #region Each grid on the number of meters reading

                mystr = sr.ReadLine();
                alldata = mystr.Split(' ');
                Cellsize = double.Parse(alldata[12]);

                #endregion

                #region NODATA_value Read

                mystr = sr.ReadLine();
                alldata = mystr.Split(' ');
                NODATA_Value = long.Parse(alldata[6]);

                #endregion

                #region Elevation data read

                int i = 0;
                while (sr.Peek() > -1)
                {
                    mystr = sr.ReadLine();
                    mystr = mystr.Replace("  ", " ");
                    alldata = mystr.Split(' ');

                    mystr = "";

                    for (int stri = 1; stri < alldata.Length; stri++)
                    {
                        Data[i, stri - 1] = double.Parse(alldata[stri]);
                    }
                    i++;
                }

                #endregion

                sr.Close();
                fs.Close();
            }
            catch (Exception ee)
            {
                cms.Visible = false;
                MessageBox.Show(ee.Message);
            }



        }



        #endregion

        #region Load XLS Data

        /// <summary>
        /// Import data from XLS file
        /// </summary>
        /// <param name="filename">File Name</param>
        private void _LoadXLSData(string datafilename)
        {
            try
            {
                FileStream fs = File.OpenRead(datafilename);
                StreamReader sr = new StreamReader(fs, Encoding.ASCII);

                fs.Seek(0, SeekOrigin.Begin);

                string mystr;
                sr.ReadLine();

                #region  Basic properties of reading Settings
                //Read and set Columns
                mystr = sr.ReadLine();
                {
                    string[] allstring = mystr.Split('\t');

                    Ncols = int.Parse(allstring[1]);
                }


                //Read and set Rows
                mystr = sr.ReadLine();
                {
                    string[] allstring = mystr.Split('\t');

                    Nrows = int.Parse(allstring[1]);
                }
                _data = new double[Nrows, Ncols];

                //read and set Xllconner
                mystr = sr.ReadLine();
                {
                    string[] allstring = mystr.Split('\t');

                    Xllcorner = double.Parse(allstring[1]);
                }

                //read and set Yllconner
                mystr = sr.ReadLine();
                {
                    string[] allstring = mystr.Split('\t');

                    Xllcorner = double.Parse(allstring[1]);
                }

                //read and set CellSize
                mystr = sr.ReadLine();
                {
                    string[] allstring = mystr.Split('\t');

                    Cellsize = double.Parse(allstring[1]);
                }

                //read and set NODATA_Vlaue
                mystr = sr.ReadLine();
                {
                    string[] allstring = mystr.Split('\t');

                    NODATA_Value = long.Parse(allstring[1]);
                }
                #endregion


                for (int i = 0; i < Nrows; i++)
                {
                    mystr = sr.ReadLine();
                    string[] allstring = mystr.Split('\t');

                    for (int j = 0; j < Ncols; j++)
                        Data[i, j] = double.Parse(allstring[j]);

                }

                sr.Close();
                fs.Close();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        #region Load Dat File

        /// <summary>
        /// Load Dat File Data
        /// </summary>
        /// <param name="datafilename">File Name</param>
        private void _LoadDatData(string datafilename)
        {

            try
            {

                #region The statistical number of Rows and Cols

                FileStream fs;
                StreamReader sr;
                string mystr = "";
                int i = 0, j = 0;
                int Lines = 0;

                #region  Cols

                fs = File.OpenRead(datafilename);
                sr = new StreamReader(fs, Encoding.ASCII);
                fs.Seek(0, SeekOrigin.Begin);
                mystr = sr.ReadLine();
                string[] alldata = mystr.Split(' ');
                if (alldata[alldata.Length - 1] == "")
                {
                    Ncols = alldata.Length - 1;
                }
                else
                {
                    Ncols = alldata.Length;
                }


                sr.Close();
                fs.Close();
                #endregion

                #region Rows

                fs = File.OpenRead(datafilename);
                sr = new StreamReader(fs, Encoding.ASCII);
                fs.Seek(0, SeekOrigin.Begin);

                while (sr.Peek() > -1)
                {
                    sr.ReadLine();
                    Lines++;
                }

                Nrows = Lines;


                sr.Close();
                fs.Close();

                #endregion



                #endregion

                #region Read Data


                _data = new double[Nrows, Ncols];
                fs = File.OpenRead(datafilename);
                sr = new StreamReader(fs, Encoding.ASCII);

                fs.Seek(0, SeekOrigin.Begin);

                while (sr.Peek() > -1)
                {
                    j = 0;
                    mystr = sr.ReadLine();
                    alldata = mystr.Split(' ');


                    foreach (string every in alldata)
                    {
                        if (every == "") continue;

                        Data[i, j++] = double.Parse(every);
                    }

                    i++;
                }



                sr.Close();
                fs.Close();
                #endregion

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion

        #region Display Data

        /// <summary>
        /// Display the data to the DataGridView
        /// </summary>
        private void _DisplayData()
        {
            try
            {
                for (int i = 0; i < Nrows; i++)
                    for (int j = 0; j < Ncols; j++)
                        this.Rows[i].Cells[j].Value = Data[i, j];
            }
            catch (Exception ee)
            {
                cms.Visible = false;
                MessageBox.Show(ee.Message);
            }


        }

        #endregion

        #region Copy,Cut,Paste Private Functions

        Point[] DataIndex;
        double[] DataTemp;
        long nums;

        #region Paste Private Functions


        #region Copyed Data Format


        private bool _ISPointBigger(Point p1, Point p2)
        {
            if ((p1.X > p2.X)) return true;
            else if (p1.X == p2.X)
            {
                if (p1.Y > p2.Y) return true;
            }

            return false;

        }

        /// <summary>
        /// Selected Cell Index Sort
        /// </summary>
        /// <param name="DataIndex">Data Index</param>
        /// <param name="DataTemp">Data Temp</param>
        private void _IndexSort(Point[] DataIndex, double[] DataTemp, long nums)
        {
            for (int i = 0; i < nums - 1; i++)
            {
                for (int j = 0; j < nums - 1 - i; j++)
                    if (_ISPointBigger(DataIndex[j], DataIndex[j + 1]))
                    {
                        Point temp;
                        temp = DataIndex[j];
                        DataIndex[j] = DataIndex[j + 1];
                        DataIndex[j + 1] = temp;

                        double temp2;
                        temp2 = DataTemp[j];
                        DataTemp[j] = DataTemp[j + 1];
                        DataTemp[j + 1] = temp2;
                    }
            }
        }

        #endregion


        /// <summary>
        /// Get the based Point of
        /// </summary>
        /// <returns>Based Point</returns>
        private Point _GetBasedPoint()
        {

            try
            {
                Point[] DataIndex1;
                double[] DataTemp1;
                long nums1;

                #region  Get Selected Data

                long selectedNums = 0;
                nums1 = 0;
                foreach (DataGridViewCell every in this.SelectedCells)
                {
                    selectedNums++;
                }

                DataIndex1 = new Point[selectedNums];
                DataTemp1 = new double[selectedNums];


                foreach (DataGridViewCell every in this.SelectedCells)
                {
                    DataIndex1[nums1] = new Point(every.RowIndex, every.ColumnIndex);

                }

                #endregion

                _IndexSort(DataIndex1, DataTemp1, nums1);

                return DataIndex1[0];
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return new Point(0, 0);

        }

        /// <summary>
        /// Get Points Away
        /// </summary>
        private void _GetPointsAway(Point[] away)
        {
            try
            {
                Point basedpoint = new Point(DataIndex[0].X, DataIndex[0].Y);

                for (int i = 0; i < nums; i++)
                {
                    away[i].X = DataIndex[i].X - basedpoint.X;
                    away[i].Y = DataIndex[i].Y - basedpoint.Y;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }




        #endregion

        #endregion


        #endregion


        #endregion

    }

    class BathymetryControl_MainPattern:Panel
    {
        #region Prpperties

        /// <summary>
        /// The width of panel
        /// </summary>
        private int _panelwidth;
        public int PanelWidth
        {
            get { return _panelwidth; }
            set 
            {
                _panelwidth = value;
                this.Width = value;
            }
        }

        /// <summary>
        /// The height of panel
        /// </summary>
        private int _panelheight;
        public int PanelHeight
        {
            get { return _panelheight; }
            set 
            { 
                _panelheight = value;
                this.Height = value;
            }
        }

        /// <summary>
        /// Data Sourse
        /// </summary>
        private DataGridViewControl _datasourse;
        public DataGridViewControl DataSourse
        {
            get { return _datasourse; }
            set { _datasourse = value; }
        }


        /// <summary>
        /// The max of all data
        /// </summary>
        private double _datamax;
        public double DataMax
        {
            get { return _datamax; }
            set { _datamax = value; }
        }

        /// <summary>
        /// The min of all data
        /// </summary>
        private double _datamin;
        public double DataMin
        {
            get { return _datamin; }
            set { _datamin = value; }
        }


        /// <summary>
        /// Elevation color
        /// </summary>
        private Color[] _elevationColor = new Color[16];
        public Color[] ElevationColor
        {
            get { return _elevationColor; }
            set { _elevationColor = value; }
        }
        

        /// <summary>
        /// Fresh data
        /// </summary>
        private Timer timer11;


        private Bitmap _pattern;

        #endregion

        #region Constructor

        public BathymetryControl_MainPattern(DataGridViewControl DataSourseControl,Control ParentControlled)
        {
            _Init(DataSourseControl, new Point(100, 150), ParentControlled, DockStyle.None);
        }

        public BathymetryControl_MainPattern(DataGridViewControl DataSourseControl, Point Location,Control ParentControlled,DockStyle Style)
        {
            _Init(DataSourseControl, Location,ParentControlled,Style);
        }

        private void _Init(DataGridViewControl DataSourseControl, Point Location, Control ParentControlled, DockStyle Style)
        {
            try
            {

            DataSourse = DataSourseControl;
            PanelWidth = DataSourse.Ncols ;
            PanelHeight = DataSourse.Nrows ;
            this.Location = Location;
            this.Parent = ParentControlled;
            this.Dock = Style;

            #region Format Elevation Color

            ElevationColor[0] = _ConvertToColor("#ff0000");
            ElevationColor[1] = _ConvertToColor("#ed5c09");
            ElevationColor[2] = _ConvertToColor("#ffa40f");
            ElevationColor[3] = _ConvertToColor("#f7ff0f");
            ElevationColor[4] = _ConvertToColor("#b1f902");
            ElevationColor[5] = _ConvertToColor("#59fc0d");
            ElevationColor[6] = _ConvertToColor("#1bef0f");
            ElevationColor[7] = _ConvertToColor("#07df3e");
            ElevationColor[8] = _ConvertToColor("#12ba7d");
            ElevationColor[9] = _ConvertToColor("#0aa4a2");
            ElevationColor[10] = _ConvertToColor("#0371bc");
            ElevationColor[11] = _ConvertToColor("#003cd8");
            ElevationColor[12] = _ConvertToColor("#0101f7");
            ElevationColor[13] = _ConvertToColor("#2d00d3");
            ElevationColor[14] = _ConvertToColor("#5800aa");
            ElevationColor[15] = _ConvertToColor("#85007f");

            #endregion


            this.MouseMove += new MouseEventHandler(_BathymetryControl_MainPattern_MouseMove);
            this.MouseDown += new MouseEventHandler(_BathymetryControl_MainPattern_MouseDown);
            this.MouseUp += new MouseEventHandler(_BathymetryControl_MainPattern_MouseUp);
 
           /* timer11 = new Timer();
            this.timer11.Tick += new EventHandler(timer11_Tick);
            this.timer11.Interval = 500;
            this.timer11.Start();*/

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }




 






        #endregion

        #region Public Function

        public void Draw()
        {
            try
            {
            _LoadingPattern();
            _GenerateCursorFile(this.PanelWidth / 15, this.PanelHeight / 7 * 2, "cursor.jpg");
           
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {

            try
            {
            //this.timer11.Stop();
            Graphics e = this.CreateGraphics();
            Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
            SolidBrush s=new SolidBrush(Color.White);

            e.FillRectangle(s, r);
            s.Dispose();
            e.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        #region Private or Protected Function



        #region  Events

 

        #region Press and release the mouse coordinates
        Point downlocation;
        Point uplocation;
        #endregion
 
        /// <summary>
        /// Bathymetry Control Main Pattern Mouse Down
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">MouseEventArgs</param>
        private void _BathymetryControl_MainPattern_MouseDown(object sender, MouseEventArgs e)
        {     
            this.MouseMove -= new MouseEventHandler(_BathymetryControl_MainPattern_MouseMove);
            this.Cursor = Cursors.Cross;

             
            if (e.Button == MouseButtons.Left)      
                downlocation = new Point(e.Y, e.X);
          
        }

        /// <summary>
        /// Bathymetry Control Main Pattern MouseUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _BathymetryControl_MainPattern_MouseUp(object sender, MouseEventArgs e)
        {
             if (e.Button == MouseButtons.Left)
            {
                uplocation = new Point(e.Y, e.X);
                _SelectAreaOfSelected(downlocation, uplocation);
            }
             _SetCursor(new Bitmap("cursor.jpg"), new Point(0, 0));
           this.MouseMove += new MouseEventHandler(_BathymetryControl_MainPattern_MouseMove);


        }


        /// <summary>
        /// Select area selected
        /// </summary>
        private void _SelectAreaOfSelected(Point start ,Point end)
        {
            int startX, endX;
            int startY, endY;

            #region Start and end position of the handle
            if (start.X < end.X)
            {
                startX = start.X;
                endX = end.X;
            }
            else
            {
                startX = end.X;
                endX = start.X;
            }

            if (start.Y < end.Y)
            {
                startY = start.Y;
                endY = end.Y;
            }
            else
            {
                startY = end.Y;
                endY = start.Y;
            }

            #endregion

            foreach (DataGridViewCell every in DataSourse.SelectedCells)
                every.Selected = false;

            for (int x = startX; x <= endX;x++ )
            {
                for (int y = startY; y <= endY; y++)
                    DataSourse.Rows[x].Cells[y].Selected = true;
                    
            }


        }

        /// <summary>
      /// Generate Cursor File
        /// </summary>
      /// <param name="cursorWidth">Cursor Width</param>
      /// <param name="cursorHeight">Cursor Height</param>
      /// <param name="cursorFileName">Cursor File Name</param>
      private void _GenerateCursorFile(int cursorWidth, int cursorHeight, string cursorFileName)
      {
          try
          {
 
          Bitmap cursor = new Bitmap(cursorWidth, cursorHeight);
          Graphics drawer = Graphics.FromImage(cursor);
          Pen boderpen = new Pen(Color.Black, 1);

          drawer.DrawRectangle(boderpen, 0, 0, cursorWidth - 1, cursorHeight - 1);

          cursor.Save(cursorFileName);
          boderpen.Dispose();
          cursor.Dispose();
          drawer.Dispose();

          }
          catch (System.Exception ex)
          {
              MessageBox.Show(ex.Message);
          }
      }

        /// <summary>
        /// Set Cursor
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="hotPoint"></param>
      private  void _SetCursor(Bitmap cursor, Point hotPoint)
      {
          try
          {

          int hotX = hotPoint.X;
          int hotY = hotPoint.Y;
          Bitmap myNewCursor = new Bitmap(cursor.Width * 2 - hotX, cursor.Height * 2 - hotY);
          Graphics g = Graphics.FromImage(myNewCursor);
          g.Clear(Color.FromArgb(0, 0, 0, 0));
          g.DrawImage(cursor, cursor.Width - hotX, cursor.Height - hotY, cursor.Width, cursor.Height);
          IntPtr iptr = myNewCursor.GetHicon();
          this.Cursor = new Cursor(iptr);
          g.Dispose();
          myNewCursor.Dispose();
          cursor.Dispose();
          }
          catch (System.Exception ex)
          {
              MessageBox.Show(ex.Message);
          }
      }


        /// <summary>
        /// Bathymetry Control  MainPattern MouseMove
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">MouseEventArgs</param>
        private void _BathymetryControl_MainPattern_MouseMove(object sender, MouseEventArgs e)
        {
            
            try
            {

                if ((e.X <= (this.PanelWidth - this.PanelWidth / 15)) && (e.Y <= (this.PanelHeight - this.PanelHeight / 7 * 2)))
                {
                    _SetCursor(new Bitmap("cursor.jpg"), new Point(0, 0));
                }
                else
                    this.Cursor = Cursors.Default;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

 
 
        /// <summary>
        /// The timer response operation function
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void timer11_Tick(object sender, EventArgs e)
        {
            try
            {

            PanelWidth = DataSourse.Ncols ;
            PanelHeight = DataSourse.Nrows ;
            _LoadingPattern();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Convert Hex Color to Class Color

        /// <summary>
        /// Convert Hex Color to Class Color
        /// </summary>
        /// <param name="HexColor">Hex Color</param>
        /// <returns></returns>
        private Color _ConvertToColor(string HexColor)
        {
            try
            {

            return System.Drawing.ColorTranslator.FromHtml(HexColor);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
                return Color.White;
             
        }

        #endregion

        /// <summary>
        /// Drawing grid lines
        /// </summary>
        private void _DrawGridLines()
        {
            try
            {

            Graphics gridlinesdraw = Graphics.FromImage(_pattern);
            Pen gridlinespen = new Pen(Color.CadetBlue, 1.8f);
            gridlinespen.DashStyle = DashStyle.Dash;


            #region The vertical grid map

            for (int i = 1; i <= 3; i++)
                gridlinesdraw.DrawLine(gridlinespen,
                    new Point(this.Width / 4 * i, 0),
                    new Point(this.Width / 4 * i, this.Height));

            #endregion

            #region The horizontal grid to draw

            for (int i = 1; i <= 6; i++)
                gridlinesdraw.DrawLine(gridlinespen,
                        new Point(0, this.PanelHeight / 7 * i),
                        new Point(this.PanelWidth, this.PanelHeight / 7 * i));


            #endregion


            gridlinesdraw.Dispose();
            gridlinespen.Dispose();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Display Parttern
        /// </summary>
        private void _DisplayPartten()
        {
            try
            {
                Graphics partterndrawer = this.CreateGraphics();
                partterndrawer.DrawImage(_pattern, new Point(0, 0));
                partterndrawer.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Loading Pattern
        /// </summary>
        private void _LoadingPattern()
        {
            try
            {

           PanelWidth = DataSourse.Ncols;
           PanelHeight = DataSourse.Nrows;
           _pattern = new Bitmap(PanelWidth, PanelHeight);
            _FindMaxMin();
            _SpiltElevationGroupData();
            _LoadingColor();
            _DrawGridLines();
            _DisplayPartten();
            
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #region Elevation color group data

        /// <summary>
        /// Elevation grouped data
        /// </summary>
        private long[,] gaocheng = new long[16, 2];
        public long[,] ElevationGroupedData
        {
            get { return gaocheng; }
            set { gaocheng = value; }
        }

        #endregion

        /// <summary>
        /// Spilt Elevation Group Data
        /// </summary>
        private void _SpiltElevationGroupData()
        {
            try
            {

            gaocheng[0, 0] = gaocheng[0, 1] = long.Parse(Math.Floor((decimal)DataMax).ToString());
            gaocheng[15, 0] = gaocheng[15, 1] = long.Parse(Math.Floor((decimal)DataMin).ToString());

            long spacing = (long)(gaocheng[0, 0] - gaocheng[15, 0]) / 14;

            for (int i = 1; i <= 14; i++)
            {
                gaocheng[i, 1] = gaocheng[i - 1, 0];
                gaocheng[i, 0] = gaocheng[i, 1] - spacing;
            }
            if (gaocheng[14, 0] != gaocheng[15, 0]) gaocheng[14, 0] = gaocheng[15, 0];

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int _GetColorIndex(double number)
        {
            try
            {


            if (number > gaocheng[0, 0]) return 0;
            else if (number <= gaocheng[15, 0]) return 15;
            else
            {
                for (int i = 1; i <= 14; i++)
                    if ((number > gaocheng[i, 0]) && (number <= gaocheng[i, 1])) return i;
            }

            /* 
            
             mystr += String.Format("Above  {0,5}\n", gaocheng[0, 0]);
             for (int i = 1; i <= 14; i++)
             {
                 mystr += String.Format("{0,5} - {1,5}\n", gaocheng[i, 0], gaocheng[i, 1]);
              }
             mystr += String.Format("Below   {0}\n", gaocheng[15, 0]);
             * 
            */

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return -1;
        }

        /// <summary>
        /// Loading Main Pattern Color
        /// </summary>
        private void _LoadingColor()
        {

            try
            {

            Color pointcolor;
           for(int i=0;i<PanelHeight ;i++)
               for (int j = 0; j < PanelWidth  ; j++)
               {
                   if (DataSourse.Rows[i].Cells[j].Value == null)
                   { _SetColor(new Point(i, j), Color.White); continue; }

                   double aa = double.Parse(DataSourse.Rows[i].Cells[j].Value.ToString());

                   pointcolor = ElevationColor[_GetColorIndex(aa)];

                   _SetColor(new Point (i,j),pointcolor);
               }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Find max min form all data
        /// </summary>
        private void _FindMaxMin()
        {

            try
            {

            DataMax = DataMin = DataSourse.Data[0, 0];

            for(int i=0;i<PanelHeight ;i++)
                for (int j = 0; j < PanelWidth ; j++)
                {
                    if (DataSourse.Rows[i].Cells[j].Value == null) continue;

                    if (double.Parse(DataSourse.Rows[i].Cells[j].Value.ToString()) > DataMax)
                    { 
                        DataMax = double.Parse(DataSourse.Rows[i].Cells[j].Value.ToString()); 
    
                    }
                    if (double.Parse(DataSourse.Rows[i].Cells[j].Value.ToString()) < DataMin) 
                    { 
                        DataMin = double.Parse(DataSourse.Rows[i].Cells[j].Value.ToString()); 
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MessageBox.Show(String.Format("Max: ({0},{1})->{2}   Min: ({3},{4})->{5}", p1.X + 1, p1.Y + 1, DataMax, p2.X + 1, p2.Y + 1, DataMin));
        }

        /// <summary>
        /// According to the value given to set the color
        /// </summary>
        /// <param name="point">Coordinates</param>
        private void _SetColor(Point point,Color PointColor)  
        {
            try
            {

            Graphics e = Graphics.FromImage(_pattern);
            SolidBrush brush = new SolidBrush(PointColor);
            Rectangle r = new Rectangle(point.Y ,point.X ,1,1);

            e.FillRectangle(brush, r);

         
            e.Dispose();
            brush.Dispose();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }




        #endregion

     
    }

    class BathymetryControl:Panel
    {
        #region Prpperties

        /// <summary>
        /// The main color display area
        /// </summary>
        private BathymetryControl_MainPattern _mainPattern;
        public  BathymetryControl_MainPattern MainPattern
        {
            get { return _mainPattern; }
            set { _mainPattern = value; }
        }

        /// <summary>
        /// Parent control
        /// </summary>
        private Control _parentcontroller;
        public Control ParentController
        {
            get { return _parentcontroller; }
            set { _parentcontroller = value; }
        }

        /// <summary>
        /// Each grid spacing
        /// </summary>
        private double _gridSpacing;
        public double GridSpacing
        {
            get { return _gridSpacing; }
            set { _gridSpacing = value; }
        }

        /// <summary>
        /// Time step
        /// </summary>
        private double _step;
        public double Step
        {
            get { return _step; }
            set { _step = value; }
        }

        /// <summary>
        /// Time Layer
        /// </summary>
        private double _layer;
        public double Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }

        #endregion

        #region Constructor

        public BathymetryControl(DataGridViewControl DataSourseControl, Control ParentControlled, DockStyle Style)
        {
            _Init(DataSourseControl, ParentControlled, Style);
        }

        private void _Init(DataGridViewControl DataSourseControl, Control ParentControlled, DockStyle Style)
        {
            try
            {
                _parentcontroller = ParentControlled;
                this.Parent = ParentControlled;
                this.Dock = Style;
                GridSpacing = 30;
                MainPattern = new BathymetryControl_MainPattern(DataSourseControl, this);
                MainPattern.DataSourse.Cms.ItemClicked += new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region  Public Function

        /// <summary>
        ///  Load Drawing Pattern
        /// </summary>
        public void LoadPattern()
        {

            try
            {

            Graphics cleanpanel = this.CreateGraphics();
            cleanpanel.Clear(Color.WhiteSmoke);
            cleanpanel.Dispose();

            MainPattern.Draw();
            _DrawTitle();
            _DrawUnderInfo();
            _DrawLeftInfo();
            _DrawRightInfo();
            //_DrawGridLines();
            _DrawCoordinate();
            _DrawingGridCoordinates();            

            #region Draw Main Pattern Border

            _DrawBorder(new Point(MainPattern.Location.X , MainPattern.Location.Y  ), MainPattern.Width, MainPattern.Height, 1);

            #endregion

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// ReDraw the MainPattern
        /// </summary>
        public void ReDraw()
        {
            try
            {
                MainPattern.Draw();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region MainPattern DataSourse Function Overide

        /// <summary>
        /// Cut Selected Data
        /// </summary>
        public void Cut()
        {
            MainPattern.DataSourse.Cut();
            ReDraw();
        }

        /// <summary>
        /// Copy Selected Data
        /// </summary>
        public void Copy()
        {
            MainPattern.DataSourse.Copy();
        }

        /// <summary>
        /// Paste Copyed Data
        /// </summary>
        public void Paste()
        {
            MainPattern.DataSourse.Paste();
            ReDraw();
        }

        /// <summary>
        ///Empty Selected Data
        /// </summary>
        public void Empty()
        {
            MainPattern.DataSourse.Empty();
            ReDraw();
        }

        /// <summary>
        /// Empty All Data
        /// </summary>
        public void EmptyAll()
        {
            MainPattern.DataSourse.EmptyAll();
            ReDraw();
        }


        /// <summary>
        ///Insert a row
        /// </summary>
        public void InsertRow()
        {
            MainPattern.DataSourse.InsertRow();
            LoadPattern();
        }

        /// <summary>
        /// Delete selected row
        /// </summary>
        public void DeleteRow()
        {
            MainPattern.DataSourse.DeleteRow();
            LoadPattern();
        }

        /// <summary>
        /// Insert a Column
        /// </summary>
        public void InsertColumn()
        {
            MainPattern.DataSourse.InsertColumn();
            LoadPattern();
        }

        /// <summary>
        /// Delete selected column
        /// </summary>
        public void DeleteColumn()
        {
            MainPattern.DataSourse.DeleteColumn();
            LoadPattern();
        }

        /// <summary>
        /// data import text
        /// </summary>
        public void DataImportText()
        {
            MainPattern.DataSourse.DataImportText();
            LoadPattern();
        }

        /// <summary>
        /// data import excel
        /// </summary>
        public void DataImportExcel()
        {
            MainPattern.DataSourse.DataImportExcel();
            LoadPattern();
        }

        /// <summary>
        /// data import dat file
        /// </summary>
        public void DataImportDatFile()
        {
             MainPattern.DataSourse.DataImportDatFile();
             LoadPattern();
        }

        /// <summary>
        /// export text format  
        /// </summary>
        public void ExportTextFormat()
        {
            MainPattern.DataSourse.ExportTextFormat();
        }

        /// <summary>
        /// export excel format
        /// </summary>
        public void ExportExcelFormat()
        {
            MainPattern.DataSourse.ExportExcelFormat();
        }

        /// <summary>
        /// export dat format
        /// </summary>
        public void ExportDatFormat()
        {
            MainPattern.DataSourse.ExportDatFormat();
        }

        #endregion

        #endregion

        #region Private or Protected Function


        /// <summary>
        /// Main Parttern Cms Click event Redraw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)  
        {
             
            try
            {

            ContextMenuStrip cms=MainPattern.DataSourse.Cms;
            if (e.ClickedItem == cms.Items[0])
            {
                MainPattern.Draw(); 
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[2])
            {
                MainPattern.Draw();
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[3])
            {
                MainPattern.Draw();
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[4])
            {
                MainPattern.Draw();
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[5])
            {
                LoadPattern();
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[6])
            {
                LoadPattern();
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[7])
            {
                LoadPattern();
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[8])
            {
                LoadPattern();
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[9])
            {
                LoadPattern();
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[10])
            {
                LoadPattern();
               // MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            else if (e.ClickedItem == cms.Items[11])
            {
                LoadPattern();
                //MainPattern.DataSourse.Cms.ItemClicked -= new ToolStripItemClickedEventHandler(_Cms_ItemClicked);
            }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

         }

        /// <summary>
        /// The title draw function
        /// </summary>
       private  void _DrawTitle()
        {
           try
           {

            Graphics titledrawer = this.CreateGraphics();
            Font titlefont = new Font("Arial", 15,FontStyle.Bold);
            SolidBrush fontbrush=new SolidBrush(Color.Black);
            titledrawer.DrawString("Bathymetry", titlefont, fontbrush,
               MainPattern.Location.X + MainPattern.PanelWidth /4, MainPattern.Location.Y-(35));     
            titledrawer.Dispose();
            fontbrush.Dispose();
            titlefont.Dispose();

           }
           catch (System.Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
        }

        /// <summary>
       /// Draw the main panel at the bottom of the information and fonts
       /// </summary>
       private void _DrawUnderInfo()
       {
           try
           {

          
           Graphics infodrawer = this.CreateGraphics();
           Font infofont = new Font("Arial", 10, FontStyle.Regular);
           SolidBrush fontbrush = new SolidBrush(Color.Black);

           infodrawer.DrawString(String.Format("(Grid spacing {0} meter)", GridSpacing), infofont, fontbrush, 
               MainPattern.Location.X +MainPattern.PanelWidth/4,MainPattern.Location.Y+MainPattern.PanelHeight+28);

           string nowtime=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
           infodrawer.DrawString(String.Format("{0} , Time step : {1} , Layer : {2}",nowtime,Step,Layer),infofont,fontbrush,
                MainPattern.Location.X + MainPattern.PanelWidth / 15, MainPattern.Location.Y + MainPattern.PanelHeight + 48);

           infodrawer.Dispose();
           fontbrush.Dispose();
           infofont.Dispose();

           }
           catch (System.Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

         /// <summary>
        /// Drawing on the left side of the font information
        /// </summary>
       private void _DrawLeftInfo()
       {
           try
           {

               Graphics ee = this.CreateGraphics();
               string text = String.Format("(Grid spacing {0} meter)", GridSpacing);
               PointF pointF = new PointF(MainPattern.Location.X - 55, MainPattern.Location.Y + MainPattern.PanelHeight / 5);
               //FontFamily fontFamily = new FontFamily("Arial");
               Font font = new Font("Arial", 10, FontStyle.Regular);
               StringFormat stringFormat = new StringFormat();
               stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;

               SolidBrush solidBrush = new SolidBrush(Color.Black);
               ee.DrawString(text, font, solidBrush, MainPattern.Location.X - 55, MainPattern.Location.Y + MainPattern.PanelHeight / 5, stringFormat);

               ee.Dispose();
               font.Dispose();
               stringFormat.Dispose();
               solidBrush.Dispose();
           }
           catch (System.Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

         /// <summary>
        /// Draw the right font information and illustrations
       /// </summary>
       private void _DrawRightInfo()
       {
           try
           {

           Graphics infodrawer = this.CreateGraphics();
           Font infofont = new Font("Arial", 10, FontStyle.Regular);
           SolidBrush fontbrush = new SolidBrush(Color.Black);

           infodrawer.DrawString(String.Format("Bathymetry [m]", GridSpacing), infofont, fontbrush,
               MainPattern.Location.X+MainPattern.Width +40 , MainPattern.Location.Y-10 );

           _DrawIllustrations();

           infodrawer.Dispose();
           fontbrush.Dispose();
           infofont.Dispose();

           }
           catch (System.Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

        /// <summary>
       /// Draw Illustrations
        /// </summary>
      private void _DrawIllustrations()
       {

          try
          {

           Graphics Illustrations = this.CreateGraphics();
           Font infofont = new Font("Arial", 8, FontStyle.Regular);
           SolidBrush fontbrush = new SolidBrush(Color.Black);

           #region Above Information Paint

           SolidBrush legendbrush = new SolidBrush(MainPattern.ElevationColor[0]);
           Rectangle r1 = new Rectangle(MainPattern.Location.X + MainPattern.Width + 40, MainPattern.Location.Y +11, 20, 10);
           Illustrations.FillRectangle(legendbrush, r1);
           string infostr=String.Format("Above {0}",MainPattern.ElevationGroupedData[0,0]);
           Illustrations.DrawString(infostr, infofont, fontbrush, MainPattern.Location.X + MainPattern.Width + 70, MainPattern.Location.Y + 9);

           _DrawBorder(new Point(MainPattern.Location.X + MainPattern.Width + 40, MainPattern.Location.Y + 11),20,10,1);
           #endregion

           #region The middle 14 column information mapping

           for (int i = 1; i <= 14; i++)
           {
               legendbrush = new SolidBrush(MainPattern.ElevationColor[i]);
               r1 = new Rectangle(MainPattern.Location.X + MainPattern.Width + 40, MainPattern.Location.Y + 11 + 11 * i , 20, 10);

               Illustrations.FillRectangle(legendbrush, r1);
               infostr = String.Format("{0}  -  {1}", MainPattern.ElevationGroupedData[i, 0], MainPattern.ElevationGroupedData[i, 1]);
               Illustrations.DrawString(infostr, infofont, fontbrush, 
                   MainPattern.Location.X + MainPattern.Width + 70, MainPattern.Location.Y + 10+11*i);

               _DrawBorder(new Point(MainPattern.Location.X + MainPattern.Width + 40, MainPattern.Location.Y + 11 + 11 * i),20,10,1);

           }

            #endregion

           #region Below Information Paint

               legendbrush = new SolidBrush(MainPattern.ElevationColor[15]);
               r1 = new Rectangle(MainPattern.Location.X + MainPattern.Width + 40, MainPattern.Location.Y + 11 + 11 *15, 20, 10);

               Illustrations.FillRectangle(legendbrush, r1);
               infostr = String.Format("Below {0}", MainPattern.ElevationGroupedData[15, 0]);
               Illustrations.DrawString(infostr, infofont, fontbrush,
                        MainPattern.Location.X + MainPattern.Width + 70, MainPattern.Location.Y + 10 + 11 * 15);

               _DrawBorder(new Point(MainPattern.Location.X + MainPattern.Width + 40, MainPattern.Location.Y + 11 + 11 * 15), 20, 10, 1);

 
            #endregion

           #region Undefined Value Paint

        legendbrush = new SolidBrush(Color.White);
        r1 = new Rectangle(MainPattern.Location.X + MainPattern.Width + 40, MainPattern.Location.Y + 11 + 11 * 16, 20, 10);

        Illustrations.FillRectangle(legendbrush, r1);
        infostr = "Undefined Value";
        Illustrations.DrawString(infostr, infofont, fontbrush,
                MainPattern.Location.X + MainPattern.Width + 70, MainPattern.Location.Y + 10 + 11 * 16);

        _DrawBorder(new Point(MainPattern.Location.X + MainPattern.Width + 40, MainPattern.Location.Y + 11 + 11 * 16), 20, 10, 1);

           #endregion

           Illustrations.Dispose();
           infofont.Dispose();
           fontbrush.Dispose();
           legendbrush.Dispose();

          }
          catch (System.Exception ex)
          {
              MessageBox.Show(ex.Message);
          }
           
       }


        /// <summary>
        /// Draw the border
        /// </summary>
        /// <param name="location">The upper left onrner coordinates</param>
        /// <param name="width">Border Width</param>
         /// <param name="height">Border Height</param>
         /// <param name="size">Border Size</param>
        private void _DrawBorder(Point location,float width,float height,int size)
         {
            try
            {

             Graphics borderdrawer = this.CreateGraphics();
             Pen boderpen = new Pen(Color.Black, size);

             borderdrawer.DrawRectangle(boderpen, location.X - size, location.Y - size, width + size, height + size);

             boderpen.Dispose();
             borderdrawer.Dispose();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         }


        /// <summary>
        /// Map coordinate system
        /// </summary>
        private void _DrawCoordinate()
        {
            try
            {

            Graphics coordinatedrawer = this.CreateGraphics();
            Pen divisionpen = new Pen(Color.Black, 1.8f);
            Pen coordinatepen = new Pen(Color.Black,1);

            #region Draw the horizontal  coordinates


            for (int i = 0; i < 4; i++)
            {
                coordinatedrawer.DrawLine(divisionpen,
                    new Point(MainPattern.Location.X + MainPattern.PanelWidth / 4 * i, MainPattern.Location.Y + MainPattern.PanelHeight),
                    new Point(MainPattern.Location.X + MainPattern.PanelWidth / 4 * i, MainPattern.Location.Y + MainPattern.PanelHeight + 8));

                for (int j = 1; j <= 9;j++ )
                {
                    coordinatedrawer.DrawLine(coordinatepen,
                        new Point(MainPattern.Location.X + MainPattern.PanelWidth / 4 * i + (MainPattern.PanelWidth / 4)/10*j, MainPattern.Location.Y + MainPattern.PanelHeight),
                        new Point(MainPattern.Location.X + MainPattern.PanelWidth / 4 * i + (MainPattern.PanelWidth / 4) /10 * j, MainPattern.Location.Y + MainPattern.PanelHeight +4));

                }

            }

            #endregion

            #region Draw the longitudinal coordinates

            for (int i = 1; i <7;i++ )
                {
                    coordinatedrawer.DrawLine(divisionpen,
                        new Point(MainPattern.Location.X-8,MainPattern.Location.Y+MainPattern.PanelHeight/7*i),
                        new Point(MainPattern.Location.X, MainPattern.Location.Y + MainPattern.PanelHeight / 7 * i));


                    for (int j = 1; j <= 4;j++ )
                    {
                        coordinatedrawer.DrawLine(coordinatepen,
                            new Point(MainPattern.Location.X - 5, MainPattern.Location.Y + MainPattern.PanelHeight / 7 * i - (MainPattern.PanelHeight / 7)/5*j),
                            new Point(MainPattern.Location.X, MainPattern.Location.Y + MainPattern.PanelHeight / 7 * i - (MainPattern.PanelHeight / 7) / 5 * j));
                    }
                }

                    coordinatedrawer.DrawLine(divisionpen,
                        new Point(MainPattern.Location.X - 8, MainPattern.Location.Y + MainPattern.PanelHeight-1),
                        new Point(MainPattern.Location.X, MainPattern.Location.Y + MainPattern.PanelHeight-1));

                    for (int j = 1; j <= 4; j++)
                    {
                        coordinatedrawer.DrawLine(coordinatepen,
                            new Point(MainPattern.Location.X - 5, MainPattern.Location.Y + MainPattern.PanelHeight - 1 - (MainPattern.PanelHeight - 1 - (MainPattern.PanelHeight / 7 * 6)) / 5 * j),
                            new Point(MainPattern.Location.X, MainPattern.Location.Y + MainPattern.PanelHeight - 1 - (MainPattern.PanelHeight - 1 - (MainPattern.PanelHeight / 7 * 6)) / 5 * j));

                    }

            #endregion

            coordinatedrawer.Dispose();
            coordinatepen.Dispose();
            divisionpen.Dispose();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Drawing grid coordinates
        /// </summary>
        private void _DrawingGridCoordinates()
        {
            try
            {

            Graphics valuedrawer = this.CreateGraphics();
            SolidBrush fontbrush=new SolidBrush(Color.Black);
            Font valuefont=new Font("Arial",10,FontStyle.Regular);

            #region Draw the longitudinal Value

            for (int i = 0; i < 4; i++)
            {
 
                string valuestr = String.Format("{0}", MainPattern.DataSourse.Ncols/4*i);

                valuedrawer.DrawString(valuestr, valuefont, fontbrush, 
                    MainPattern.Location.X + MainPattern.PanelWidth / 4 * i-8, MainPattern.Location.Y + MainPattern.PanelHeight+10);
            }

            #endregion

            #region  Draw the horizontal Value

            #region To calculate the maximum number of bits

            int maxbit=0;
            long temp = (long)MainPattern.DataMax;

            while (temp>0)
            {
                temp /= 10;
                maxbit++;
            }

            #endregion

            for (int i = 0; i < 7; i++)
            {
                string strformat=String.Format("{0}0,{1}{2}",'{',maxbit,'}');
                string valuestr = String.Format(strformat, MainPattern.DataSourse.Nrows / 7 * i);

                valuedrawer.DrawString(valuestr,valuefont,fontbrush,
                    MainPattern.Location.X - 35, MainPattern.Location.Y + MainPattern.PanelHeight - (MainPattern.PanelHeight/7*i)-12);
            }

            #endregion

            valuefont.Dispose();
            fontbrush.Dispose();
            valuedrawer.Dispose();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        #endregion

    }

}
