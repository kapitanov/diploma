using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace AISTek.DataFlow.Shared.Patterns
{
    internal static class ReflectionHelper
    {
        public static MethodInfo GetMethod<TInterface>(this Type type, Expression<Func<TInterface, Delegate>> selector)
        {
            var methodName = ((selector.Body as MemberExpression).Member as MethodInfo);
            return methodName;
            //return type.GetMethod(methodName);
        }
    }
}
