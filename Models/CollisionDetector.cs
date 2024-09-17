using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Models
{
    internal class CollisionDetector
    {
        public static bool IsCollision(Position p1, double width1, double height1,
            Position p2, double width2, double height2)
        {
            bool col =  (p1.X < p2.X + width2 &&
                p1.X + width1 > p2.X &&
                p1.Y < p2.Y + height2 &&
                p1.Y + height1 > p2.Y);

            if (col)
            {
                Console.WriteLine("Kolizja wykryta!");
            }

            return col;
        }
    }
}
