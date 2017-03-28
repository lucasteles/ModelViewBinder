
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;


namespace ModelViewBinder
{
    public class ModelViewBinder<TSource> : IModelViewBinder<TSource>, IModelViewBinderWithCallback<TSource>
    {
        private TSource _Model;
        private readonly IList<IDisposable> BindersToDispose = new List<IDisposable>();
        public IList<IRegisterItem<TSource>> Controls { get; set; } = new List<IRegisterItem<TSource>>();
        protected IRegisterItem<TSource> LastRegister { get; set; }
        public ModelViewBinder()
        {
        }
        public IModelViewBinder<TSource> SetSource(TSource model)
        {
            _Model = model;

            return this;
        }

        public IModelViewBinderWithCallback<TSource> Bind<TTarget>(Expression<Func<TSource, object>> expression, TTarget control) where TTarget : class, IControlWithValue
        {
            var register = new RegisterItem<TSource, object, TTarget>(expression, control, e => e.Value);

            Add(register);

            return this;
        }

        public IModelViewBinderWithCallback<TSource> Register<TPropertie, TTarget>(Expression<Func<TSource, TPropertie>> expression, TTarget control, Expression<Func<TTarget, TPropertie>> controlExpression)
             where TTarget : class
        {
            var register = new RegisterItem<TSource, TPropertie, TTarget>(expression, control, controlExpression, null);
            Add(register);

            return this;
        }
        public IModelViewBinderWithCallback<TSource> Register<TPropertie, TTarget, TConvertFromModel>(Expression<Func<TSource, TConvertFromModel>> expression, TTarget control, Expression<Func<TTarget, TPropertie>> controlExpression, Func<TConvertFromModel, TPropertie> convertFunction)
             where TTarget : class
        {
            var register = new RegisterItem<TSource, TPropertie, TTarget, TConvertFromModel>(expression, control, controlExpression, convertFunction, null);
            Add(register);

            return this;
        }
        public IModelViewBinderWithCallback<TSource> Register<TPropertie, TTarget, TConvertFromModel>(Expression<Func<TSource, TConvertFromModel>> expression, TTarget control, Expression<Func<TTarget, TPropertie>> controlExpression, Func<TConvertFromModel, TPropertie> convertFromModelFunction, Func<TPropertie, TConvertFromModel> convertToModelFunction)
             where TTarget : class
        {
            var register = new RegisterItem<TSource, TPropertie, TTarget, TConvertFromModel>(expression, control, controlExpression, convertFromModelFunction, convertToModelFunction, null);
            Controls.Add(register);

            return this;
        }
        private void Add(IRegisterItem<TSource> item)
        {
            Controls.Add(item);
            LastRegister = item;
        }
        public void FillTargets()
        {
            foreach (var item in Controls)
            {
                item.FillTarget(_Model);
            }
        }
        public void FillSource()
        {
            foreach (var item in Controls)
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

            var controlRegister = Controls.ToList().Where(e => e.GetPropertyInfo().Name == propertyName)?.FirstOrDefault();

            result = (TTarget)controlRegister?.GeTTarget();

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
    }

}
