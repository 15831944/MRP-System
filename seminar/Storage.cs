using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace seminar
{
    public partial class Storage : Form
    {
        public Storage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Receipts.num = int.Parse(textBox1.Text);
            }
            catch
            {
                Receipts.num = 0;
            }

            this.Close();
        }
    }
}
