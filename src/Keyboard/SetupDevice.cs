using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keyboard
{
    /// <summary>
    /// set device form
    /// </summary>
    public partial class SetupDevices : Form
    {
        Keyboard keyboard;

        // list of devices
        List<string> dataSource = new List<string>();

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="keyboard">the keyboard form to callback</param>
        public SetupDevices(Keyboard keyboard)
        {
            InitializeComponent();
            this.keyboard = keyboard;
        }

        /// <summary>
        /// click event handler for add device button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void AddDeviceButton_Click(object sender, EventArgs e)
        {
            string device = DeviceIdText.Text;
            string type = typeCB.Text;
            string scannerID = scannerLB.Text.Split(new[] { "ID: " }, StringSplitOptions.None).Last();
            if (device == string.Empty || type == string.Empty || scannerLB.Text.Length < 13)
            {
                MessageBox.Show("Cant be empty");
                return;
            }

            try
            {
                keyboard.SetScanner2Device(scannerID, device);
            }
            catch (Exception)
            {
                MessageBox.Show("Device or scanner have already been registered");
                return;
            }

            deviceGridView.Rows.Add(new[] { device, type, scannerID });

            keyboard.SetDeviceUpdateUI(scannerID, device, type);
        }

        /// <summary>
        /// form close event handler for this form. Start handling keypress.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void SetDevices_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyboard.AddMessageFilter();
        }

        /// <summary>
        /// Start button click event handler. Close the form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void startBT_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// form load event hanlder. Initialize the grid view
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void SetDevices_Load(object sender, EventArgs e)
        {
            deviceGridView.RowHeadersVisible = false;

            deviceGridView.Columns.Add("DeviceID", "Device ID");
            deviceGridView.Columns.Add("DeviceType", "Device Type");
            deviceGridView.Columns.Add("ScannerID", "Scanner ID");

            deviceGridView.Columns[0].Width = 150;
            deviceGridView.Columns[1].Width = 150;
            deviceGridView.Columns[2].Width = 150;
        }

        /// <summary>
        /// setter for the scanner label
        /// </summary>
        /// <param name="str">string to set</param>
        public void setScannerLB(string str)
        {
            scannerLB.Text = "Scanner ID: " + str;
        }
    }
}
