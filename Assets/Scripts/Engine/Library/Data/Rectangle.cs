using System;

namespace Maplewing.LeapBound.Engine.Data
{
    public class Rectangle
    {
        public Vector2D Position;
        public Vector2D Size;

        public Vector2D LeftTopPoint { get => new Vector2D(Position.X - Size.X, Position.Y + Size.Y); }
        public Vector2D RightBottomPoint { get => new Vector2D(Position.X + Size.X, Position.Y - Size.Y); }

        public Rectangle(Vector2D position, Vector2D size)
        {
            Position = position;
            Size = size;
        }

        public bool IsPointIn(Vector2D point)
            => Math.Abs(point.X - Position.X) <= Size.X &&
            Math.Abs(point.Y - Position.Y) <= Size.Y;

        public bool IsIntersect(Rectangle rectangle)
            => _CheckIntersectByPoints(
                new Vector2D(Position.X - Size.X / 2, Position.Y + Size.Y / 2),
                new Vector2D(Position.X + Size.X / 2, Position.Y - Size.Y / 2),
                new Vector2D(rectangle.Position.X - rectangle.Size.X / 2, rectangle.Position.Y + rectangle.Size.Y / 2),
                new Vector2D(rectangle.Position.X + rectangle.Size.X / 2, rectangle.Position.Y - rectangle.Size.Y / 2));

        private bool _CheckIntersectByPoints(Vector2D leftTop1, Vector2D rightBottom1,
            Vector2D leftTop2, Vector2D rightBottom2)
            => !(leftTop1.X > rightBottom2.X || leftTop2.X > rightBottom1.X) &&
               !(leftTop1.Y < rightBottom2.Y || leftTop2.Y < rightBottom1.Y);
    }
}