using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;

namespace _200421248A1
{
    public partial class ChangeCalculator : Form
    {
        public ChangeCalculator()
        {
            InitializeComponent();
        }

        private void ChangeCalculator_Load(object sender, EventArgs e)
        {
            // Making fileds unavailible for user to change
            txtChange.Enabled = false;
            txtToonies.Enabled = false;
            txtLoonies.Enabled = false;
            txtQuaters.Enabled = false;
            txtDimes.Enabled = false;
            txtNickels.Enabled = false;


        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            float paid;
            float total;
            float change;
            //Reading input and saving it as a string
            String inputPaid = txtPaid.Text;
            String inputTotal = txtTotal.Text;

            /// <summary>
            ///  Validate input by using simple "if" and "esle if" statments to make sure the format of the input is right, 
            ///  if validation had passed go to "else" where calculations are 
            /// </summary>

            /// Making sure the input type in sumeric
            if (!Single.TryParse(inputPaid, out paid) | !Single.TryParse(inputTotal, out total))
            {
                // If not, show an error massage in message box
                MessageBox.Show("Invalid Paid or/and Total value, try again!", "Error");
                // Clean calculations in a case if form already been used before and there something left
                DeleteCalculations();
            }

            /// Validate if the total amount is a positive integer
            else if (total < 0)
            {
                // If not, show an error massage in message box
                MessageBox.Show("Total amount, must be positive integer. Try again!", "Total amount Error");
                // Clean calculations in a case if form already been used before and there something left
                DeleteCalculations();
            }

            /// Validate if the paid amount is a positive integer
            else if (paid <= 0)
            {
                // If not, show an error massage in message box
                MessageBox.Show("Paid amount, must be positive integer. Try again!", "Paid amount Error");
                // Clean calculations in a case if form already been used before and there something left
                DeleteCalculations();
            }

            ///Validating if the total amount less then paid.
            ///If user had a paid amount error before, so paid variable has not been set, what is by default 0, total>0 will be True always
            else if (total > paid && paid != 0 && total != 0)
            {
                // If not, show an error massage in message box
                MessageBox.Show("Your payment is insufficient to cover the total, you still need to pay: " + (total - paid) + " dollars", "Error");
                // Clean calculations in a case if form already been used before and there something left
                DeleteCalculations();
            }

            /// All validations had passed
            else
            {
                /// <summary>
                /// Logic of the calculations:
                /// To find out the number of the particular coins in every case: Take the floor from change (or remainder
                /// from the previous calculation) devided by the coefficient by this particullar coin.
                /// For example, if for a tonie the coefficient will be 2
                /// </summary>

                // Change textBox
                change = paid - total;
                //formate change value as currency and dispay
                txtChange.Text = change.ToString("C", CultureInfo.CurrentCulture);

                // Calculating number of tonies and dispaying it in textBox
                decimal tonies = (decimal)Math.Floor(change / 2);
                // saving rest in a remainder var
                float remainder = change - ((float)tonies * 2);
                //dispaying to the textbox
                txtToonies.Text = tonies.ToString();

                // Calculating number of loonies and dispaying it in textBox
                decimal loonies = (decimal)Math.Floor(remainder / 1);
                // saving rest in a remainder var
                remainder = remainder - ((float)loonies * 1);
                //dispaying to the textbox
                txtLoonies.Text = loonies.ToString();

                //Calculating number of quaters and dispaying it in textBox
                int quaters = (int)(remainder / 0.25);
                // saving rest in a remain var
                double remain = remainder - ((float)quaters * 0.25);
                //dispaying to the textbox
                txtQuaters.Text = quaters.ToString();

                //Calculating number of dimes and dispaying it in textBox
                int dimes = (int)(remain / 0.10);
                // saving rest in a remain var
                remain = remain - ((float)dimes * 0.10);
                //dispaying to the textbox
                txtDimes.Text = dimes.ToString();

                //Calculating number of dimes and dispaying it in textBox
                int nickels = (int)(remain / 0.05);
                // saving rest in a remain var
                remain = remain - ((float)nickels * 0.05);
                //where pennies get rounded down if there are 2c or less, and up to a nickel if 3 or more cents remain.
                if (remain >= 0.03) { nickels = nickels + 1; }
                //dispaying to the textbox
                txtNickels.Text = nickels.ToString();
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            DeleteAll();
        }

        /// <summary>
        ///  DeleteAll function cleaning all textboxes
        /// </summary>
        public void DeleteAll()
        {
            txtTotal.Clear();
            txtPaid.Clear();
            txtChange.Clear();
            txtLoonies.Clear();
            txtToonies.Clear();
            txtQuaters.Clear();
            txtDimes.Clear();
            txtNickels.Clear();
        }

        /// <summary>
        /// DeleteCalculations function cleaning calculation(unenabled) textboxes
        /// </summary>
        public void DeleteCalculations()
        {
            txtChange.Clear();
            txtLoonies.Clear();
            txtToonies.Clear();
            txtQuaters.Clear();
            txtDimes.Clear();
            txtNickels.Clear();
        }

    }
}
