using System;

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
        void Report();
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
      
        private const int TOP = 5;
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

        public void Report()
        {
            Console.WriteLine("--OUTPUT--");
            Console.WriteLine($"{_turtleState.XPos} {_turtleState.YPos} {_turtleState.Face}");
        }
    }

    class Program
    {
  
        static Turtle turtle;
        static void Main(string[] args)
        {
            turtle = new Turtle();
            Console.WriteLine("--INPUT--");
            ProcessCommand(Console.ReadLine());

        }

        static void ProcessCommand(string input)
        {
            input = input.ToUpper();
            string cmd = input.Split(" ")[0];
            Command command;
            if (Enum.TryParse(cmd.ToUpper(), out command))
            {
                if (command != Command.PLACE)
                {
                    if (!turtle.IsPlaced)
                    {
                        ProcessCommand(Console.ReadLine());
                    }
                }

                switch (command)
                {
                    case Command.PLACE: turtle.Place(input); break;
                    case Command.MOVE: turtle.Move(); break;
                    case Command.LEFT: turtle.Left(); break;
                    case Command.RIGHT: turtle.Right(); break;
                    case Command.REPORT: turtle.Report(); break;
                }
            }

            ProcessCommand(Console.ReadLine());
        }

    }
}
