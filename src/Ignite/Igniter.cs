using System;
using System.IO;
using Ignite.Tasks;

namespace Ignite
{
    class Igniter
    {
        readonly ConsoleWriter writer;

        public Igniter(ConsoleWriter writer)
        {
            this.writer = writer;
        }

        public void Ignite(IgnitionArgs args)
        {
            var currentDirectory = Environment.CurrentDirectory;

            var tasks = new Task[]
                            {
                                new ValidateSolutionName(args.SolutionName),

                                new EnsureDirectoryDoesNotExist(currentDirectory, "src"),
                                new EnsureDirectoryDoesNotExist(currentDirectory, "lib"),

                                new CreateDirectory(currentDirectory, "src"),
                                new CreateDirectory(currentDirectory, "lib"),

                                new CreateEmptySolutionFile(Path.Combine(currentDirectory, "src"), args.SolutionName),
                            };

            Array.ForEach(tasks, SetUpSubscriptions);

            foreach (var task in tasks)
            {
                task.Execute();
            }
        }

        void SetUpSubscriptions(Task task)
        {
            task.Output += text => writer.Write(text);
        }
    }
}