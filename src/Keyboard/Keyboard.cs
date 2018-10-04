using System;
using RawInput_dll;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MessagingToolkit.Barcode;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Media.Imaging;

namespace Keyboard
{
    /// <summary>
    /// Main form of the application
    /// </summary>
    public partial class Keyboard : Form
    {
        // time frame to warm if there is no input
        private int second = 30;

        // raw input 
        private readonly RawInput _rawinput;

        // only accept input when the form has focus
        private const bool CaptureOnlyInForeground = true;

        // contain all the linked scanner source (Keyboard_01) to device ID
        private Dictionary<string, string> scanner2device = new Dictionary<string, string>();

        // contain a scanner and the barcodes it scanned
        private Dictionary<string, List<string>> scanner2barcode = new Dictionary<string, List<string>>();

        // contain all the capillaries that have been scanned
        private List<string> capillary = new List<string>();

        // barcode that is currently scanned
        private string barcodeNumber = string.Empty;

        private DataTable data = new DataTable();

        private double cellValue;

        private SetupDevices setupDevices;

        // represents a cell's cordinate
        private struct Cell
        {
            public int row;
            public int column;

            public Cell(int row, int column)
            {
                this.row = row;
                this.column = column;
            }
        }

        // cell to edit
        private Cell cellToEdit;

        private System.Timers.Timer timer;

        SaveFileDialog saveDialog;

        /// <summary>
        /// constructor
        /// </summary>
        public Keyboard()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            _rawinput = new RawInput(Handle, CaptureOnlyInForeground);
            _rawinput.AddMessageFilter();   // Adding a message filter will cause keypresses to be handled
            Win32.DeviceAudit();            // Writes a file DeviceAudit.txt to the current directory
            _rawinput.KeyPressed += OnKeyPressed;
        }


        /// <summary>
        /// form load event handler. Initialize everything.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void Keyboard_Load(object sender, EventArgs e)
        {
            // column to indicate if the row is finished
            data.Columns.Add("Completed", typeof(string));
            gridView.DataSource = data;

            // data grid view style setting
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            gridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            gridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            gridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            gridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            gridView.EnableHeadersVisualStyles = false;

            SourceLB.Visible = false;
        }

        /// <summary>
        /// connect a device to a scanner
        /// </summary>
        /// <param name="scanner"> scanner to connect </param>
        /// <param name="device"> device to connect </param>
        public void SetScanner2Device(string scanner, string device)
        {
            if (scanner2device.Values.ToArray().Contains(device))
            {
                throw new Exception();
            }

            scanner2device.Add(scanner, device);
        }

        /// <summary>
        /// set all data for a reference device
        /// </summary>
        /// <param name="OD">diameter</param>
        /// <param name="average">average</param>
        /// <param name="averageWidth">average with</param>
        /// <param name="count">number count</param>
        public void SetCellDate(double value)
        {
            cellValue = value;
        }

