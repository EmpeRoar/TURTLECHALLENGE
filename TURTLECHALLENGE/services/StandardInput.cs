using System;
using TURTLECHALLENGE.interfaces;

namespace TURTLECHALLENGE.services
{
    public class StandardInput : ICommandInput
    {
        public ITurtle Turtle;
        public Action<string> Report;
        public Action ClearCommandLine;
        public Func<string, bool> IsValidPlaceCommand;

        public StandardInput(ITurtle turtle,
                            Action<string> report,
                            Action clearCommandLine,
                            Func<string, bool> isValidPlaceCommand)
        {
            Turtle = turtle;
            Report = report;
            ClearCommandLine = clearCommandLine;
            IsValidPlaceCommand = isValidPlaceCommand;
        }

        public virtual void Execute()
        {
            Console.WriteLine("--INPUT--");
            while (true)
            {
                var input = Console.ReadLine();
                Turtle.ProcessCommand(input, Report, ClearCommandLine, IsValidPlaceCommand);
            }
        }
    }
}
