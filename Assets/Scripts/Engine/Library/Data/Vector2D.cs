namespace Maplewing.LeapBound.Engine.Data
{
    public class Vector2D
    {
        public static readonly Vector2D ZERO = new Vector2D(0, 0);

        public float X;
        public float Y;

        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
            => new Vector2D(a.X + b.X, a.Y + b.Y);
    }
}