using System;
using System.Reflection;
using Ignite.Exceptions;

namespace Ignite
{
    class Program
    {
        readonly ConsoleWriter writer;
        readonly Igniter igniter;

        public Program(ConsoleWriter writer, Igniter igniter)
        {
            this.writer = writer;
            this.igniter = igniter;
        }

        void Run(IgnitionArgs args)
        {
            igniter.Ignite(args);
        }

        static void Main(string[] args)
        {
            var writer = new ConsoleWriter();

            try
            {
                writer.Write(
                    @"Ignite v. {0}

(c) 2011 Mogens Heller Grabe
mookid8000@gmail.com
http://mookid.dk/oncode
",
                    Assembly.GetExecutingAssembly().GetName().Version);

                new Program(writer, new Igniter(writer))
                    .Run(ParseArguments(args));

                Environment.ExitCode = 0;
            }
            catch (IgnitionException e)
            {
                writer.Error(e.Message);

                Environment.ExitCode = 1;
            }
            catch (Exception e)
            {
                writer.WriteError(e);

                Environment.ExitCode = 2;
            }
        }

        static IgnitionArgs ParseArguments(string[] args)
        {
            if (args.Length != 1)
            {
                throw new IgnitionException(@"You need to specify the name of the solution to create.");
            }

            return new IgnitionArgs{SolutionName = args[0]};
        }
    }
}