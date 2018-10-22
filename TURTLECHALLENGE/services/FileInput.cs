using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TURTLECHALLENGE.extensions;
using TURTLECHALLENGE.interfaces;
using TURTLECHALLENGE.objects;

namespace TURTLECHALLENGE.services
{
    public class FileInput
    {
        ITurtle _turtle;
        Action<string> _report;
        Action _deleteConsoleLine;
        Func<string, bool> _isValidPlaceCommand;
        public FileInput(ITurtle turtle,
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
            int counter = 0;
            while (true)
            {
                var path = AskFilePath(counter);
                Console.WriteLine("--INPUT--");
                var inputSequence = string.IsNullOrEmpty(path) ? 
                                    new List<string>() : 
                                    ReadCommandsFromFile(path);
                foreach (var input in inputSequence)
                    _turtle.ProcessCommand(input, _report, _deleteConsoleLine, _isValidPlaceCommand);               
                counter++;
            }
        }

        private string AskFilePath(int counter)
        {
            var another = counter > 0 ? " another " : " ";
            Console.WriteLine($"Paste{another}file location...");
            return Console.ReadLine();
        }

        private List<string> ReadCommandsFromFile(string path)
        {
            var inputSequence = new List<string>();
            using (var reader = new StreamReader(@path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    inputSequence.Add(line);
                }
                return inputSequence;
            }
        }

    }
}
