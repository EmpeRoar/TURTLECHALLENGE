using System;
using TURTLECHALLENGE.interfaces;

namespace TURTLECHALLENGE.services
{
    public class StandardInput : ICommandInput
    {
        public ITurtle Turtle;
        public Action<string> Report;
        public Action DeleteConsoleLine;
        public Func<string, bool> IsValidPlaceCommand;

        public StandardInput(ITurtle turtle,
                            Action<string> report,
                            Action deleteConsoleLine,
                            Func<string, bool> isValidPlaceCommand)
        {
            Turtle = turtle;
            Report = report;
            DeleteConsoleLine = deleteConsoleLine;
            IsValidPlaceCommand = isValidPlaceCommand;
        }

        public virtual void Execute()
        {
            Console.WriteLine("--INPUT--");
            while (true)
            {
                var input = Console.ReadLine();
                Turtle.ProcessCommand(input, Report, DeleteConsoleLine, IsValidPlaceCommand);
            }
        }
    }
}
