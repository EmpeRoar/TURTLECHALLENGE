using System;
using System.Collections.Generic;
using System.Text;
using TURTLECHALLENGE.enumerations;

namespace TURTLECHALLENGE.interfaces
{
    public interface ITurtleState
    {
        int XPos { get; set; }
        int YPos { get; set; }
        Face Face { get; set; }
    }
}
