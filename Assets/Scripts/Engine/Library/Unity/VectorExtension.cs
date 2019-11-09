using Maplewing.LeapBound.Engine.Data;
using UnityEngine;

namespace Maplewing.LeapBound.Engine.Unity
{
    public static class VectorExtension
    {
        public static Vector3 WithZ(this Vector2 vector, float z)
            => new Vector3(vector.x, vector.y, z);
    }
}