using System;
using System.Text.RegularExpressions;

namespace TURTLECHALLENGE
{
    public enum Face
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    public enum Command
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }

    public interface ITurtle
    {
        void ProcessCommand(Func<string> readLine, Action<string> report, Action deleteConsoleLine);
        bool Place(string input);
        void Move();
        void Left();
        void Right();
        string Report();
    }

    public interface ITable
    {
        int NorthEdge { get; set; }
        int SouthEdge { get; set; }
        int EastEdge { get; set; }
        int WestEdge { get; set; }
    }

    public class Table : ITable
    {
        public int NorthEdge { get; set; } = 4;
        public int SouthEdge { get; set; } = 0;
        public int EastEdge { get; set; } = 4;
        public int WestEdge { get; set; } = 0;
    }

    public interface ITurtleState
    {
        int XPos { get; set; }
        int YPos { get; set; }
        Face Face { get; set; }
    }

    public class TurtleState : ITurtleState
    {
        public int XPos { get; set; } 
        public int YPos { get; set; }
        public Face Face { get; set; }
    }

    public class Turtle : ITurtle
    {
        public bool IsPlaced { get; private set; } = false;
        private readonly ITurtleState _turtleState;
        private readonly ITable _table;
        public Turtle(ITurtleState turtleState, ITable table)
        {
            _turtleState = turtleState;
            _table = table;
        }
        public bool Place(string input)
        {
            string[] str = input.Split(" ");
            var orientation = str[1];
            string[] coordinates = orientation.Split(",");

            var xpos = Convert.ToInt32(coordinates[0]);
            var ypos = Convert.ToInt32(coordinates[1]);

            if (xpos <= _table.EastEdge && xpos >= _table.WestEdge && 
                ypos <= _table.NorthEdge && ypos >= _table.SouthEdge )
            {
                Face face;
                if (Enum.TryParse(coordinates[2].ToUpper(), out face))
                {
                    _turtleState.XPos = xpos;
                    _turtleState.YPos = ypos;
                    _turtleState.Face = face;
                    IsPlaced = true;
                    return IsPlaced;
                }
            }

            return IsPlaced;
        }
        public void Move()
        {
            switch (_turtleState.Face)
            {
                case Face.NORTH:
                    if (_turtleState.YPos < _table.NorthEdge)
                        _turtleState.YPos++;
                    break;
                case Face.SOUTH:
                    if (_turtleState.YPos > _table.SouthEdge)
                        _turtleState.YPos--;
                    break;
                case Face.EAST:
                    if(_turtleState.XPos < _table.EastEdge)
                        _turtleState.XPos++;
                    break;
                case Face.WEST:
                    if(_turtleState.XPos > _table.WestEdge)
                        _turtleState.XPos--;
                    break;
            }
        }
        public void Right()
        {
            switch (_turtleState.Face)
            {
                case Face.NORTH: _turtleState.Face = Face.EAST; break;
                case Face.SOUTH: _turtleState.Face = Face.WEST; break;
                case Face.EAST: _turtleState.Face = Face.SOUTH; break;
                case Face.WEST: _turtleState.Face = Face.NORTH; break;
            }
        }
        public void Left()
        {
            switch (_turtleState.Face)
            {
                case Face.NORTH: _turtleState.Face = Face.WEST; break;
                case Face.SOUTH: _turtleState.Face = Face.EAST; break;
                case Face.EAST: _turtleState.Face = Face.NORTH; break;
                case Face.WEST: _turtleState.Face = Face.SOUTH; break;
            }
        }
        public string Report()
        {
            return $"{_turtleState.XPos} {_turtleState.YPos} {_turtleState.Face}";
        }
        public void ProcessCommand(Func<string> readLine, Action<string> report, Action deleteConsoleLine)
        {
            var input = readLine();
            var inputs = input.ToUpper().Split(" ");
            string cmd = inputs[0];
            Command command;
            if (Enum.TryParse(cmd.ToUpper(), out command))
            {
                if (command != Command.PLACE)
                {
                    if (!IsPlaced)
                    {
                        deleteConsoleLine();
                        ProcessCommand(readLine, report, deleteConsoleLine);
                    }
                }
                else
                {
                    if (!IsValidPlaceCommand(input))
                    {
                        deleteConsoleLine();
                        ProcessCommand(readLine, report, deleteConsoleLine);
                    }
                }

                switch (command)
                {
                    case Command.PLACE:
                        if (!Place(input))
                            deleteConsoleLine();
                        break;
                    case Command.MOVE: Move(); break;
                    case Command.LEFT: Left(); break;
                    case Command.RIGHT: Right(); break;
                    case Command.REPORT:
                        Report();
                        report(Report());
                        break;
                }
            }
            else
            {
                deleteConsoleLine();
            }

            ProcessCommand(readLine, report, deleteConsoleLine);

        }

        private bool IsValidPlaceCommand(string input)
        {
            var regex = @"PLACE\s\d,\d,(NORTH|SOUTH|EAST|WEST)";
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }

    
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
            if (isValidSelection(selection))
            {
                switch (selection)
                {
                    case "1": StandardInput(); break;
                    case "2": FileInput(); break;
                }
            }
            else
            {
                StartUp();
            }
        }

        static void FileInput()
        {

        }

        static void StandardInput()
        {
            Console.WriteLine("--INPUT--");

            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            Func<string> readLine = () =>
            {
                return Console.ReadLine();
            };

            Action<string> report = (message) =>
            {
                Console.WriteLine(message);
            };

            Action deleteConsoleLine = () =>
            {
                DeletePrevConsoleLine();
            };

            turtle.ProcessCommand(readLine, report, deleteConsoleLine);
        }
        
        
        private static void DeletePrevConsoleLine()
        {
            if (Console.CursorTop == 0) return;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        private static bool isValidSelection(string input)
        {
            var regex = @"\d";
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}
