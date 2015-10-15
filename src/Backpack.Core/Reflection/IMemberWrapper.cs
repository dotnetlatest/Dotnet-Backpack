namespace Backpack.Core.Reflection
{
    /// <summary>
    /// Represents common operations needed for setting a member property or field.
    /// </summary>
    /// <remarks>
    /// System.Reflection field and property classes require different code for
    /// getting/setting a field or property. This provides a common interface to avoid
    /// branching code every time the properties are accessed. The tradeoff is that 
    /// this does not work for indexed properties.
    /// </remarks>
    /// <typeparam name="T">The member type</typeparam>
    internal interface IMemberWrapper<T> where T : class
    {
        /// <summary>
        /// Gets the value of the member
        /// </summary>
        /// <param name="owner">The object to which the member belongs</param>
        /// <returns>The member's value</returns>
        T GetValue(object owner);

        /// <summary>
        /// Sets the value of the member
        /// </summary>
        /// <param name="owner">The object to set the member of</param>
        /// <param name="o">The value to set the member to</param>
        void SetValue(object owner, T o);

        /// <summary>
        /// Gets a single custom attrbute by type
        /// </summary>
        /// <typeparam name="AttributeT">The type of the attribute to get</typeparam>
        /// <returns>The attribute</returns>
        AttributeT GetSingleAttribute<AttributeT>() where AttributeT : class;
    }
}