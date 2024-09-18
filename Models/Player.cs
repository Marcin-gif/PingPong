using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PingPong.Models
{
    internal class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public Paddle PaddlePlayer { get; private set; }
        public Key UpKey { get; set; }
        public Key DownKey { get; set; }


        public Player(string name, Paddle paddle,Key upKey, Key downKey) 
        { 
            Name = name;
            Score = 0;
            PaddlePlayer = paddle;
            UpKey = upKey;
            DownKey = downKey;
        }

        public void IncreaseScore()
        {
            Score += 1;
        }

        public void UpdatePaddlePosition(double canvasHeight, Key key)
        {
            if(key == UpKey)
            {
                PaddlePlayer.Speed = -20;
            }
            else if(key==DownKey)
            {
                PaddlePlayer.Speed = 20;
            }else
            {
                PaddlePlayer.Speed = 0;
            }

            PaddlePlayer.MovePaddle(canvasHeight);
        }

    }
}
