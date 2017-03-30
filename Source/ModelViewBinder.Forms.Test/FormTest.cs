using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelViewBinder.Forms.Test
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();

            var values = new Dictionary<int, string>
            {
                [0] = "value 0",
                [1] = "value 1",
                [2] = "value 2",
                [3] = "value 3"
            };

            comboBox1.DataSource = new BindingSource(values, null);
            comboBox1.ValueMember = "Key";
            comboBox1.DisplayMember = "Value";
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
        
        }
    }
}