        /// <summary>
        /// update UI after set device
        /// </summary>
        /// <param name="scanner">scanner name</param>
        /// <param name="device">device ID</param>
        /// <param name="type">device type, either reference or normal</param>
        public void SetDeviceUpdateUI(string scanner, string device, string type)
        {
            gridView.Columns.Add(device, gridView.ColumnCount + Environment.NewLine + type + Environment.NewLine + "Input: " + scanner + Environment.NewLine + "Device ID: " + device);

            if (type == "Reference Device")
            {
                gridView.Columns[device].DisplayIndex = 1;
                gridView.Columns[device].HeaderCell.Style.BackColor = Color.Green;
            }

            // clear the checkmarks
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                gridView.Rows[i].Cells[0].Value = String.Empty;
            }
        }

        /// <summary>
        /// update UI after edit data in a cell. The value will be determined low, mid, or high depending on a given range.
        /// </summary>
        public void EditDataUpdateUI()
        {
            string sign = " ";
            string low = char.ConvertFromUtf32(8595);
            string high = char.ConvertFromUtf32(8593);
            string med = "-";
            bool outOfRange = false;

            if (cellToEdit.row % 3 == 1)    // Average
            {
                if (cellValue > 2 && cellValue < 7)
                {
                    sign = low;
                }
                else if (cellValue > 15 && cellValue < 25)
                {
                    sign = med;
                }
                else if (cellValue > 30)
                {
                    sign = high;
                }
                else
                {
                    outOfRange = true;
                }
            }
            else if (cellToEdit.row % 3 == 0)   // OD
            {
                if (cellValue > .35 && cellValue < .55)
                {
                    sign = low;
                }
                else if (cellValue > .8 && cellValue < 1.2)
                {
                    sign = med;
                }
                else if (cellValue > 1.3)
                {
                    sign = high;
                }
                else
                {
                    outOfRange = true;
                }
            }
            else if (cellToEdit.row % 3 == 2)   // Count
            {
                if (cellValue > 50 && cellValue < 199)
                {
                    sign = low;
                }
                else if (cellValue > 200 && cellValue < 380)
                {
                    sign = med;
                }
                else if (cellValue > 400)
                {
                    sign = high;
                }
                else
                {
                    outOfRange = true;
                }
            }

            gridView.Rows[cellToEdit.row].Cells[cellToEdit.column].Value = cellValue;
            string header = gridView.Rows[cellToEdit.row].HeaderCell.Value.ToString();
            header = header.Remove(header.Length - 1, 1) + sign;
            gridView.Rows[cellToEdit.row].HeaderCell.Value = header;

            if (outOfRange)
            {
                gridView.Rows[cellToEdit.row].Cells[cellToEdit.column].Style.BackColor = Color.Red;
            }
            else
            {
                gridView.Rows[cellToEdit.row].Cells[cellToEdit.column].Style.BackColor = Color.White;
            }

            // mark the row as completed 
            if (RowFinished(cellToEdit.row))
            {
                gridView.Rows[cellToEdit.row].Cells[0].Value = "\u221A";
            }
        }

        /// <summary>
        /// handle key press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            // input source number by format Keyboar_Number
            string inputSource = e.KeyPressEvent.Source;

            Form fc = Application.OpenForms["SetupDevices"];
            if (fc != null)
            {
                setupDevices.setScannerLB(inputSource);
            }

            // ASCII value of the pressed key
            int value = Int16.Parse(e.KeyPressEvent.VKey.ToString(CultureInfo.InvariantCulture));

            // state of the press, either PRESS of BREAK
            string state = e.KeyPressEvent.KeyPressState;

            /*
                The following is all data you can get from a keypress:
            lbHandle.Text = e.KeyPressEvent.DeviceHandle.ToString();
            lbType.Text = e.KeyPressEvent.DeviceType;
            lbName.Text = e.KeyPressEvent.DeviceName;
            lbDescription.Text = e.KeyPressEvent.Name;
            lbKey.Text = e.KeyPressEvent.VKey.ToString(CultureInfo.InvariantCulture);
            lbNumKeyboards.Text = _rawinput.NumberOfKeyboards.ToString(CultureInfo.InvariantCulture);
            lbVKey.Text = e.KeyPressEvent.VKeyName;
            lbSource.Text = inputSource;
            lbKeyPressState.Text = state;
            lbMessage.Text = string.Format("0x{0:X4} ({0})", e.KeyPressEvent.Message);
            */

            SourceLB.Text = "Source: " + inputSource;

            bool registered = scanner2device.Keys.ToArray().Contains(inputSource);

            // 13 is ENTER
            if (value == 13 && state == "BREAK" && registered)
            {
                if (barcodeNumber == String.Empty)
                {
                    return;
                }

                // comlumn index of the cell just added
                int columnIndex = 0;
                // row index of the cell just added
                int rowIndex;

                // find the column of the cell
                for (int i = 1; i < scanner2device.Count + 1; i++)
                {
                    string header = gridView.Columns[i].HeaderText;
                    if (header.Contains(inputSource))
                    {
                        columnIndex = i;
                        break;
                    }
                }

                /*
                 * if the capillary list does not contain the barcode, add a new row. 
                 * the barcode must start with MESCAP.
                 * new cell will be marked as yellow
                */
                if (!capillary.Contains(barcodeNumber))
                {
                    if (barcodeNumber.StartsWith("MESCAP"))
                    {
                        capillary.Add(barcodeNumber);
                        
                        // first time the scanner scans a barcode
                        if (!scanner2barcode.Keys.ToArray().Contains(inputSource))
                        {
                            scanner2barcode.Add(inputSource, new List<string>());
                        }

                        scanner2barcode[inputSource].Add(barcodeNumber);

                        DataRow rowOD = data.NewRow();
                        data.Rows.Add(rowOD);
                        DataRow rowAverage = data.NewRow();
                        data.Rows.Add(rowAverage);
                        DataRow rowCount = data.NewRow();
                        data.Rows.Add(rowCount);

                        gridView.Rows[gridView.RowCount - 4].HeaderCell.Value = capillary.Count + " - ID: " + barcodeNumber + Environment.NewLine + "OD:";
                        gridView.Rows[gridView.RowCount - 3].HeaderCell.Value = "Average:";
                        gridView.Rows[gridView.RowCount - 2].HeaderCell.Value = "Count:";

                        gridView.Rows[gridView.RowCount - 4].Cells[columnIndex].Style.BackColor = Color.Yellow;
                        gridView.Rows[gridView.RowCount - 3].Cells[columnIndex].Style.BackColor = Color.Yellow;
                        gridView.Rows[gridView.RowCount - 2].Cells[columnIndex].Style.BackColor = Color.Yellow;

                        // set timer
                        setTimer(new Cell(gridView.RowCount - 4, columnIndex));
                        setTimer(new Cell(gridView.RowCount - 3, columnIndex));
                        setTimer(new Cell(gridView.RowCount - 2, columnIndex));
                    }
                    else
                    {
                        MessageBox.Show("Label must start with MESCAP!");
                    }
                }
                else
                {
                    rowIndex = capillary.IndexOf(barcodeNumber);
                    gridView.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Yellow;
                    gridView.Rows[rowIndex + 1].Cells[columnIndex].Style.BackColor = Color.Yellow;
                    gridView.Rows[rowIndex + 2].Cells[columnIndex].Style.BackColor = Color.Yellow;
                }

                barcodeNumber = String.Empty;
                return;
            }

            if (registered)
            {
                // there are two states - press(MAKE) and release(BREAK) 
                bool validInput = (value > 47 && value < 58) || (value > 64 && value < 91);
                if (state == "MAKE" && validInput)
                {
                    barcodeNumber += ((char)value).ToString();
                }
            }
        }

        /// <summary>
        /// set a timer for the cell.
        /// </summary>
        /// <param name="cell"> the cell to set timer</param>
        private void setTimer(Cell cell)
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += (sender, e) => TimeTick(sender, e, cell);
            timer.Interval = 1000 * second;
            timer.Enabled = true;
            timer.AutoReset = false;
            timer.Start();
        }

        /// <summary>
        /// event hanlder for the timer
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        /// <param name="cell">cell for the timer </param>
        private void TimeTick(object sender, EventArgs e, Cell cell)
        {
            timer.Stop();
            timer.Dispose();

            if (gridView.Rows[cell.row].Cells[cell.column].Style.BackColor == Color.Yellow)
            {
                string str = string.Format("({0}, {1}) is scanned but has not getten any input!", cell.row + 1, cell.column);
                MessageBox.Show(str);
                cellValue = GetRandomDouble(0, 500);
                cellToEdit = new Cell(cell.row, cell.column);
                EditDataUpdateUI();
            }
        }

        /// <summary>
        /// click event handler for add device button
        /// </summary>
        /// <param name="sender"> sender</param>
        /// <param name="e">event args</param>
        private void AddDeviceButton_Click(object sender, EventArgs e)
        {
            setupDevices = new SetupDevices(this);
            _rawinput.RemoveMessageFilter();
            setupDevices.ShowDialog();
        }

        /// <summary>
        /// double click event handler for a cell in the grid view
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void gridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditValue editValue = new EditValue(this);
            _rawinput.RemoveMessageFilter();
            cellToEdit.row = e.RowIndex;
            cellToEdit.column = e.ColumnIndex;
            editValue.ShowDialog();
        }

        /// <summary>
        /// selection change event hanlder for grid table. It does not allow user to select a cell
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void gridTable_SelectionChanged(object sender, EventArgs e)
        {
            gridView.ClearSelection();
        }

        /// <summary>
        /// click handler for the export button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void exportBT_Click(object sender, EventArgs e)
        {
            // create Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // create new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add();
            // create new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet1 = null;

            // create a new sheet
            worksheet1 = workbook.Sheets["Sheet1"];

            // change the name of the sheet
            worksheet1.Name = "Output";

            // store column header part in Excel  
            for (int i = 1; i < gridView.Columns.Count + 1; i++)
            {
                worksheet1.Cells[1, i + 1] = gridView.Columns[i - 1].HeaderText;
            }

            // store row header part in Excel
            for (int i = 1; i < gridView.RowCount; i++)
            {
                worksheet1.Cells[i + 1, 1] = gridView.Rows[i - 1].HeaderCell.Value.ToString();
            }

            // store each row and column value to excel sheet  
            for (int i = 0; i < gridView.Rows.Count - 1; i++)
            {
                for (int j = 0; j < gridView.Columns.Count; j++)
                {
                    if (gridView.Rows[i].Cells[j].Value == null)
                    {
                        continue;
                    }

                    string str = gridView.Rows[i].Cells[j].Value.ToString();
                    worksheet1.Cells[i + 2, j + 2] = str;
                }
            }

            saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveDialog.FilterIndex = 2;
            saveDialog.FileName = "output";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workbook.SaveAs(saveDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error");
                }
            }

            app.Quit();
        }

        /// <summary>
        /// click event handler for check button. Check if all cells are filled
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void checkBT_Click(object sender, EventArgs e)
        {
            bool empty = false;
            string s = "cell" + Environment.NewLine;

            // find all cells that are empty and get the device id for that cell
            for (int i = 0; i < gridView.RowCount - 1; i++)
            {
                for (int j = 1; j < gridView.ColumnCount; j++)
                {
                    if (gridView.Rows[i].Cells[j].Value == null || gridView.Rows[i].Cells[j].Value.ToString() == string.Empty)
                    {
                        string header = gridView.Columns[j].HeaderText;
                        // split with new line
                        string splitNewline = header.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Last();
                        // split with "ID:"
                        string deviceId = splitNewline.Split(new[] { "ID:" }, StringSplitOptions.None).Last();

                        string param;

                        if (i % 3 == 0)
                        {
                            param = "OD";
                        }
                        else if (i % 3 == 1)
                        {
                            param = "Average";
                        }
                        else
                        {
                            param = "Count";
                        }

                        s += string.Format("({0}, {1})'s {2} with Device ID: {3}", (i / 3 + 1).ToString(), j.ToString(), param, deviceId);
                        s += Environment.NewLine;
                        empty = true;
                    }
                }
            }

            if (empty)
            {
                s += "is empty!";
                MessageBox.Show(s);
            }
            else
            {
                MessageBox.Show("Everything is filled up!");
            }

        }

        /// <summary>
        /// generate a barcode starts with MESCAP then followed with a random 8-digit number, then save it
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void barcodeBT_Click(object sender, EventArgs e)
        {
            BarcodeEncoder generator = new BarcodeEncoder();
            string ramdomNum = new Random().Next(0, 99999999).ToString();
            string code = "MESCAP" + ramdomNum;
            saveDialog = new SaveFileDialog();

            generator.IncludeLabel = true;
            generator.CustomLabel = "MESCAP" + ramdomNum;
            WriteableBitmap image = generator.Encode(BarcodeFormat.Code39, code);

            saveDialog.Filter = "PNG File|*png";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream stream = new FileStream(saveDialog.FileName, FileMode.Create);
                PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
                bitmapEncoder.Frames.Add(BitmapFrame.Create(image));
                bitmapEncoder.Save(stream);
                MessageBox.Show("Successfully saved!");
            }
        }

        /// <summary>
        /// import an excel sheet to the grid view
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void importBt_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //open file format define Excel Files(.xls)|*.xls| Excel Files(.xlsx)|*.xlsx| 
            openFileDialog.Filter = "XML Files (*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb) |*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb";
            openFileDialog.FilterIndex = 3;

            openFileDialog.Multiselect = false;
            // define the name of openfileDialog
            openFileDialog.Title = "Open Text File-R13";
            //define the initial directory
            openFileDialog.InitialDirectory = @"Desktop";

            // execute when file open
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string pathName = openFileDialog.FileName;
                string sheetName = "Output";

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    pathName +
                    ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand oconn = new OleDbCommand("Select * From [" + sheetName + "$]", con);
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                data = new DataTable();
                sda.Fill(data);
                gridView.Columns.Clear();
                gridView.DataSource = data;

                for (int i = 0; i < gridView.RowCount; i++)
                {
                    gridView.Rows[i].HeaderCell.Value = gridView.Rows[i].Cells[0].Value;
                }
                gridView.Columns.RemoveAt(0);

                // clear the capillary list and device2scanner map
                capillary.Clear();
                scanner2device.Clear();
                
                for (int i = 1; i < gridView.ColumnCount; i++)
                {
                    // somehow newline in the column header is converted to __
                    string header = gridView.Columns[i].HeaderText;

                    // change it back
                    gridView.Columns[i].HeaderText = header.Replace("__", Environment.NewLine);

                    // bind device to scanner
                    string scanner = header.Split(new[] { "Input: " }, StringSplitOptions.None).Last();
                    scanner = scanner.Split(new[] { "__" }, StringSplitOptions.None).First();
                    string device = header.Split(new[] { "ID: " }, StringSplitOptions.None).Last();

                    SetScanner2Device(scanner, device);
                }

                // add barcode to capillary list
                for (int i = 0; i < gridView.RowCount - 1; i++)
                {
                    if (i % 3 == 0)
                    {
                        string header = gridView.Rows[i].HeaderCell.Value.ToString();
                        string barcode = header.Split(new[] { "MESCAP" }, StringSplitOptions.None).Last();
                        barcode = barcode.Split(new[] { Environment.NewLine }, StringSplitOptions.None).First();

                        capillary.Add(barcode);
                    }
                }
            }
        }

        /// <summary>
        /// return whether a row is completed
        /// </summary>
        /// <param name="rowIndex"> row number </param>
        /// <returns> true if the row is completed, otherwise false </returns>
        private bool RowFinished(int rowIndex)
        {
            bool finished = true;

            for (int i = 1; i < gridView.Columns.Count; i++)
            {
                if (gridView.Rows[rowIndex].Cells[i].Value == null || gridView.Rows[rowIndex].Cells[i].Value.ToString() == string.Empty)
                {
                    finished = false;
                    break;
                }
            }

            return finished;
        }

        /// <summary>
        /// handle the row header paainting. Make the row header without a seperator
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void gridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == -1 && e.RowIndex >= 0)
            {
                if (e.RowIndex % 3 == 0)
                {
                    e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                    e.AdvancedBorderStyle.Top = ((DataGridView)sender).AdvancedCellBorderStyle.Top;
                }
                else if (e.RowIndex % 3 == 1)
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                    e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                }
                else if (e.RowIndex % 3 == 2)
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                    e.AdvancedBorderStyle.Bottom = ((DataGridView)sender).AdvancedCellBorderStyle.Bottom;
                }
            }
        }

        /// <summary>
        /// use for getting data from the device and fill the data into the grid table
        /// </summary>
        /// <param name="deviceID">device ID where the data is from</param>
        /// <param name="OD">OD param from the device test</param>
        /// <param name="average">average param from the device test</param>
        /// <param name="count">count param from the device test</param>
        private void GetDataFromDevice(string deviceID, double OD, double average, int count)
        {
            OD = GetRandomDouble(0, 2);
            average = GetRandomDouble(0, 50);
            count = (int)GetRandomDouble(0, 500);

            // find the scanner binded with the device
            string scanner = scanner2device.FirstOrDefault(x => x.Value == deviceID).Key;

            // get the barcode first being scanned
            string firstBarcode = scanner2barcode[scanner].First();
            scanner2barcode[scanner].Remove(firstBarcode);

            // find column index
            int column = 0;
            for (int i = 1; i < gridView.ColumnCount; i++)
            {
                if (gridView.Columns[i].HeaderText.Contains(deviceID))
                {
                    column = i;
                    break;
                }
            }

            if (column == 0)
            {
                MessageBox.Show("device id not registered!");
                return;
            }

            // find row index
            for (int i = 0; i < capillary.Count; i++) 
            {
                if (gridView.Rows[i].HeaderCell.Value.ToString().Contains(firstBarcode))
                {
                    gridView.Rows[i].Cells[column].Value = OD;
                    gridView.Rows[i + 1].Cells[column].Value = average;
                    gridView.Rows[i + 2].Cells[column].Value = count;

                    gridView.Rows[i].Cells[column].Style.BackColor = Color.White;
                    gridView.Rows[i + 1].Cells[column].Style.BackColor = Color.White;
                    gridView.Rows[i + 2].Cells[column].Style.BackColor = Color.White; 
                }
            }
        }

        /// <summary>
        /// generate a random double in given range
        /// </summary>
        /// <param name="min">min of the range</param>
        /// <param name="max">max of the range</param>
        /// <returns></returns>
        private double GetRandomDouble(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }




        /// <summary>
        /// form close event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void Keyboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            _rawinput.KeyPressed -= OnKeyPressed;
        }

        /// <summary>
        /// handle exception from current domain
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private static void CurrentDomain_UnhandledException(Object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

            if (null == ex) return;

            // Log this error. Logging the exception doesn't correct the problem but at least now
            // you may have more insight as to why the exception is being thrown.
            Debug.WriteLine("Unhandled Exception: " + ex.Message);
            Debug.WriteLine("Unhandled Exception: " + ex);
            MessageBox.Show(ex.Message);
        }

        /// <summary>
        /// handle keypresses when go back to main form
        /// </summary>
        public void AddMessageFilter()
        {
            _rawinput.AddMessageFilter();
        }
    }
}
