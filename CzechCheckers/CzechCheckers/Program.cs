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
            Player white = new Player { Name = "Human", Color = Color.White };
            Player black = new Player { Name = "Human2", Color = Color.Black };

            new Game(white, black, StandardBoards.Get2v2Board()).Start();
        }
    }
}
