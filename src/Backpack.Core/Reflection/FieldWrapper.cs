using System.Reflection;

namespace Backpack.Core.Reflection
{
    /// <summary>
    /// Abstracts field access for Get/Set operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class FieldWrapper<T> : IMemberWrapper<T> where T : class
    {
        private FieldInfo _fi;

        public FieldWrapper(FieldInfo pi)
        {
            _fi = pi;
        }

        public T GetValue(object owner) { return _fi.GetValue(owner) as T; }
        public void SetValue(object owner, T o) { _fi.SetValue(owner, o); }

        public AttributeT GetSingleAttribute<AttributeT>() where AttributeT : class
        {
            object[] attrs = _fi.GetCustomAttributes(typeof(AttributeT), true);
            if (attrs == null || attrs.Length != 1)
                return null;
            else
                return attrs[0] as AttributeT;
        }
    }

}