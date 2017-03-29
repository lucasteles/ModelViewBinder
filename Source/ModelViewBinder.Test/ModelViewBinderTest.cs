using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ModelViewBinder.Test
{
    public class ModelViewBinderTest
    {

        public ModelViewBinderTest()
        {

        }

        private BasicSource CreateSource() => new BasicSource { A = "value", B = 7, C = true };

        [Fact(DisplayName = "Should copy source to target")]
        public void Should_copy_source_to_target()
        {
            var target = new BasicTarget();
            var source = CreateSource();
            var binder = new ModelViewBinder<BasicSource>(source);

            binder.Bind(e => e.A, target, e => e.X);
            binder.Bind(e => e.B, target, e => e.Y);
            binder.Bind(e => e.C, target, e => e.Z);

            binder.FillTargets();
            var result = source.A == target.X && source.B == target.Y && source.C == target.Z;

            Assert.True(result);

        }

        [Fact(DisplayName = "Should copy and convert source to target")]
        public void Should_copy_and_convert_source_to_target()
        {
            var target = new BasicTarget();
            var source = CreateSource();
            var binder = new ModelViewBinder<BasicSource>(source);

            binder.Bind(e => e.B, target, e => e.X, Convert.ToString);
            binder.FillTargets();

            Assert.True(source.B.ToString() == target.X);
        }


        [Fact(DisplayName = "Should copy target to source ")]
        public void Should_copy_target_to_source()
        {
            var target = new BasicTarget { X = "1", Y = 1, Z = true } ;
            var source = CreateSource();
            var binder = new ModelViewBinder<BasicSource>(source);

            binder.Bind(e => e.A, target, e => e.X)
                  .Bind(e => e.B, target, e => e.Y)
                  .Bind(e => e.C, target, e => e.Z);

            binder.FillSource();
            var result = source.A == target.X && source.B == target.Y && source.C == target.Z;

            Assert.True(result);

        }

        [Fact(DisplayName = "Should copy and convert target to source ")]
        public void Should_copy_and_convert_target_to_source()
        {
            var target = new BasicTarget { X = "9", Y = 1, Z = true };
            var source = CreateSource();
            var binder = new ModelViewBinder<BasicSource>(source);

            binder.Bind(e => e.B, target, e => e.X, Convert.ToString, int.Parse);
            binder.FillSource();

            Assert.True(source.B.ToString() == target.X);
        }

        [Fact(DisplayName = "Should trigger change event on target and fill source")]
        public void Should_trigger_change_event_on_target_and_fill_source()
        {
            var target = new ComplexTarget();
            var source = CreateSource();
            var binder = new ModelViewBinder<BasicSource>(source);

            binder.Bind(e => e.B, target);

            target.Value = 100;
            
            Assert.True(source.B == 100);

        }


        [Fact(DisplayName = "Should disable target")]
        public void Should_disable_target()
        {
            var target = new ComplexTarget { Enabled = true };
            var source = CreateSource();
            var binder = new ModelViewBinder<BasicSource>(source);
            binder.Bind(e => e.B, target);

            binder.DisableAll();

            Assert.True(!target.Enabled);

        }

        [Fact(DisplayName = "Should enable target")]
        public void Should_enable_target()
        {
            var target = new ComplexTarget { Enabled = false };
            var source = CreateSource();
            var binder = new ModelViewBinder<BasicSource>(source);
            binder.Bind(e => e.B, target);

            binder.EnableAll();

            Assert.True(target.Enabled);

        }


        [Fact(DisplayName = "Should copy source to target and run a callback")]
        public void Should_copy_source_to_target_and_run_a_callback()
        {
            var target = new BasicTarget();
            var source = CreateSource();
            var binder = new ModelViewBinder<BasicSource>(source);
            var runned = false;

            binder.Bind(e => e.A, target, e => e.X).Then(() => runned = true);
       

            binder.FillTargets();
            var result = source.A == target.X && runned;

            Assert.True(result);

        }
    }
}
