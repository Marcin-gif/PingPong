using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PingPong.Models
{
    internal class Ball
    {
        public Ellipse BallShape { get; private set; }
        public Position PositionBall { get; private set; }
        public double speedX { get; set; }
        public double speedY { get; set; }
        private Random random = new Random();

        public Ball(Ellipse ballShape)
        {
            BallShape = ballShape;
            this.speedX = 5;
            this.speedY = 5;
            PositionBall = new Position(Canvas.GetLeft(BallShape)
                ,Canvas.GetTop(BallShape));
        }

        public void UpdatePosition(double canvasWidth, double canvasHeight, Player p1, Player p2)
        {
            PositionBall.move(speedX, speedY);

            if(PositionBall.Y <= 0 || PositionBall.Y + BallShape.Height >= canvasHeight)
            {
                speedY = -speedY;
            }

            if(CollisionDetector.IsCollision(PositionBall, BallShape.Width, BallShape.Height, p1.PaddlePlayer.PositionPaddle, p1.PaddlePlayer.Width, p1.PaddlePlayer.Height) ||
               CollisionDetector.IsCollision(PositionBall, BallShape.Width, BallShape.Height, p2.PaddlePlayer.PositionPaddle, p2.PaddlePlayer.Width, p2.PaddlePlayer.Height))
            
            {
                speedX = -speedX;
            }

            if(PositionBall.X <= 0 || PositionBall.X + BallShape.Width >=canvasWidth)
            {
                if(PositionBall.X <= 0)
                {
                    p2.IncreaseScore();
                    p2.PaddlePlayer.Speed += 1;
                }else if (PositionBall.X+BallShape.Width >= canvasWidth)
                {
                    p1.IncreaseScore();
                    p1.PaddlePlayer.Speed += 1;
                }
                IncreaseSpeed();
                ResetBallPosition(canvasWidth / 2, canvasHeight / 2);

            }
            Canvas.SetLeft(BallShape, PositionBall.X);
            Canvas.SetTop(BallShape, PositionBall.Y);
        }

        private double minSpeed = 5;
        private double maxSpeed = 10;
        private double speedIncrement = 0.3;

        public void ResetBallPosition(double startX, double startY)
        {
            PositionBall = new Position(startX, startY);

            //Losowanie Kierunku
            double dirX = random.NextDouble() * 2 - 1;
            double dirY = random.NextDouble() * 2 - 1;

            // Normalizowanie kierunku
            double magnitude = Math.Sqrt(dirX * dirX + dirY * dirY);
            dirX /= magnitude;
            dirY /= magnitude;

            
        }

        public void IncreaseSpeed()
        {
            speedX += speedX > 0 ? speedIncrement : -speedIncrement;
            speedY += speedY > 0 ? speedIncrement : -speedIncrement;

            double currentSpeed = Math.Sqrt(speedX * speedX + speedY * speedY);
            if(currentSpeed>maxSpeed)
            {
                speedX = speedX / currentSpeed * maxSpeed;
                speedY = speedY / currentSpeed * maxSpeed;
            }
        }
    }
}
