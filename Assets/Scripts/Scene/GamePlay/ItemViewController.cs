using System.Linq;
using Maplewing.LeapBound.Engine;
using Maplewing.LeapBound.Engine.Unity;
using UnityEngine;

namespace Maplewing.LeapBound.Unity.GamePlay
{
    [RequireComponent(typeof(Transform))]
    public class ItemViewController : MonoBehaviour
    {
        public class ViewData
        {
            public string PrefabName;
            public Rect AreaRange;
        }

        [SerializeField] private GameObject _coinPrefab = null;
        private Transform _itemRoot = null;

        private GameObject[] _coinGameObjects = new GameObject[0];

        private void Start()
            => _itemRoot = GetComponent<Transform>();

        private void OnDestroy()
        {
            foreach (var coinGameObject in _coinGameObjects)
                Destroy(coinGameObject);
            _coinGameObjects = new GameObject[0];
        }

        public void SetItems(ViewData[] items)
        {
            int diff = items.Length - _coinGameObjects.Length;
            if (diff > 0)
            {
                _coinGameObjects = _coinGameObjects.Concat(
                    Enumerable.Range(0, diff).Select(_ => _coinPrefab.Instantiate(_itemRoot)))
                    .ToArray();
            }
            else if(diff < 0)
            {
                int takeCount = _coinGameObjects.Length + diff;
                foreach (var coinGameObject in _coinGameObjects.Skip(takeCount))
                    Destroy(coinGameObject);
                _coinGameObjects = _coinGameObjects.Take(takeCount).ToArray();
            }

            _coinGameObjects = _coinGameObjects.Zip(items,
                (gameObject, item) =>
                {
                    gameObject.transform.localPosition = item.AreaRange.center;
                    gameObject.transform.localScale = item.AreaRange.size;
                    return gameObject;
                }).ToArray();
        }
    }
}