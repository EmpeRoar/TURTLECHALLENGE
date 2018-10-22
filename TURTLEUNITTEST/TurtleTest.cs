using FluentAssertions;
using System;
using System.Collections.Generic;
using TURTLECHALLENGE.model;
using Xunit;

namespace TURTLEUNITTEST
{
    public class TurtleTest
    {
        public TurtleTest()
        {

        }

        [Fact]
        public void Invalid_First_Command_Return_False()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string input = "";
            Action<string> message = (msg) => { };
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var result = turtle.ProcessCommand(input, message, clearCommandLine, isValidPlaceCommand);

            result.Should().Be(false);
        }

        [Fact]
        public void Valid_First_Command_Return_True()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string input = "PLACE 0,0,NORTH";
            Action<string> message = (msg) => { };
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var result = turtle.ProcessCommand(input, message, clearCommandLine, isValidPlaceCommand);

            result.Should().Be(true);
        }

        

        [Fact]
        public void SequenceOfCommands_All_Valid()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string reportMessage = "";

            Action<string> report = (msg) => reportMessage = msg;
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var commandSequence = new List<string>()
            {
                "PLACE 0,0,NORTH",
                "MOVE",
                "MOVE",
                "MOVE",
                "RIGHT",
                "MOVE",
                "LEFT",
                "MOVE",
                "LEFT",
                "REPORT"
            };

            foreach (var command in commandSequence)
                turtle.ProcessCommand(command, report, clearCommandLine, isValidPlaceCommand);

            reportMessage.Should().Be("1 4 WEST");
        }

        [Fact]
        public void SequenceOfCommands_Some_AreInValid()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string reportMessage = "";

            Action<string> report = (msg) => reportMessage = msg;
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var commandSequence = new List<string>()
            {
                "Julius",
                "PLACE 0,0,NORTH",
                "MOVE",
                "Bacosa",
                "MOVE",
                "MOVE",
                "Faith",
                "RIGHT",
                "MOVE",
                "LEFT",
                "MOVE",
                "LEFT",
                "REPORT",
                "Barte"
            };

            foreach (var command in commandSequence)
                turtle.ProcessCommand(command, report, clearCommandLine, isValidPlaceCommand);

            reportMessage.Should().Be("1 4 WEST");
        }

        [Fact]
        public void SequenceOfCommands_All_AreInValid()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string reportMessage = "";

            Action<string> report = (msg) => reportMessage = msg;
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var commandSequence = new List<string>()
            {
                "MOVE",
                "Julius",
                "BACOSA",
                "REPORT"
            };

            foreach (var command in commandSequence)
                turtle.ProcessCommand(command, report, clearCommandLine, isValidPlaceCommand);

            reportMessage.Should().Be("");
        }

        [Fact]
        public void SequenceOfCommands_InValid_Place_Coordinates()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string reportMessage = "";

            Action<string> report = (msg) => reportMessage = msg;
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var commandSequence = new List<string>()
            {
                "PLACE -0,7,EAST",
                "MOVE",
                "REPORT"
            };

            foreach (var command in commandSequence)
                turtle.ProcessCommand(command, report, clearCommandLine, isValidPlaceCommand);

            reportMessage.Should().Be("");
        }

        [Fact]
        public void SequenceOfCommands_Valid_Place_Coordinates_MoveOutSideTable_EAST()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string reportMessage = "";

            Action<string> report = (msg) => reportMessage = msg;
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var commandSequence = new List<string>()
            {
                "PLACE 0,0,EAST",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "REPORT"
            };

            foreach (var command in commandSequence)
                turtle.ProcessCommand(command, report, clearCommandLine, isValidPlaceCommand);

            reportMessage.Should().Be("4 0 EAST");
        }

        [Fact]
        public void SequenceOfCommands_Valid_Place_Coordinates_MoveOutSideTable_WEST()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string reportMessage = "";

            Action<string> report = (msg) => reportMessage = msg;
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var commandSequence = new List<string>()
            {
                "PLACE 0,0,WEST",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "REPORT"
            };

            foreach (var command in commandSequence)
                turtle.ProcessCommand(command, report, clearCommandLine, isValidPlaceCommand);

            reportMessage.Should().Be("0 0 WEST");
        }

        [Fact]
        public void SequenceOfCommands_Valid_Place_Coordinates_MoveOutSideTable_NORTH()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string reportMessage = "";

            Action<string> report = (msg) => reportMessage = msg;
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var commandSequence = new List<string>()
            {
                "PLACE 0,0,NORTH",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "REPORT"
            };

            foreach (var command in commandSequence)
                turtle.ProcessCommand(command, report, clearCommandLine, isValidPlaceCommand);

            reportMessage.Should().Be("0 4 NORTH");
        }

        [Fact]
        public void SequenceOfCommands_Valid_Place_Coordinates_MoveOutSideTable_SOUTH()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string reportMessage = "";

            Action<string> report = (msg) => reportMessage = msg;
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var commandSequence = new List<string>()
            {
                "PLACE 0,0,SOUTH",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "REPORT"
            };

            foreach (var command in commandSequence)
                turtle.ProcessCommand(command, report, clearCommandLine, isValidPlaceCommand);

            reportMessage.Should().Be("0 0 SOUTH");
        }

        [Fact]
        public void SequenceOfCommandsBeforeValidPlaceCommand()
        {
            var table = new Table();
            var turtleState = new TurtleState();
            var turtle = new Turtle(turtleState, table);

            string reportMessage = "";

            Action<string> report = (msg) => reportMessage = msg;
            Action clearCommandLine = () => { };
            Func<string, bool> isValidPlaceCommand = (cmd) => { return true; };

            var commandSequence = new List<string>()
            {
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "MOVE",
                "PLACE 3,1,NORTH",
                "MOVE",
                "MOVE",
                "REPORT"
            };

            foreach (var command in commandSequence)
                turtle.ProcessCommand(command, report, clearCommandLine, isValidPlaceCommand);

            reportMessage.Should().Be("3 3 NORTH");
        }
    }
}
