using System.IO;

namespace Ignite.Tasks
{
    class EnsureFileDoesNotExist : Task
    {
        readonly string path;

        public EnsureFileDoesNotExist(string path)
        {
            this.path = path;
        }

        public override void Execute()
        {
            if (File.Exists(path))
            {
                Fail("There's already a file named '{0}'.");
            }
        }
    }
}