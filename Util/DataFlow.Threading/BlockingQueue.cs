using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using AISTek.DataFlow.Util;

namespace AISTek.DataFlow.Threading
{
    /// <summary>
    /// Implements generic thread-safe queue.
    /// </summary>
    /// <typeparam name="T">
    /// A type of objects to contain.
    /// </typeparam>
    public class BlockingQueue<T> : IEnumerable<T>
    {
        /// <summary>
        /// Initializes new instance of <see cref="BlockingQueue{T}"/> class.
        /// </summary>
        public BlockingQueue()
        {
            //  DebugTracing.InstanceCreated(this);
        }

        /// <summary>
        /// Add an item to the top of queue.
        /// </summary>
        /// <param name="item">
        /// Item to add.
        /// </param>
        public void Enqueue(T item)
        {
            Contract.Requires(item != null);

            lock (synchRoot)
            {
                queue.Enqueue(item);
                Debug.Print("AISTek.DataFlow.Threading.BlockingQueue.Enqueue({0})", item);
                Monitor.Pulse(synchRoot);
            }
        }

        /// <summary>
        /// Takes one item from the bottom of queue.
        /// If no items are available, blocks calling thread until an item becomes available.
        /// </summary>
        /// <returns>
        /// An item to return.
        /// </returns>
        public T Dequeue()
        {
            lock (synchRoot)
            {
                while (queue.Count == 0)
                {
                    Monitor.Wait(synchRoot);
                }

                var item = queue.Dequeue();
                return item;
            }
        }

        /// <summary>
        /// Takes one item from the bottom of queue.
        /// If no items are available, blocks calling thread until an item becomes available.
        /// </summary>
        /// <returns>
        /// An item to return.
        /// </returns>
        public T Dequeue(TimeSpan timeout)
        {
            lock (synchRoot)
            {
                while (queue.Count == 0)
                {
                    if (!Monitor.Wait(synchRoot, timeout))
                        throw new TimeoutException("BlockingQueue.Dequeue(): timeout expired.");
                }

                var item = queue.Dequeue();
                return item;
            }
        }

        /// <summary>
        /// Removes an item from the queue.
        /// </summary>
        /// <param name="item">
        /// Item to remove.
        /// </param>
        public void Remove(T item)
        {
            lock (synchRoot)
            {
                queue = new Queue<T>(queue.Where(x => !x.Equals(item)));
            }
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerator{T}"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Code contract invariant

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(queue != null);
        }

        #endregion

        private Queue<T> queue = new Queue<T>();
        private readonly object synchRoot = new object();
    }
}
