using Maplewing.LeapBound.Engine.Data;
using UnityEngine;

namespace Maplewing.LeapBound.Engine.Unity
{
    public static class Vector2DUnityExtension
    {
        public static Vector2 ToVector2(this Vector2D vector)
            => new Vector2(vector.X, vector.Y);

        public static Vector3 ToVector3(this Vector2D vector)
            => new Vector3(vector.X, vector.Y, 0);
    }
}