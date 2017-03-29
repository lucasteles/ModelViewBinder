using System;
using System.Windows.Forms;
using ModelViewBinder;


namespace ModelViewBinder
{
   public static class ModelViewBinderExtensions
    {
        /*
        public static void SetForm<TSource>(this IModelViewBinder<TSource> me, Form form)
        {
            
            me.FillTargets();
            foreach (var item in me.Controls)
            {
                IDisposable observable = null;

                if (item.GeTTarget() is IControlWithChangeEvent)
                {
                    var control = item.GeTTarget() as IControlWithChangeEvent;

                    observable = Observable.FromEventPattern<EventHandler, EventArgs>(
                       handler => handler.Invoke,
                       h => control.ValueChanged += h,
                       h => control.ValueChanged -= h)
                       .ObserveOn(form)
                       .Subscribe(e => item.FillSource(_Model));
                }

                if (observable != null && me is IDisposeContainer)
                        (me as IDisposeContainer).RegisterDispose(observable);
                  
            }
        }
       
        
        public static void DisableAll<TSource>(this IModelViewBinder<TSource> me)
        {
            foreach (var item in me.Targets)
            {
                if (item is ITargetWithEnabled)
                    ((ITargetWithEnabled)item.GeTTarget()).Enabled = false;

                if (item is Control)
                    ((Control)item.GeTTarget()).Enabled = false;
            }
        }

        public static void EnableAll<TSource>(this IModelViewBinder<TSource> me)
        {
            foreach (var item in me.Targets)
            {
                if (item is ITargetWithEnabled)
                    ((ITargetWithEnabled)item.GeTTarget()).Enabled = true;

                if (item is Control)
                    ((Control)item.GeTTarget()).Enabled = true;
            }
        }
        
         */
    }
}
