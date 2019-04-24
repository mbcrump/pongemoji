using System;
using System.Threading;

namespace PongEmoji
{
    class GameTable
    {
        private Player _firstPlayer { get; set; }
        private Player _secondPlayer { get; set; }
        private Ball _ball { get; set; }
        private bool _scored = false;
        public GameTable()
        {
            Console.CursorVisible = false;
            _ball = new Ball();
            _firstPlayer = new Player();
            _secondPlayer = new Player(isSecond: true);
            InitDraw();
        }

        public void Start()
        {
            DrawScore();
            while ((_firstPlayer.Points < 2) && (_secondPlayer.Points < 2))
            {
                while (!Console.KeyAvailable) { }
                _scored = false;
                while (!_scored)
                {
                    if (!Console.KeyAvailable)
                    {
                        _scored = _ball.Move(_firstPlayer, _secondPlayer);
                        Thread.Sleep(100);
                    }
                    else
                    {
                        _scored = _ball.Move(_firstPlayer, _secondPlayer);
                        PlayerMove();
                        Thread.Sleep(100);
                    }
                }
                DrawScore();
                _ball.Reset();
                _firstPlayer.Reset();
                _secondPlayer.Reset();
            }
            var winner = _firstPlayer.Points == 2 ? "First player won!" : "Second player won!";
            ClearEverything();
            WhosTheWinner(winner);
            Restart(winner);
        }

        private void InitDraw()
        {
            Console.ResetColor();
            DrawTable();
            _firstPlayer.Draw();
            _secondPlayer.Draw();
            _ball.Draw();
        }

        public void InitPlayers()
        {
            _firstPlayer = new Player();
            _secondPlayer = new Player(isSecond: true);
        }

        public void DrawTable()
        {
            Console.Clear();
            Console.CursorLeft = 0;
            Console.CursorTop = (int)Borders.Ceiling;
            for (int i = 0; i < 21; i++)
            {
                Console.Write("🌮");
            }

            Console.CursorLeft = 0;
            Console.CursorTop = (int)Borders.Floor;
            for (int i = 0; i < 21; i++)
            {
                Console.Write("🌮");
            }

        }

        public void PlayerMove()
        {
            var key = Console.ReadKey(true).Key.ToString();
            Thread.Sleep(10);

            switch (key)
            {
                case "W":
                    _firstPlayer.Move(Move.Up);
                    break;
                case "S":
                    _firstPlayer.Move(Move.Down);
                    break;
                case "UpArrow":
                    _secondPlayer.Move(Move.Up);
                    break;
                case "DownArrow":
                    _secondPlayer.Move(Move.Down);
                    break;
                case "Q":
                    break;
            }
        }

        public void ClearEverything()
        {
            _ball.Draw(remove: true);
            _firstPlayer.Draw(remove: true);
            _secondPlayer.Draw(remove: true);
        }

        public void DrawScore()
        {
            Console.CursorLeft = 3;
            Console.CursorTop = 0;
            Console.Write("First player 👨 : {0}  |  Second player 👩🏿: {1}", _firstPlayer.Points, _secondPlayer.Points);
        }

        public void WhosTheWinner(string winner)
        {
            Console.ResetColor();
            Console.CursorLeft = 15;
            Console.CursorTop = 7;
            Console.Write(winner);
        }

        public void Restart(string winner)
        {
            Console.CursorLeft = 15;
            Console.CursorTop = 10;
            Console.Clear();
            Environment.Exit(0);
            Console.Write("Restart? (y/n)");

            while (true)
            {
                var key = Console.ReadKey(true).Key.ToString();
                switch (key)
                {
                    case "Y":
                        _firstPlayer.Points = 0;
                        _secondPlayer.Points = 0;
                        InitDraw();
                        Start();
                        break;
                    case "N":
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
