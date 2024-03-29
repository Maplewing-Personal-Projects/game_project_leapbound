﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace LaYumba.Functional
{
    public static class Immutable
    {
        public static T With<T>(this T source, string propertyName, object newValue)
           where T : class
        {
            T @new = source.ShallowCopy();

            typeof(T).GetField(propertyName)
               .SetValue(@new, newValue);

            return @new;
        }

        public static T With<T, P>(this T source, Expression<Func<T, P>> exp, P newValue)
           where T : class
           => source.With(exp.MemberName(), newValue);

        static string MemberName<T, P>(this Expression<Func<T, P>> e)
           => ((MemberExpression)e.Body).Member.Name;

        static T ShallowCopy<T>(this T source)
           => (T)source.GetType().GetTypeInfo().GetMethod("MemberwiseClone"
                 , BindingFlags.Instance | BindingFlags.NonPublic)
              .Invoke(source, null);
        
        static FieldInfo GetField(this Type t, string propertyName)
           => t.GetTypeInfo().GetField(propertyName
              , BindingFlags.Instance | BindingFlags.NonPublic);
    }
}