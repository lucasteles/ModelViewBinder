using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelViewBinder.Forms
{
   public class FormModelViewBinder<TSource> : ModelViewBinder<TSource>
    {
        public bool AutoFillSourceWhenTargetChanges { get; set; } = true;

        public FormModelViewBinder()
        {

        }

        public FormModelViewBinder(TSource source) : base(source)
        {
  
        }

        public override void EnableAll()
        {
            base.EnableAll();
            foreach (var item in Targets)
            {
                if (item.GeTTarget() is Control)
                    ((Control)item.GeTTarget()).Enabled = false;
            }

        }


        public override void DisableAll()
        {
            base.DisableAll();
            foreach (var item in Targets)
            {
                if (item.GeTTarget() is Control)
                    ((Control)item.GeTTarget()).Enabled = true;
            }
        }

        protected override void TryApplyChangeEvent(IRegisterItem<TSource> item)
        {
            IDisposable observable = null;
            var target = item.GeTTarget();
             Action<object, EventArgs> eventBody = (object sender, EventArgs e) => {
                 if (AutoFillSourceWhenTargetChanges) item.FillSource(_Model);
             };

            if (target is ITargetWithChangeEvent)
            {
                var t = item.GeTTarget() as ITargetWithChangeEvent;
                t.ValueChanged += new EventHandler(eventBody);
            }
            else if (target is Control)
            {
               if (target is TextBoxBase)
                {
                    var textbox = target as TextBoxBase;
                    textbox.TextChanged += new EventHandler(eventBody);
                }
                else if (target is ComboBox)
                {
                    var list = target as ComboBox;
                    list.SelectedIndexChanged += new EventHandler(eventBody);
                }
                else if (target is ListControl)
                {
                    var list = target as ListControl;
                    list.TextChanged += new EventHandler(eventBody);
                }
                else if (target is CheckBox)
                {
                    var list = target as CheckBox;
                    list.CheckedChanged += new EventHandler(eventBody);
                }
                else if (target is NumericUpDown)
                {
                    var list = target as NumericUpDown;
                    list.ValueChanged += new EventHandler(eventBody);
                }
                else if (target is DateTimePicker)
                {
                    var list = target as DateTimePicker;
                    list.ValueChanged+= new EventHandler(eventBody);
                }


            }


            RegisterDispose(observable);
        }

     

    }
}
