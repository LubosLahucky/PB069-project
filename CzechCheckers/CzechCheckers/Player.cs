namespace CzechCheckers
{
    public class Player
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is Player player) && Color == player.Color;
        }

        public override int GetHashCode()
        {
            return -1200350280 + Color.GetHashCode();
        }
    }
}
