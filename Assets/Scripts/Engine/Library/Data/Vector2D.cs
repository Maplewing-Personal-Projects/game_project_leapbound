using System;

namespace Maplewing.LeapBound.Engine.Data
{
    public class Vector2D
    {
        public static readonly Vector2D ZERO = new Vector2D(0, 0);

        public float X;
        public float Y;

        public Vector2D(float x = 0, float y = 0)
        {
            X = x;
            Y = y;
        }

        public float Magnitude { get => (float)Math.Sqrt(X * X + Y * Y); }

        public static Vector2D operator +(Vector2D a, Vector2D b)
            => new Vector2D(a.X + b.X, a.Y + b.Y);

        public static Vector2D operator -(Vector2D a, Vector2D b)
            => new Vector2D(a.X - b.X, a.Y - b.Y);

        public static Vector2D operator *(Vector2D a, float b)
            => new Vector2D(a.X * b, a.Y * b);

        public static Vector2D operator /(Vector2D a, float b)
            => new Vector2D(a.X / b, a.Y / b);


        public Vector2D Normalized()
            => this / Magnitude;
    }
}