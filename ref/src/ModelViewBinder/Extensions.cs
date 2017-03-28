using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Extensions
{
    internal static class Extensions
    {

        public static string MemberName<T, V>(this Expression<Func<T, V>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.Name;
        }

        public static PropertyInfo GetPropertyInfo<T, V>(this Expression<Func<T, V>> expression)
        {
            PropertyInfo property;

            var exp = expression.Body as MemberExpression;
            if (exp == null)
                exp = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            property = (PropertyInfo)exp.Member;

            return property;
        }

      

      

    }

}