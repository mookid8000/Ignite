using System;
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
            var solutionItemsProjectId = Guid.NewGuid();

            var slnVs2010 =
                @"
Microsoft Visual Studio Solution File, Format Version 11.00
# Visual Studio 2010
Project(""{2150E333-8FDC-42A3-9474-1A3956D46DE8}"") = ""Solution Items"", ""Solution Items"", ""{solutionItemsProjectId}""
	ProjectSection(SolutionItems) = preProject
		..\scripts\build.proj = ..\scripts\build.proj
	EndProjectSection
EndProject
Global
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
"
                    .Replace("{solutionItemsProjectId}", solutionItemsProjectId.ToString("B").ToUpper());

            var filename = GetFilename();
            Info("Generating empty VS2010 solution file: {0}", filename);
            File.WriteAllText(filename, slnVs2010, Encoding.UTF8);
        }

        public override void Cancel()
        {
            var filename = GetFilename();
            Info("Removing empty VS2010 solution file: {0}", filename);
            File.Delete(filename);
        }

        string GetFilename()
        {
            return Path.Combine(directory, string.Format("{0}.sln", solutionName));
        }
    }
}