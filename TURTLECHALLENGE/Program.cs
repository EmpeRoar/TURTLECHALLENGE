using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TURTLECHALLENGE.model;
using TURTLECHALLENGE.objects;

namespace TURTLECHALLENGE
{

    class Program
    {
        static void Main(string[] args)
        {
            StartUp();
        }

        static void StartUp()
        {
            Console.WriteLine("Choose Options");
            Console.WriteLine("1. Standard Input 2. Input from FILE");
            var selection = Console.ReadLine();
            switch (selection)
            {
                case "1": StandardInput(); break;
                case "2": FileInput(); break;
                default: BackToStartUp(); break;
            }
        }

        static void BackToStartUp()
        {
            StartUp();
        }

        static void FileInput()
        {
            int counter = 0;
            while (true)
            {
                var path = AskFilePath(counter);
                Console.WriteLine("--INPUT--");
                var turtle = SetupTurtle();
                var inputSequence = ReadCommandsFromFile(path);
                foreach (var input in inputSequence)
                {
                    turtle.ProcessCommand(input,
                                          (message) => Console.WriteLine(message),
                                          () => { },
                                          (command) => IsValidPlaceCommand(command));
                }
                counter++;
            }
        }

        static void StandardInput()
        {
            Console.WriteLine("--INPUT--");
            var turtle = SetupTurtle();
            while (true)
            {
                var input = Console.ReadLine();
                turtle.ProcessCommand(input,
                                     (message) => Console.WriteLine(message),
                                     () => DeletePrevConsoleLine(),
                                     (command) => IsValidPlaceCommand(command));
            }
        }

        static Turtle SetupTurtle()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            return new Turtle(turtleState, table);
        } 

        static string AskFilePath(int counter)
        {
            var another = counter > 0 ? " another " : " ";
            Console.WriteLine($"Paste{another}file location...");
            return Console.ReadLine();
        }

        static List<string> ReadCommandsFromFile(string path)
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

        public static bool IsValidPlaceCommand(string input)
        {
            var regex = @"PLACE\s\d,\d,(NORTH|SOUTH|EAST|WEST)";
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            return match.Success;
        }

        private static void DeletePrevConsoleLine()
        {
            if (Console.CursorTop == 0) return;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
