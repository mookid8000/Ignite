using System;
using System.Collections.Generic;
using System.IO;
using Ignite.Tasks;

namespace Ignite
{
    class Igniter
    {
        const string SrcDirName = "src";
        const string LibDirName = "lib";
        const string ScriptsDirName = "scripts";
        
        readonly ConsoleWriter writer;

        public Igniter(ConsoleWriter writer)
        {
            this.writer = writer;
        }

        public void Ignite(IgnitionArgs args)
        {
            var tasksToCancelInCaseOfErrors = new Stack<Task>();
            var currentDirectory = args.BaseDirectory;

            var tasks = new Task[]
                            {
                                new ValidateSolutionName(args.SolutionName),

                                new EnsureDirectoryDoesNotExist(currentDirectory, SrcDirName),
                                new EnsureDirectoryDoesNotExist(currentDirectory, LibDirName),
                                new EnsureDirectoryDoesNotExist(currentDirectory, ScriptsDirName),
                                new EnsureFileDoesNotExist(Path.Combine(currentDirectory, ScriptsDirName, "build.proj")),

                                new CreateDirectory(currentDirectory, ""),

                                new CreateDirectory(currentDirectory, SrcDirName),
                                new CreateDirectory(currentDirectory, LibDirName),
                                new CreateDirectory(currentDirectory, ScriptsDirName),

                                new CreateEmptySolutionFile(Path.Combine(currentDirectory, SrcDirName), args.SolutionName),
                                new CreateDefaultMsBuildScript(Path.Combine(currentDirectory, ScriptsDirName), "build"),
                                new CreateBuildBatchScripts(currentDirectory, "build"),
                            };

            Array.ForEach(tasks, SetUpSubscriptions);

            try
            {
                foreach (var task in tasks)
                {
                    task.Execute();
                    tasksToCancelInCaseOfErrors.Push(task);
                }
            }
            catch
            {
                while (tasksToCancelInCaseOfErrors.Count > 0)
                {
                    tasksToCancelInCaseOfErrors.Pop().Cancel();
                }

                throw;
            }
        }

        void SetUpSubscriptions(Task task)
        {
            task.Output += text => writer.Write(text);
        }
    }
}