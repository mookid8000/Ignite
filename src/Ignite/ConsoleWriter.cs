using System;

namespace Ignite
{
    class ConsoleWriter
    {
        readonly ConsoleColor defaultForegroundColor;

        public ConsoleWriter()
        {
            defaultForegroundColor = Console.ForegroundColor;
        }

        public ConsoleWriter Write(string message, params object[] objs)
        {
            WriteInternal(defaultForegroundColor, message, objs);
            return this;
        }

        public ConsoleWriter WriteError(Exception exception)
        {
            WriteInternal(ConsoleColor.Red, "{0}", exception);
            return this;
        }

        public ConsoleWriter Warn(string message)
        {
            WriteInternal(ConsoleColor.Yellow, message);
            return this;
        }

        public ConsoleWriter Error(string message)
        {
            WriteInternal(ConsoleColor.Red, message);
            return this;
        }

        void WriteInternal(ConsoleColor color, string message, params object[] objs)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message, objs);
            Console.ForegroundColor = defaultForegroundColor;
        }
    }
}