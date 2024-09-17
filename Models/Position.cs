using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Models
{
    internal class Position
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void move(double x, double y)
        {
            X += x;
            Y += y;
        }

        public double distance(Position p) 
        { 
            double dX = p.X - this.X;
            double dY = p.Y - this.Y;

            return Math.Sqrt((dX * dX) + (dY * dY));
        }
    }
}
