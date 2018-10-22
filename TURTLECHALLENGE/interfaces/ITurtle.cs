using System;

namespace TURTLECHALLENGE.interfaces
{
    public interface ITurtle
    {
        bool ProcessCommand(string readLine, Action<string> report, Action deleteConsoleLine, Func<string,bool> isValidPlaceCommand);
        bool Place(string input);
        void Move();
        void Left();
        void Right();
        string Report();
    }
}
