using System.IO;

namespace Ignite.Tasks
{
    public class CreateDirectory : Task
    {
        readonly string basePath;
        readonly string directoryToCreate;

        public CreateDirectory(string basePath, string directoryToCreate)
        {
            this.basePath = basePath;
            this.directoryToCreate = directoryToCreate;
        }

        public override void Execute()
        {
            var path = Path.Combine(basePath, directoryToCreate);
            Info("Creating directory: {0}", path);
            Directory.CreateDirectory(path);
        }
    }
}