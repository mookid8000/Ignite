using System.IO;

namespace Ignite.Tasks
{
    public class CreateBuildBatchScripts : Task
    {
        readonly string directory;
        readonly string scriptFilename;

        public CreateBuildBatchScripts(string directory, string scriptFilename)
        {
            this.directory = directory;
            this.scriptFilename = scriptFilename;
        }

        public override void Execute()
        {
            var filename = GetFilename();
            Info("Generating build invocation script: {0}", filename);
            File.WriteAllText(filename, @"%WINDIR%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe scripts\build.proj");
        }

        string GetFilename()
        {
            return Path.Combine(directory, string.Format("{0}.bat", scriptFilename));
        }

        public override void Cancel()
        {
            var filename = GetFilename();
            Info("Removing build invocation script: {0}", filename);
            File.Delete(filename);
        }
    }
}