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
    /// Edit value form
    /// </summary>
    public partial class EditValue : Form
    {
        private Keyboard keyboard;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="keyboard">the keyboard form to callback</param>
        public EditValue(Keyboard keyboard)
        {
            InitializeComponent();
            this.keyboard = keyboard;
        }

        /// <summary>
        /// click event handler for confirm button. Save all the values.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ConfirmBT_Click(object sender, EventArgs e)
        {
            try
            {
                double value = Convert.ToDouble(ODText.Text);
                keyboard.SetCellDate(value);
                keyboard.EditDataUpdateUI();
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("must be a number");
            }
        }

        /// <summary>
        /// form close event handler for this form. Start handling keypress.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void EditValue_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyboard.AddMessageFilter();
        }
    }
}
