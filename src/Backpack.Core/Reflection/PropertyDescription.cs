using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Backpack.Core.Reflection
{
    /// <summary>
    /// Provides simpler access to accessing the getter and setter of a property efficiently
    /// </summary>
    public class PropertyDescription
    {
        public PropertyDescription(PropertyInfo property)
        {
            Property = property;

            MethodInfo setMethod = property.GetSetMethod();
            IsWritable = setMethod != null && setMethod.IsPublic;

            if (setMethod != null)
                _Setter = CreateSetter(property, setMethod);

            CollectionInfo = CollectionDescription.Create(property.PropertyType);
        }

        public PropertyInfo Property { get; private set; }
        public bool IsWritable { get; private set; }
        public CollectionDescription CollectionInfo { get; private set; }
        private Action<object, object> _Setter;

        private Action<object, object> CreateSetter(PropertyInfo propertyInfo, MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object), "i");
            var argument = Expression.Parameter(typeof(object), "a");
            var setterCall = Expression.Call(
                Expression.Convert(instance, propertyInfo.DeclaringType),
                method,
                Expression.Convert(argument, propertyInfo.PropertyType));

            return (Action<object, object>)Expression.Lambda(setterCall, instance, argument).Compile();
        }

        public object Get(object target)
        {
            return Property.GetValue(target, null);
        }

        public void Set(object target, object value)
        {
            if (_Setter == null)
            {
                string message = String.Format("Tried to set property '{0}', which does not have a valid setter.", Property.Name);
                throw new InvalidOperationException(message);
            }

            _Setter(target, value);
            //Property.SetValue(target, value, null);
        }

        #region IL version
        //private Action<object, object> CreateSetterDynamicMethod(PropertyInfo propertyInfo, MethodInfo method)
        //{
        //    var watch = Stopwatch.StartNew();
        //    try
        //    {
        //        /*
        //        * Create the dynamic method
        //        */
        //        Type[] arguments = new Type[2];
        //        arguments[0] = arguments[1] = typeof(object);

        //        DynamicMethod setter = new DynamicMethod(
        //          String.Concat("_Set", propertyInfo.Name, "_"),
        //          typeof(void), arguments, propertyInfo.DeclaringType);

        //        ILGenerator generator = setter.GetILGenerator();
        //        generator.Emit(OpCodes.Ldarg_0);
        //        generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
        //        generator.Emit(OpCodes.Ldarg_1);

        //        if (propertyInfo.PropertyType.IsClass)
        //            generator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
        //        else
        //            generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);

        //        generator.EmitCall(OpCodes.Callvirt, method, null);
        //        generator.Emit(OpCodes.Ret);

        //        return (Action<object, object>)setter.CreateDelegate(typeof(Action<object, object>));
        //    }
        //    finally
        //    {
        //        watch.Stop();
        //        Console.WriteLine(watch.Elapsed);
        //    }
        //}
        #endregion
    }
}