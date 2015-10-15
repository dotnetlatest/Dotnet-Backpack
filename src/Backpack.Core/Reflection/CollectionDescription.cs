using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Backpack.Core.Reflection
{
    /// <summary>
    /// Simplifies working with a collection type, including adding items to the collection
    /// </summary>
    public class CollectionDescription
    {
        public Type ItemType { get; private set; }
        public MethodInfo AddItemMethod { get; private set; }

        private Action<object, object> _Adder;

        public static CollectionDescription Create(Type type)
        {
            Type listType = GetListTypes(type).FirstOrDefault();

            if (listType == null)
                return null;

            Type itemType = listType.GetGenericArguments()[0];
            MethodInfo method = GetAddItemMethod(type, itemType) ?? GetAddItemMethod(listType, itemType);

            var desc = new CollectionDescription
            {
                ItemType = itemType,
                AddItemMethod = method
            };

            if (method != null)
                desc._Adder = CreateAdder(type, method, itemType);

            return desc;

        }

        public void Add(object target, object value)
        {
            if (_Adder == null)
            {
                throw new InvalidOperationException("Tried to add to collection, which did not have a valid adder");
            }

            _Adder(target, value);
        }

        private static MethodInfo GetAddItemMethod(Type listType, Type listItemType)
        {
            return listType.GetMethod("Add", new[] { listItemType });
        }

        private static Action<object, object> CreateAdder(Type type, MethodInfo method, Type elementType)
        {
            var param = Expression.Parameter(typeof(object));
            var valueParam = Expression.Parameter(typeof(object));
            return Expression.Lambda<Action<object, object>>(Expression.Call(Expression.Convert(param, type), method, Expression.Convert(valueParam, elementType)), param, valueParam).Compile();
        }

        public static IEnumerable<Type> GetListTypes(Type type)
        {
            Type listType = typeof(ICollection<>);

            foreach (Type t in type.GetInterfaces())
            {
                if (t.IsGenericType && t.GetGenericTypeDefinition() == listType)
                    yield return t;

                foreach (Type child in GetListTypes(t))
                {
                    yield return child;
                }
            }
        }
    }
}