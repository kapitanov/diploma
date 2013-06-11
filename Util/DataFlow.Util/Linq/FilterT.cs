using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace AISTek.DataFlow.Util.Linq
{
    internal sealed class Filter<T> : IFilter<T>
    {
        #region Private members

        internal Filter(IQueryable<T> queryable, IDisposable context)
        {
            Contract.Requires(queryable != null);
            Contract.Requires(context != null);

            this.queryable = queryable;
            this.context = context;
        }

        private readonly IQueryable<T> queryable;
        private readonly IDisposable context;

        #endregion

        #region Implementation of IEnumerable

        /// <summary>
        /// Возвращает перечислитель, выполняющий перебор элементов в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return queryable.GetEnumerator();
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
            return (this as IEnumerable<T>).GetEnumerator();
        }

        #endregion

        #region Implementation of IQueryable

        /// <summary>
        /// Получает выражение, связанное с экземпляром класса <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>
        /// Выражение <see cref="T:System.Linq.Expressions.Expression"/>, связанное с данным экземпляром класса <see cref="T:System.Linq.IQueryable"/>.
        /// </returns>
        Expression IQueryable.Expression { get { return queryable.Expression; } }

        /// <summary>
        /// Получает тип элементов, которые возвращаются при выполнении дерева выражения, связанного с данным экземпляром класса <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>
        /// Тип <see cref="T:System.Type"/>, представляющий тип элементов, которые возвращаются при выполнении дерева выражения, связанного с данным объектом.
        /// </returns>
        Type IQueryable.ElementType { get { return queryable.ElementType; } }

        /// <summary>
        /// Возвращает объект поставщика запросов, связанного с указанным источником данных.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.Linq.IQueryProvider"/>, связанный с указанным источником данных.
        /// </returns>
        IQueryProvider IQueryable.Provider { get { return queryable.Provider; } }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        void IDisposable.Dispose()
        {
            context.Dispose();
        }

        #endregion
    }
}
