using System;
using System.Windows.Forms;
using ModelViewBinder;


namespace ModelViewBinder.Forms
{
   public static class ModelViewBinderExtensions
    {
        
        public static void SetForm<TSource>(this IModelViewBinder<TSource> me, Form form)
        {
            
            FillTargets();
            foreach (var item in Controls)
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

                if (observable != null)
                    BindersToDispose.Add(observable);
            }
        }

        /*
        public void DisableAll()
        {
            foreach (var item in Controls)
            {
                if (item is IControlWithEnabled)
                    ((IControlWithEnabled)item.GeTTarget()).Enabled = false;

                if (item is Control)
                    ((Control)item.GeTTarget()).Enabled = false;
            }
        }

        public void EnableAll()
        {
            foreach (var item in Controls)
            {
                if (item is IControlWithEnabled)
                    ((IControlWithEnabled)item.GeTTarget()).Enabled = true;

                if (item is Control)
                    ((Control)item.GeTTarget()).Enabled = true;
            }
        }
        */
        
    }
}
