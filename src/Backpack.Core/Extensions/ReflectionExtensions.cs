using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Backpack.Core.Reflection;

namespace Backpack.Core.Extensions
{
    /// <summary>
    /// Methods designed to aid in common reflection operations
    /// </summary>
    public static class ReflectionExtensions
    {
        public static IEnumerable<T> GetAttributes<T>(this MemberInfo info, bool inherit = true)
        {
            return info
                .GetCustomAttributes(typeof(T), inherit)
                .Cast<T>();
        }


        public static string GetResourceString(this Type type, string name)
        {
            using (Stream s = type.Assembly.GetManifestResourceStream(type, name))
            {
                var reader = new StreamReader(s);
                return reader.ReadToEnd();
            }
        }

        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Type GetInterface(this Type sourceType, Type interfaceType)
        {
            Func<Type, bool> check = t => t.IsInterface && t.IsGenericType && t.GetGenericTypeDefinition() == interfaceType;

            if (check(sourceType))
                return sourceType;

            return sourceType.GetInterfaces().FirstOrDefault(check);
        }

        public static IEnumerable<PropertyDescription> Describe(this Type t)
        {
            foreach (var prop in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                yield return new PropertyDescription(prop);
            }
        }
    }
}