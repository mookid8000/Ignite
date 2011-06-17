using System;
using Ignite.Exceptions;

namespace Ignite.Tasks
{
    public abstract class Task
    {
        public event Action<string> Output = delegate { };

        public abstract void Execute();
        public virtual void Cancel() {}

        protected void Info(string message, params object[] objs)
        {
            Output(string.Format(message, objs));
        }

        protected void Fail(string message, params object[] objs)
        {
            throw new TaskExecutionException(message, objs);
        }
    }
}