using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer.Application.Helper
{
    public class QueueProcessor<T>
    {
        private readonly Queue<T> _queue = new Queue<T>();
        private readonly Func<T, Task> _processQueueItem;
        
        public QueueProcessor(Func<T, Task> processQueueItem)
        {
            _processQueueItem = processQueueItem ?? throw new ArgumentNullException(nameof(processQueueItem));
        }

        public void OnQueueItemReceived(T queueItem)
        {
            bool startThread;

            lock (_queue)
            {
                startThread = _queue.Count == 0;
                _queue.Enqueue(queueItem);
            }

            if (startThread)
                Task.Run(ProcessQueue);
        }

        private void ProcessQueue()
        {
            while (true)
            {
                T queueItem;
                lock (_queue)
                {
                    if (_queue.Count == 0)
                        return;

                    queueItem = _queue.Peek();
                }

                try
                {
                    if (queueItem != null)
                    {
                        _processQueueItem(queueItem).ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                lock (_queue)
                {
                    _queue.Dequeue();
                    if (_queue.Count == 0)
                        return;
                }
            }
        }
    }
}