using Maplewing.LeapBound.Engine.Data;
using UnityEngine;

namespace Maplewing.LeapBound.Engine.Unity
{
    public static class RectangleUnityExtension
    {
        public static Rect ToRect(this Rectangle rectangle)
            => new Rect((rectangle.Position - rectangle.Size / 2).ToVector2(), rectangle.Size.ToVector2());
    }
}