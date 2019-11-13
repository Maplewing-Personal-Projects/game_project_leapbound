using System.Linq;
using Maplewing.LeapBound.Engine;
using Maplewing.LeapBound.Engine.Unity;
using UnityEngine;

namespace Maplewing.LeapBound.Unity.GamePlay
{
    [RequireComponent(typeof(Transform))]
    public class BulletViewController : MonoBehaviour
    {
        public class ViewData
        {
            public string PrefabName;
            public Rect AreaRange;
        }

        [SerializeField] private GameObject _swordBulletPrefab = null;
        private Transform _bulletRoot = null;

        private GameObject[] _swordBulletGameObject = new GameObject[0];

        private void Start()
            => _bulletRoot = GetComponent<Transform>();

        private void OnDestroy()
        {
            foreach (var swordBulletGameObject in _swordBulletGameObject)
                Destroy(swordBulletGameObject);
            _swordBulletGameObject = new GameObject[0];
        }

        public void SetBullets(ViewData[] bullets)
        {
            int diff = bullets.Length - _swordBulletGameObject.Length;
            if (diff > 0)
            {
                _swordBulletGameObject = _swordBulletGameObject.Concat(
                    Enumerable.Range(0, diff).Select(_ => _swordBulletPrefab.Instantiate(_bulletRoot)))
                    .ToArray();
            }
            else if(diff < 0)
            {
                int takeCount = _swordBulletGameObject.Length + diff;
                foreach (var swordBulletGameObject in _swordBulletGameObject.Skip(takeCount))
                    Destroy(swordBulletGameObject);
                _swordBulletGameObject = _swordBulletGameObject.Take(takeCount).ToArray();
            }

            _swordBulletGameObject = _swordBulletGameObject.Zip(bullets,
                (gameObject, item) =>
                {
                    gameObject.transform.localPosition = item.AreaRange.center;
                    gameObject.transform.localScale = item.AreaRange.size;
                    return gameObject;
                }).ToArray();
        }
    }
}