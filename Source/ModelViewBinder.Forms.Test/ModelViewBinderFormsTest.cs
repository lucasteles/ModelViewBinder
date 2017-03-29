using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ModelViewBinder.Forms;

namespace ModelViewBinder.Forms.Test
{
   public class ModelViewBinderFormsTest
    {
        public ModelViewBinderFormsTest()
        {

        }

        SourceObject CreateSource()
            => new SourceObject { ValueForTextbox = "textbox value", ValueForRichText = "rich value", ValueForComboBox = 2, ValueForCheckBox = true, ValueForUpDown = 10M, ValueForDatePicker = new DateTime(2000, 1, 1) };


        FormTest GetBindedForm(IModelViewBinder<SourceObject> binder)
        {
            var form = new FormTest();

            binder
                .Bind(e => e.ValueForTextbox, form.textBox1, e => e.Text)
                .Bind(e => e.ValueForRichText, form.richTextBox1, e => e.Text)
                .Bind(e => e.ValueForComboBox, form.comboBox1, e => e.SelectedValue)
                .Bind(e => e.ValueForUpDown, form.numericUpDown1, e => e.Value)
                .Bind(e => e.ValueForDatePicker, form.dateTimePicker1, e => e.Value)
                .Bind(e => e.ValueForCheckBox, form.checkBox1, e => e.Checked)
            ;


            binder.FillTargets();
            return form;
        }

        [Fact(DisplayName = "Should fill form controls")]
        public void Should_fill_form_controls()
        {
            var source = CreateSource();
            var binder = new FormModelViewBinder<SourceObject>(source);
            var form = GetBindedForm(binder); ;

            var result =
                source.ValueForTextbox == form.textBox1.Text &&
                source.ValueForRichText == form.richTextBox1.Text &&
                source.ValueForComboBox == (int)form.comboBox1.SelectedValue &&
                source.ValueForUpDown == form.numericUpDown1.Value &&
                source.ValueForDatePicker == form.dateTimePicker1.Value &&
                source.ValueForCheckBox == form.checkBox1.Checked;

            Assert.True(result);

        }


        [Fact(DisplayName = "Should change source when change target textbox")]
        public void Should_change_source_when_change_target_textbox()
        {
            var source = CreateSource();
            var binder = new FormModelViewBinder<SourceObject>(source);
            var form = GetBindedForm(binder); ;

            form.textBox1.Text = "other value";

            Assert.True(form.textBox1.Text == source.ValueForTextbox);

        }


        [Fact(DisplayName = "Should change source when change target richtext")]
        public void Should_change_source_when_change_target_richtext()
        {
            var source = CreateSource();
            var binder = new FormModelViewBinder<SourceObject>(source);
            var form = GetBindedForm(binder); ;

            form.Load += (object sender, EventArgs e) => {
                form.richTextBox1.Text = "other value";
                Assert.True(form.richTextBox1.Text == source.ValueForRichText);
            };
            
        }


        [Fact(DisplayName = "Should change source when change target combo")]
        public void Should_change_source_when_change_target_combo()
        {
            var source = CreateSource();
            var binder = new FormModelViewBinder<SourceObject>(source);
            var form = GetBindedForm(binder); ;

            form.comboBox1.SelectedValue = 1;

            Assert.True((int)form.comboBox1.SelectedValue == source.ValueForComboBox);

        }



        [Fact(DisplayName = "Should change source when change target numeric")]
        public void Should_change_source_when_change_target_numeric()
        {
            var source = CreateSource();
            var binder = new FormModelViewBinder<SourceObject>(source);
            var form = GetBindedForm(binder); ;

            form.numericUpDown1.Value = 99;

            Assert.True(form.numericUpDown1.Value == source.ValueForUpDown);

        }

        [Fact(DisplayName = "Should change source when change target datepicker")]
        public void Should_change_source_when_change_target_datepicker()
        {
            var source = CreateSource();
            var binder = new FormModelViewBinder<SourceObject>(source);
            var form = GetBindedForm(binder); ;

            form.dateTimePicker1.Value = DateTime.Now;

            Assert.True(form.dateTimePicker1.Value == source.ValueForDatePicker);

        }

        [Fact(DisplayName = "Should change source when change target checkbox")]
        public void Should_change_source_when_change_target_checkbox()
        {
            var source = CreateSource();
            var binder = new FormModelViewBinder<SourceObject>(source);
            var form = GetBindedForm(binder); ;

            form.checkBox1.Checked = !form.checkBox1.Checked;

            Assert.True(form.checkBox1.Checked == source.ValueForCheckBox);

        }


    }
}
