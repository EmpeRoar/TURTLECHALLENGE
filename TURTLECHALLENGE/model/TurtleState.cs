using System;
using System.Collections.Generic;
using System.Text;
using TURTLECHALLENGE.enumerations;
using TURTLECHALLENGE.interfaces;

namespace TURTLECHALLENGE.model
{
    public class TurtleState : ITurtleState
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public Face Face { get; set; }
    }
}
