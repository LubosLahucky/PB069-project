using System;

namespace CzechCheckers
{
    public class GameEndedEventArgs : EventArgs
    {
        public Player Winner { get; set; }
    }
}