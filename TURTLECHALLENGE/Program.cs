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
        void Place(string input);
        void Move();
        void Left();
        void Right();
        string Report();
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
      
        private const int TOP = 4;
        private const int BOTTOM = 0;

        private ITurtleState _turtleState;
        public Turtle(ITurtleState turtleState)
        {
            _turtleState = turtleState;
        }

        public Turtle()
        {
            _turtleState = new TurtleState();
        }

        public void Place(string input)
        {
            string[] str = input.Split(" ");
            var orientation = str[1];
            string[] coordinates = orientation.Split(",");

            Face face;
            if (Enum.TryParse(coordinates[2].ToUpper(), out face))
            {
                _turtleState.XPos = Convert.ToInt32(coordinates[0]);
                _turtleState.YPos = Convert.ToInt32(coordinates[1]);
                _turtleState.Face = face;
                IsPlaced = true;
            }

        }

        public void Move()
        {
            switch (_turtleState.Face)
            {
                case Face.NORTH:
                    if (_turtleState.YPos < TOP)
                        _turtleState.YPos++;
                    break;
                case Face.SOUTH:
                    if (_turtleState.YPos > BOTTOM)
                        _turtleState.YPos--;
                    break;
                case Face.EAST:
                    if(_turtleState.XPos < TOP)
                        _turtleState.XPos++;
                    break;
                case Face.WEST:
                    if(_turtleState.XPos > BOTTOM)
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
    }

    public static class CommandExtension
    {
        public static bool IsValidPlaceCommand(this string input)
        {
            var regex = @"([A-Z])\w+\s\d,\d,([A-Z])\w+";
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return false;
            }

            string[] str = input.Split(" ");
            var orientation = str[1];
            string[] coordinates = orientation.Split(",");

            Face face;
            if (!Enum.TryParse(coordinates[2].ToUpper(), out face))
            {
                return false;
            }
            return true;
        }
    }
    
    class Program
    {
  
        static Turtle turtle;
        static void Main(string[] args)
        {
            Startup();
        }

        static void Startup()
        {
            turtle = new Turtle();
            Console.WriteLine("--INPUT--");
            ProcessCommand(Console.ReadLine());
        }

        static void ProcessCommand(string input)
        {
            var inputs = input.ToUpper().Split(" ");
            string cmd = inputs[0];
            Command command;
            if (Enum.TryParse(cmd.ToUpper(), out command))
            {
                if (command != Command.PLACE)
                {
                    if (!turtle.IsPlaced)
                    {
                        DeletePrevConsoleLine();
                        ProcessCommand(Console.ReadLine());
                    }
                }
                else
                {
                    if (!input.IsValidPlaceCommand())
                    {
                        DeletePrevConsoleLine();
                        ProcessCommand(Console.ReadLine());
                    }
                }

                switch (command)
                {
                    case Command.PLACE: turtle.Place(input); break;
                    case Command.MOVE: turtle.Move(); break;
                    case Command.LEFT: turtle.Left(); break;
                    case Command.RIGHT: turtle.Right(); break;
                    case Command.REPORT:
                        Console.WriteLine("--OUTPUT--");
                        Console.WriteLine(turtle.Report());
                        break;
                }
            }
            else
            {
                DeletePrevConsoleLine();
            }

            ProcessCommand(Console.ReadLine());
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
