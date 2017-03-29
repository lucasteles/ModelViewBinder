using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViewBinder.Test
{
    public class ComplexTarget : ITargetWithValue<int>, ITargetWithEnabled
    {
        int _value;
        public int Value {
            get => _value;
            set { _value = value; ValueChanged?.Invoke(this, EventArgs.Empty); }

        }
        public bool Enabled { get; set; }

        public event EventHandler ValueChanged;
    }
}
