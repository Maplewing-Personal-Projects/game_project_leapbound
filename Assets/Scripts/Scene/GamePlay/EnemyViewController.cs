using System.Linq;
using Maplewing.LeapBound.Engine;
using Maplewing.LeapBound.Engine.Unity;
using UnityEngine;

namespace Maplewing.LeapBound.Unity.GamePlay
{
    [RequireComponent(typeof(Transform))]
    public class EnemyViewController : MonoBehaviour
    {
        public class ViewData
        {
            public string PrefabName;
            public Rect AreaRange;
        }

        [SerializeField] private GameObject _monsterPrefab = null;
        private Transform _enemyRoot = null;

        private GameObject[] _monsterGameObjects = new GameObject[0];

        private void Start()
            => _enemyRoot = GetComponent<Transform>();

        private void OnDestroy()
        {
            foreach (var coinGameObject in _monsterGameObjects)
                Destroy(coinGameObject);
            _monsterGameObjects = new GameObject[0];
        }

        public void SetEnemies(ViewData[] enemies)
        {
            int diff = enemies.Length - _monsterGameObjects.Length;
            if (diff > 0)
            {
                _monsterGameObjects = _monsterGameObjects.Concat(
                    Enumerable.Range(0, diff).Select(_ => _monsterPrefab.Instantiate(_enemyRoot)))
                    .ToArray();
            }
            else if(diff < 0)
            {
                int takeCount = _monsterGameObjects.Length + diff;
                foreach (var coinGameObject in _monsterGameObjects.Skip(takeCount))
                    Destroy(coinGameObject);
                _monsterGameObjects = _monsterGameObjects.Take(takeCount).ToArray();
            }

            _monsterGameObjects = _monsterGameObjects.Zip(enemies,
                (gameObject, item) =>
                {
                    gameObject.transform.localPosition = item.AreaRange.center;
                    gameObject.transform.localScale = item.AreaRange.size;
                    return gameObject;
                }).ToArray();
        }
    }
}