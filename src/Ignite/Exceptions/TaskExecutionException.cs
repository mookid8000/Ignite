namespace Ignite.Exceptions
{
    public class TaskExecutionException : IgnitionException
    {
        public TaskExecutionException(string message, params object[] objs) : base(message, objs)
        {
        }
    }
}