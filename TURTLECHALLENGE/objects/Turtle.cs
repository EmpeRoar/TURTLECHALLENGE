using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TURTLECHALLENGE.enumerations;
using TURTLECHALLENGE.interfaces;

namespace TURTLECHALLENGE.objects
{
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
                ypos <= _table.NorthEdge && ypos >= _table.SouthEdge)
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
                    if (_turtleState.XPos < _table.EastEdge)
                        _turtleState.XPos++;
                    break;
                case Face.WEST:
                    if (_turtleState.XPos > _table.WestEdge)
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
        public bool ProcessCommand(string readLine, Action<string> report, Action deleteConsoleLine, Func<string,bool> isValidPlaceCommand)
        {
            var input = readLine;
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
                        return false;
                    }
                }
                else
                {
                    if (!isValidPlaceCommand(input))
                    {
                        deleteConsoleLine();
                        return false;
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
                return false;
            }
            return true;
        }

    }
}
