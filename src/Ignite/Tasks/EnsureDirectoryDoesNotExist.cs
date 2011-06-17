using System.IO;

namespace Ignite.Tasks
{
    public class EnsureDirectoryDoesNotExist : Task
    {
        readonly string basePath;
        readonly string relativePath;

        public EnsureDirectoryDoesNotExist(string basePath, string relativePath)
        {
            this.basePath = basePath;
            this.relativePath = relativePath;
        }

        public override void Execute()
        {
            var path = Path.Combine(basePath, relativePath);

            if (Directory.Exists(path))
            {
                Fail("The relative path '{0}' already exists in '{1}'", relativePath, basePath);
            }
        }
    }
}