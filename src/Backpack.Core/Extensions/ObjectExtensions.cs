using System;
using System.ComponentModel;
using System.Linq;

namespace Backpack.Core.Extensions
{
    /// <summary>
    /// Methods to make working with all objects simpler
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Helper method to dereference a property, even if the provided object is null
        /// </summary>
        /// <typeparam name="T">Type of the object to dereference a property from</typeparam>
        /// <typeparam name="TResult">Type of the property</typeparam>
        /// <param name="item">The item to dereference the property from</param>
        /// <param name="deriver">The method that should be used to refer to the property</param>
        /// <returns>The value of the property on the object if not null, otherwise the default value for that type</returns>
        public static TResult Ref<T, TResult>(this T item, Func<T, TResult> deriver)
        {
            return Ref<T, TResult>(item, deriver, default(TResult));
        }

        /// <summary>
        /// Helper method to dereference a property, even if the provided object is null
        /// </summary>
        /// <typeparam name="T">Type of the object to dereference a property from</typeparam>
        /// <typeparam name="TResult">Type of the property</typeparam>
        /// <param name="item">The item to dereference the property from</param>
        /// <param name="deriver">The method that should be used to refer to the property</param>
        /// <param name="defaultValue">The value to return if the source object is null</param>
        /// <returns>The value of the property on the object if not null, otherwise the default value specified</returns>
        public static TResult Ref<T, TResult>(this T item, Func<T, TResult> deriver, TResult defaultValue)
        {
            if (item == null)
                return defaultValue;

            return deriver(item);
        }

        /// <summary>
        /// Convert this object to a different type
        /// </summary>
        /// <typeparam name="T">The type to convert this object to</typeparam>
        /// <param name="value">The value of the object to convert from</param>
        /// <param name="strict">Whether to throw exceptions on non-nullable types and other mismatches</param>
        /// <returns>The converted value</returns>
        public static T ConvertValue<T>(this object value, bool strict = false)
        {
            return (T)ConvertValue(value, typeof(T), strict);
        }

        /// <summary>
        /// Convert this object to a different type
        /// </summary>
        /// <param name="value">The value of the object to convert from</param>
        /// <param name="targetType">The type to convert this object to</param>
        /// <param name="strict">Whether to throw exceptions on non-nullable types and other mismatches</param>
        /// <returns>The converted value</returns>
        public static object ConvertValue(this object value, Type targetType, bool strict = false)
        {
            if (value == null || value == DBNull.Value)
            {
                if (targetType.IsValueType && !targetType.IsNullable())
                {
                    if (strict)
                    {
                        string msg = String.Format("Null value cannot be converted to type '{0}'", targetType.Name);
                        throw new ArgumentNullException(msg);
                    }
                    else
                    {
                        return Activator.CreateInstance(targetType); // return default for null
                    }
                }

                return null;
            }

            Type valueType = value.GetType();

            // TODO: support nullable enums?

            if (targetType.IsAssignableFrom(valueType))
                return value;

            if (targetType.IsEnum && Enum.GetUnderlyingType(targetType).IsAssignableFrom(valueType))
                return value;

            TypeConverter converter = TypeDescriptor.GetConverter(targetType);

            object o;
            if (converter.CanConvertFrom(valueType))
            {
                o = converter.ConvertFrom(value);
            }
            else
            {
                converter = TypeDescriptor.GetConverter(valueType);

                if (converter.CanConvertTo(targetType))
                {
                    o = converter.ConvertTo(value, targetType);
                }
                else
                {
                    // hail mary pass
                    if (targetType.IsNullable())
                        targetType = Nullable.GetUnderlyingType(targetType);

                    if (targetType.IsArray && value is string)
                    {
                        Type elementType = targetType.GetElementType();

                        object[] valueArray = value.ToString().Split(',').Select(s => ConvertValue(s, elementType)).ToArray();

                        Array typedArray = Array.CreateInstance(elementType, valueArray.Length);

                        Array.Copy(valueArray, typedArray, valueArray.Length);

                        o = typedArray;
                    }
                    else
                    {
                        o = Convert.ChangeType(value, targetType);
                    }
                }
            }

            return o;
        }
    }
}