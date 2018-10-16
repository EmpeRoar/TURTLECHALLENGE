using System;
using System.Collections.Generic;
using System.Text;
using TURTLECHALLENGE.interfaces;

namespace TURTLECHALLENGE.objects
{
    public class Table : ITable
    {
        public int NorthEdge { get; set; } = 4;
        public int SouthEdge { get; set; } = 0;
        public int EastEdge { get; set; } = 4;
        public int WestEdge { get; set; } = 0;
    }
}
