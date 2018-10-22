using System;
using TURTLECHALLENGE.interfaces;

namespace TURTLECHALLENGE.services
{
    public class StandardInput
    {
        ITurtle _turtle;
        Action<string> _report;
        Action _deleteConsoleLine;
        Func<string, bool> _isValidPlaceCommand;
        public StandardInput(ITurtle turtle,
                            Action<string> report,
                            Action deleteConsoleLine,
                            Func<string, bool> isValidPlaceCommand)
        {
            _turtle = turtle;
            _report = report;
            _deleteConsoleLine = deleteConsoleLine;
            _isValidPlaceCommand = isValidPlaceCommand;
        }
        public void Execute()
        {
            Console.WriteLine("--INPUT--");
            while (true)
            {
                var input = Console.ReadLine();
                _turtle.ProcessCommand(input, _report, _deleteConsoleLine, _isValidPlaceCommand);
            }
        }
    }
}
