using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechCheckers
{
    class Program
    {
        static void Main(string[] args)
        {
            var white = new Player { Name = "Human", Color = Color.White };
            var black = new ComputerPlayer { Name = "Computer", Color = Color.Black };

            new Game(white, black, StandardBoards.Get2v2Board()).Start();
        }
    }
}
