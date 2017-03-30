
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace ModelViewBinder
{
    public class ModelViewBinder<TSource> : IModelViewBinder<TSource>, IModelViewBinderWithCallback<TSource>, IDisposeContainer
    {
        protected TSource _Model;
        protected IList<IDisposable> BindersToDispose = new List<IDisposable>();
        public IList<IRegisterItem<TSource>> Targets { get; set; } = new List<IRegisterItem<TSource>>();
        protected IRegisterItem<TSource> LastRegister { get; set; }

        public ModelViewBinder()
        {

        }

        public ModelViewBinder(TSource source)
        {
            SetSource(source);
        }

        public IModelViewBinder<TSource> SetSource(TSource model)
        {
            _Model = model;

            return this;
        }


        public IModelViewBinderWithCallback<TSource> Bind(Expression<Func<TSource, object>> expression, ITargetWithValue target)
        {
            var register = new RegisterItem<TSource, object, ITargetWithValue>(expression, target, e => e.Value);

            Add(register);

            return this;
        }

        public IModelViewBinderWithCallback<TSource> Bind<TValue>(Expression<Func<TSource, TValue>> expression, ITargetWithValue<TValue> target) 
        {
            var register = new RegisterItem<TSource, TValue, ITargetWithValue<TValue>>(expression, target, e => e.Value);

            Add(register);

            return this;
        }

        public IModelViewBinderWithCallback<TSource> Bind<TPropertie, TTarget>(Expression<Func<TSource, TPropertie>> expression, TTarget target, Expression<Func<TTarget, TPropertie>> targetExpression)
             where TTarget : class
        {
            var register = new RegisterItem<TSource, TPropertie, TTarget>(expression, target, targetExpression, null);
            Add(register);

            return this;
        }
        public IModelViewBinderWithCallback<TSource> Bind<TPropertie, TTarget, TConvertFromModel>(Expression<Func<TSource, TConvertFromModel>> expression, TTarget target, Expression<Func<TTarget, TPropertie>> targetExpression, Func<TConvertFromModel, TPropertie> convertFunction)
             where TTarget : class
        {
            var register = new RegisterItem<TSource, TPropertie, TTarget, TConvertFromModel>(expression, target, targetExpression, convertFunction, null);
            Add(register);

            return this;
        }
        public IModelViewBinderWithCallback<TSource> Bind<TPropertie, TTarget, TConvertFromModel>(Expression<Func<TSource, TConvertFromModel>> expression, TTarget target, Expression<Func<TTarget, TPropertie>> targetExpression, Func<TConvertFromModel, TPropertie> convertFromModelFunction, Func<TPropertie, TConvertFromModel> convertToModelFunction)
             where TTarget : class
        {
            var register = new RegisterItem<TSource, TPropertie, TTarget, TConvertFromModel>(expression, target, targetExpression, convertFromModelFunction, convertToModelFunction, null);
            Add(register);

            return this;
        }
        private void Add(IRegisterItem<TSource> item)
        {
            TryApplyChangeEvent(item);
            Targets.Add(item);
            LastRegister = item;
        }
        public void FillTargets()
        {
            foreach (var item in Targets)
            {
                item.FillTarget(_Model);
            }
        }
        public void FillSource()
        {
            foreach (var item in Targets)
            {
                item.FillSource(_Model);
            }
        }
        public IModelViewBinder<TSource> Then(Action action)
        {
            LastRegister.SetCallback(action);
            return this;
        }
      
        public TTarget GetTargetFromProperty<TTarget>(string propertyName) where TTarget : class
        {
            var result = default(TTarget);

            var targetRegister = Targets.ToList().Where(e => e.GetPropertyInfo().Name == propertyName)?.FirstOrDefault();

            result = (TTarget)targetRegister?.GeTTarget();

            return result;
        }
        public TTarget GetTargetFromProperty<TTarget, TProperty>(Expression<Func<TSource, TProperty>> property) where TTarget : class
        {
            return GetTargetFromProperty<TTarget>(property.GetPropertyInfo().Name);
        }
   
        public void Dispose()
        {
            foreach (var item in BindersToDispose)
            {
                item.Dispose();
            }
        }

        protected virtual void TryApplyChangeEvent(IRegisterItem<TSource> item)
        { 

                if (item.GeTTarget() is ITargetWithChangeEvent)
                {
                    var target = item.GeTTarget() as ITargetWithChangeEvent;
                    target.ValueChanged += (object sender, EventArgs e) => { item.FillSource(_Model);  };
                 }               

        }

        public virtual void DisableAll()
        {
            foreach (var item in Targets)
            {
                if (item.GeTTarget() is ITargetWithEnabled)
                    ((ITargetWithEnabled)item.GeTTarget()).Enabled = false;
            }
        }

        public virtual void EnableAll()
        {
            foreach (var item in Targets)
            {
                if (item.GeTTarget() is ITargetWithEnabled)
                    ((ITargetWithEnabled)item.GeTTarget()).Enabled = true;

               
            }
        }


        public void RegisterDispose(IDisposable item)
        {
            if (item != null)
                BindersToDispose.Add(item);
        }
    }

}
