using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace AISTek.DataFlow.Threading
{
    /// <summary>
    /// Represents a strongly typed immutable FIFO list of objects.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in the list.
    /// </typeparam>
    public class ImmutableList<T> : IEnumerable<T>
    {
        private ImmutableList(IEnumerable<T> items)
        {
            Contract.Requires(items != null);
            this.items = items.ToList();
        }

        /// <summary>
        /// Gets an instance of <see cref="ImmutableList{T}"/> which represents empty list.
        /// </summary>
        public static ImmutableList<T> Empty = new ImmutableList<T>(Enumerable.Empty<T>());

        /// <summary>
        /// Add an item to the top of the list list.
        /// </summary>
        /// <param name="item">
        /// Item to add.
        /// </param>
        /// <returns>
        /// The list with added item.
        /// </returns>
        public ImmutableList<T> Add(T item)
        {
            return new ImmutableList<T>(new[] { item }.Concat(items));
        }

        /// <summary>
        /// Takes out one item from the bottom the list.
        /// </summary>
        /// <param name="item">
        /// "Out" parameter - item to return.
        /// </param>
        /// <returns>
        /// The list without item that has been taken out.
        /// </returns>
        public ImmutableList<T> Take(out T item)
        {
            if(items.Count == 0)
                throw new InvalidOperationException("List is empty");
            item = items.Last();
            return (items.Count == 1)
                       ?
                           Empty
                       :
                           new ImmutableList<T>(items.Take(items.Count - 1));
        }

        private readonly IList<T> items;

        #region Implementation of IEnumerable

        /// <summary>
        /// Возвращает перечислитель, выполняющий перебор элементов в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Возвращает перечислитель, который осуществляет перебор элементов коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.Collections.IEnumerator"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(items != null);
        }
    }
}
