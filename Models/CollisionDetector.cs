using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PingPong.Models
{
    internal class CollisionDetector
    {
        public static bool IsCollision(Position p1, double width1, double height1,
            Position p2, double width2, double height2)
        {
            bool col =  (p1.X <= p2.X + width2 &&
                p1.X + width1 > p2.X &&
                p1.Y <= p2.Y + height2 &&
                p1.Y + height1 >= p2.Y);

            if (col)
            {
                Console.WriteLine("Kolizja wykryta!");
            }

            return col;
        }

        public static bool CheckPaddleCollision(Ball ball,Paddle paddle)
        {
            double ballLeft = ball.PositionBall.X;
            double ballRight = ball.PositionBall.X + ball.BallShape.Width;
            double ballTop = ball.PositionBall.Y;
            double ballBottom = ball.PositionBall.Y + ball.BallShape.Height;

            double paddleLeft = Canvas.GetLeft(paddle.PaddleShape);
            double paddleRight = paddleLeft + paddle.PaddleShape.Width;
            double paddleTop = Canvas.GetTop(paddle.PaddleShape);
            double paddleBottom = paddleTop + paddle.PaddleShape.Height;

            bool isIsSideCollision = ballRight >= paddleLeft && ballLeft <= paddleRight &&
                ballTop <= paddleBottom && ballBottom >= paddleTop;

            bool isTopOrBottomCollision = ballBottom >= paddleTop && ballTop <= paddleBottom &&
                ballRight >= paddleLeft && ballLeft<=paddleRight;

            //if(isIsSideCollision) ball.speedX = -ball.speedX;

            //if(isTopOrBottomCollision) ball.speedY = -ball.speedY;

            return isIsSideCollision || isTopOrBottomCollision;
        }
    }
}
