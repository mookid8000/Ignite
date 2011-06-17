using System;

namespace Ignite.Exceptions
{
    public class IgnitionException : ApplicationException
    {
        public IgnitionException(string message, params object[] objs) : base(string.Format(message, objs))
        {
        }
    }
}