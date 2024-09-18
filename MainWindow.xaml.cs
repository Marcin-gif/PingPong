using PingPong.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PingPong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Player player1;
        private Player player2;
        private Ball ball;
        private DispatcherTimer timer;
        private TimeSpan gameTime;
        private DispatcherTimer countDownTimer;

        public MainWindow()
        {
            InitializeComponent();

            Paddle paddle1 = new Paddle(Paddle1);
            Paddle paddle2 = new Paddle(Paddle2);
            
            player1 = new Player("Gracz1",paddle1,Key.W,Key.S);
            player2 = new Player("Gracz2",paddle2,Key.Up,Key.Down);

            ball = new Ball(Ball);


            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            timer.Tick += GamerTimer_Tick;
            timer.Start();

            gameTime = TimeSpan.FromMinutes(3);

            countDownTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            countDownTimer.Tick += CountDownTimer_Tick;
            countDownTimer.Start();

            this.KeyDown += MainWindow_KeyDown;
            this.KeyUp += MainWindow_KeyUp;
        }
        private void CountDownTimer_Tick(object sender, EventArgs e)
        {
            gameTime = gameTime.Subtract(TimeSpan.FromSeconds(1));

            Score.Text = $"Time left: {gameTime.Minutes:D2}:{gameTime.Seconds:D2}";
            if(gameTime.TotalSeconds <= 0)
            {
                EndGame();
            }
            else if(player1.Score == 5 || player2.Score == 5)
            {
                EndGame();
            }
        }
        private void GamerTimer_Tick(object sender, EventArgs e)
        {
            double canvasWidth = GameCanvas.ActualWidth;
            double canvasHeight = GameCanvas.ActualHeight;

            ball.UpdatePosition(canvasWidth, canvasHeight, player1, player2);

            // Zaktualizuj położenie paletek i piłki
            /*Canvas.SetLeft(Paddle1, player1.PaddlePlayer.PositionPaddle.X);
            Canvas.SetTop(Paddle1, player1.PaddlePlayer.PositionPaddle.Y);
            Canvas.SetLeft(Paddle2, player2.PaddlePlayer.PositionPaddle.X);
            Canvas.SetTop(Paddle2, player2.PaddlePlayer.PositionPaddle.Y);
            Canvas.SetLeft(Ball, ball.PositionBall.X);
            Canvas.SetTop(Ball, ball.PositionBall.Y);*/

            // Opcjonalnie zaktualizuj wynik
            Player1.Content = player1.Score;
            Player2.Content = player2.Score;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);

            player1.UpdatePaddlePosition(GameCanvas.ActualHeight, e.Key);
            player2.UpdatePaddlePosition(GameCanvas.ActualHeight, e.Key);
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if(e.Key == player1.UpKey || e.Key == player1.DownKey)
            {
                player1.UpdatePaddlePosition(GameCanvas.ActualHeight, Key.None);
            }

            if(e.Key == player2.UpKey || e.Key == player2.DownKey)
            {
                player2.UpdatePaddlePosition(GameCanvas.ActualHeight, Key.None);
            }
        }

        private void EndGame()
        {
            timer.Stop();
            countDownTimer.Stop();

            MessageBox.Show($"Koniec gry! Wynik:\n{player1.Name}: {player1.Score} - {player2.Name}: {player2.Score}");
        }
    }
}