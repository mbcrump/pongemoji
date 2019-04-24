using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongEmoji
{
    //Up and left, down and left, straight and left...
    enum Directions { UnL = 1, DnL, SnL, UnR, DnR, SnR }
    enum Borders { Ceiling = 1, Floor = 15, Left = 0, Right = 41 }
    enum Move { Down = 1, Up = -1 }
}