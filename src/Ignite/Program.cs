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
            if (args.Length != 2)
            {
                throw new IgnitionException(@"You need to run ignite.exe like this:

    ignite <directory> <solution_name>

e.g.

    ignite subdir SomeNewProject

or

    ignite . test");
            }

            return new IgnitionArgs
                       {
                           BaseDirectory = args[0],
                           SolutionName = args[1],
                       };
        }
    }
}