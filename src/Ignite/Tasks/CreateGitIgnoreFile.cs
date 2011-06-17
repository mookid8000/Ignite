using System.IO;

namespace Ignite.Tasks
{
    public class CreateGitIgnoreFile : Task
    {
        readonly string directory;

        public CreateGitIgnoreFile(string directory)
        {
            this.directory = directory;
        }

        public override void Execute()
        {
            var filename = GetFilename();
            Info("Generating Git ignore file: {0}", filename);
            File.WriteAllText(filename, @"obj
bin
deploy
deploy/*
_ReSharper.*
*.csproj.user
*.resharper.user
*.ReSharper.user
*.resharper
*.suo
*.cache
~$*");
        }

        public override void Cancel()
        {
            var filename = GetFilename();
            Info("Removing Git ignore file: {0}", filename);
            File.Delete(filename);
        }

        string GetFilename()
        {
            return Path.Combine(directory, ".gitignore");
        }
    }
}