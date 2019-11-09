using UnityEngine;

namespace Maplewing.LeapBound.Engine.Unity
{
    public static class GameObjectExtension
    {
        public static GameObject Instantiate(this GameObject prefab, Transform rootTransform)
        {
            var gameObject = GameObject.Instantiate(prefab, rootTransform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = Vector3.one;
            return gameObject;
        }

        public static T Instantiate<T>(this GameObject prefab, Transform rootTransform)
            => prefab.Instantiate(rootTransform).GetComponent<T>();
    }
}