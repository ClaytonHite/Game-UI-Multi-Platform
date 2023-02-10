using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereal.Client.Source.Engine
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Position()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
    }
}
