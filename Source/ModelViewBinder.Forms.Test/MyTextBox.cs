using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelViewBinder.Forms
{
    public partial class MyTextBox : TextBox, ITargetWithValue
    {
        public MyTextBox()
        {
            InitializeComponent();
            if (!DesignMode)
            TextChanged += MyTextBox_TextChanged;
        }

        private void MyTextBox_TextChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(sender,e);
        }

        public event EventHandler ValueChanged;
        public object Value { get => Text; set => Text = value.ToString(); }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
