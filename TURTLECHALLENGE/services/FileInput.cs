using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TURTLECHALLENGE.extensions;
using TURTLECHALLENGE.interfaces;

namespace TURTLECHALLENGE.services
{
    public class FileInput
    {
        ITurtle _turtle;
        Action<string> _report;
        Action _deleteConsoleLine;
        Func<string, bool> _isValidPlaceCommand;
        bool _firstPrompt = true;

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
            while (true)
            {
                var path = AskFilePath();
                Console.WriteLine("--INPUT--");

                var inputSequence = (string.IsNullOrEmpty(path) || path.IsNotValidPath())  ?  
                                    new List<string>() :
                                    ReadCommandsFromFile(path);

                foreach (var input in inputSequence)
                    _turtle.ProcessCommand(input, _report, _deleteConsoleLine, _isValidPlaceCommand);
            }
        }
        
        private IEnumerable<string> ReadCommandsFromFile(string path)
        {
            using (var reader = new StreamReader(@path))
            {
                if (_firstPrompt) _firstPrompt = false;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    yield return line;
                }
            }
        }

        private string AskFilePath()
        {
            var another = !_firstPrompt ? " another " : " ";
            Console.WriteLine($"Paste{another}file location...");
            return Console.ReadLine();
        }

    }
}
