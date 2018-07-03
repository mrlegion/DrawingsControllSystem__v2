namespace DrawingsControllSystem.Model.Common
{
    public class Marker
    {
        public int X { get; }
        public int Y { get; }
        public MarkerPosition Position { get; }

        public Marker()
        {
        }

        public Marker(int x, int y, MarkerPosition position)
        {
            X = x;
            Y = y;
            Position = position;
        }
    }
}