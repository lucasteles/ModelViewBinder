using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewBinder.Forms.Test
{
    public class SourceObject
    {
        public string ValueForTextbox { get; set; }
        public string ValueForRichText { get; set; }
        public int ValueForComboBox { get; set; }
        public decimal ValueForUpDown { get; set; }
        public DateTime ValueForDatePicker { get; set; }
        public bool ValueForCheckBox { get; set; }

    }
}
