using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace ModelViewBinder
{

    public interface IModelViewBinder : IDisposable
    {

        void FillTargets();
        void FillSource();

        void DisableAll();
        void EnableAll();



        TTarget GetTargetFromProperty<TTarget>(string propertyName) where TTarget : class;

    }

    public interface IModelViewBinder<TSource> : IModelViewBinder
    {
        IList<IRegisterItem<TSource>> Targets { get; set; }
        IModelViewBinderWithCallback<TSource> Bind<TTarget, TValue>(Expression<Func<TSource, TValue>> expression, TTarget control) where TTarget : class, ITargetWithValue<TValue>;
        IModelViewBinderWithCallback<TSource> Bind<TPropertie, TTarget>(Expression<Func<TSource, TPropertie>> expression, TTarget control, Expression<Func<TTarget, TPropertie>> controlExpression) where TTarget : class;
        IModelViewBinderWithCallback<TSource> Bind<TPropertie, TTarget, TConvertFromModel>(Expression<Func<TSource, TConvertFromModel>> expression, TTarget control, Expression<Func<TTarget, TPropertie>> controlExpression, Func<TConvertFromModel, TPropertie> convertFunction) where TTarget : class;
        IModelViewBinderWithCallback<TSource> Bind<TPropertie, TTarget, TConvertFromModel>(Expression<Func<TSource, TConvertFromModel>> expression, TTarget control, Expression<Func<TTarget, TPropertie>> controlExpression, Func<TConvertFromModel, TPropertie> convertFromModelFunction, Func<TPropertie, TConvertFromModel> convertToModelFunction) where TTarget : class;
        IModelViewBinder<TSource> SetSource(TSource model);
        TTarget GetTargetFromProperty<TTarget, TProperty>(Expression<Func<TSource, TProperty>> property) where TTarget : class;
    }

    public interface IModelViewBinderWithCallback<TSource> : IModelViewBinder<TSource>
    {
        IModelViewBinder<TSource> Then(Action action);
    }
}