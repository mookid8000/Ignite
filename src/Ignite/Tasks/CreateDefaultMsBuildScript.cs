using System.IO;
using System.Text;

namespace Ignite.Tasks
{
    class CreateDefaultMsBuildScript : Task
    {
        readonly string directory;
        readonly string name;

        public CreateDefaultMsBuildScript(string directory, string name)
        {
            this.directory = directory;
            this.name = name;
        }

        public override void Execute()
        {
            var filename = GetFilename();
            Info("Generating default MsBuild script: {0}", filename);
            File.WriteAllText(filename, @"<Project DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <ItemGroup>
    <AllProjects Include=""..\src\**\*.csproj"" />
  </ItemGroup>

  <PropertyGroup>
  </PropertyGroup>

  <Target Name=""build"">
    <MSBuild Projects=""@(AllProjects)"" Targets=""build"" StopOnFirstFailure=""true"" Properties=""Configuration=Release"">
      <Output TaskParameter=""TargetOutputs"" ItemName=""BuildOutput"" />
    </MSBuild>
  </Target>

</Project>", Encoding.UTF8);
        }

        public override void Cancel()
        {
            var filename = GetFilename();
            Info("Removing default MsBuild script: {0}", filename);
            File.Delete(filename);
        }

        string GetFilename()
        {
            return Path.Combine(directory, string.Format("{0}.proj", name));
        }
    }
}