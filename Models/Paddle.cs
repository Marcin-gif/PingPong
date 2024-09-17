using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PingPong.Models
{
    internal class Paddle
    {
        public Rectangle PaddleShape { get; private set; }
        public Position PositionPaddle { get; set; }
        public double Speed { get; set; }
        public double Width => PaddleShape.Width;
        public double Height => PaddleShape.Height;

        public Paddle(Rectangle paddleShape)
        {
            PaddleShape = paddleShape;
            Speed = 2;
            PositionPaddle = new Position(Canvas.GetLeft(PaddleShape), Canvas.GetTop(PaddleShape));
        }

        public void MovePaddle(double dY)
        {
            PositionPaddle.move(0, Speed);

            if(PositionPaddle.Y<0) PositionPaddle.Y = 0;
            if (PositionPaddle.Y + Height > dY) PositionPaddle.Y = dY - Height;

            PositionPaddle.Y = Math.Max(0, Math.Min(dY - Height, PositionPaddle.Y));
            Canvas.SetTop(PaddleShape, PositionPaddle.Y);
        }

        public bool checkCollision(Ball ball)
        {
            return CollisionDetector.IsCollision(this.PositionPaddle, this.Width, this.Height,
                ball.PositionBall, ball.speedX, ball.speedY);
        }
    }
}
