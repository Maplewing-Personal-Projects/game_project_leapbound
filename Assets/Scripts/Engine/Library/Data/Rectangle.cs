using System;

namespace Maplewing.LeapBound.Engine.Data
{
    public class Rectangle
    {
        public Vector2D Position;
        public Vector2D Size;

        public Rectangle(Vector2D position, Vector2D size)
        {
            Position = position;
            Size = size;
        }

        public bool IsPointIn(Vector2D point)
            => Math.Abs(point.X - Position.X) <= Size.X &&
            Math.Abs(point.Y - Position.Y) <= Size.Y;
    }
}