namespace DomainModel
{
    using System.Threading;
    using System.Threading.Tasks;

    internal static class TaskExtensions
    {
        public static Task ThrowIfFaulted(this Task task)
        {
            task.ContinueWith(
                t => ThreadPool.QueueUserWorkItem(a => { throw t.Exception; }),
                TaskContinuationOptions.OnlyOnFaulted);

            return task;
        }

        public static Task<T> ThrowIfFaulted<T>(this Task<T> task)
        {
            task.ContinueWith(
                t => ThreadPool.QueueUserWorkItem(a => { throw t.Exception; }),
                TaskContinuationOptions.OnlyOnFaulted);

            return task;
        }
    }
}