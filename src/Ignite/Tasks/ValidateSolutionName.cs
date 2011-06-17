namespace Ignite.Tasks
{
    public class ValidateSolutionName : Task
    {
        readonly string solutionName;

        public ValidateSolutionName(string solutionName)
        {
            this.solutionName = solutionName;
        }

        public override void Execute()
        {
            if (solutionName.Contains(" "))
            {
                Fail("Solution names cannot contain space characters!");
            }
        }
    }
}