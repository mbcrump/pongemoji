using System;

namespace PongEmoji
{
    class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Directions Direction { get; set; }
        private Random _rand;
        public Ball()
        {
            _rand = new Random();
            X = 20;
            Y = 7;
            var rand = _rand.Next(1, 6);
            Direction = (Directions)rand;
        }

        public bool Move(Player first, Player second)
        {

            Draw(remove: true);
            switch (Direction)
            {
                case Directions.UnL:
                    X--;
                    Y--;
                    break;
                case Directions.DnL:
                    X--;
                    Y++;
                    break;
                case Directions.SnL:
                    X--;
                    break;
                case Directions.UnR:
                    X++;
                    Y--;
                    break;
                case Directions.DnR:
                    X++;
                    Y++;
                    break;
                case Directions.SnR:
                    X++;
                    break;
            }

            if (Repulse(first) || Repulse(second))
            {
                Move(first, second);
            }
            else if (Y - 1 <= (int)Borders.Ceiling)
            {
                Direction = Direction == Directions.UnL ? Directions.DnL : Directions.DnR;
            }
            else if (Y + 1 >= (int)Borders.Floor)
            {
                Direction = Direction == Directions.DnL ? Directions.UnL : Directions.UnR;
            }
            Draw();
            return Score(first, second);
        }

        public bool Repulse(Player player)
        {
            var second = player.IsSecond ? true : false;
            var collX = player.X == X;
            if (!collX)
                return false;
            var collY = player.Y == Y || player.Y + 1 == Y || player.Y + 2 == Y;
            if (collY)
            {
                int rand;
                if (second)
                    rand = _rand.Next(1, 3);
                else
                    rand = _rand.Next(4, 6);

                Direction = (Directions)rand;
                return true;
            }
            return false;
        }
        public bool Score(Player first, Player second)
        {
            if (X == (int)Borders.Left)
            {
                second.Points++;
                return true;
            }
            else if (X == (int)Borders.Right)
            {
                first.Points++;
                return true;
            }
            else
            {
                return false;
            }
        }
       public void Draw(bool remove = false)
        {
            Console.CursorLeft = X;
            Console.CursorTop = Y;
            if (remove)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("  ");
            }
            else
            {
                Console.ResetColor();
                //Console.Write("X");
                Console.Write("🏀");
            }
        }

        public void Reset()
        {
            Draw(remove: true);
            X = 20;
            Y = 7;
            var rand = _rand.Next(1, 6);
            Direction = (Directions)rand;
            Draw();
        }
    }
}