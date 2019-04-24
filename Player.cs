using System;

namespace PongEmoji
{
    class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsSecond { get; set; }
        public int Points { get; set; }
        public Player(bool isSecond = false)
        {
            X = isSecond ? 40 : 1;
            Y = 5;
            IsSecond = isSecond;
            Points = 0;
        }

        public void Move(Move move)
        {
            var newY = Y + (int)move;
            if (CanMove(newY))
            {
                Draw(remove: true);

                Y = newY;

                Draw();
            }

        }
        public bool CanMove(int newY)
        {
            if (newY + 2 == (int)Borders.Floor || newY == (int)Borders.Ceiling)
                return false;
            else
                return true;
        }
        public void Draw(bool remove = false)
        {
            if (remove)
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ResetColor();
            }
            for (int i = Y; i < Y + 4; i++)
            {
                Console.CursorLeft = X;
                Console.CursorTop = i;
                Console.Write("|");
            }
        }

        public void Reset()
        {
            Draw(remove: true);
            Y = 5;
            Draw();
        }
    }
}