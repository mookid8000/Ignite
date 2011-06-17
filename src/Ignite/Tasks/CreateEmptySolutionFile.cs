using System.IO;
using System.Text;

namespace Ignite.Tasks
{
    public class CreateEmptySolutionFile : Task
    {
        readonly string directory;
        readonly string solutionName;

        public CreateEmptySolutionFile(string directory, string solutionName)
        {
            this.directory = directory;
            this.solutionName = solutionName;
        }

        public override void Execute()
        {
            const string slnVs2010 = @"
Microsoft Visual Studio Solution File, Format Version 11.00
# Visual Studio 2010
Global
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";

            var filename = Path.Combine(directory, string.Format("{0}.sln", solutionName));
            Info("Generating empty VS2010 solution file: {0}", filename);
            File.WriteAllText(filename, slnVs2010, Encoding.UTF8);
        }
    }
}