using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class AssetProvider : IAssetProvider
    {
        private DiContainer _diContainer;
        private Transform _objectsParent;

        public AssetProvider(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            if (_objectsParent == null)
                InstantiateParent();

            GameObject prefab = Resources.Load<GameObject>(path);
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, _objectsParent);
        }

        public GameObject Instantiate(string path)
        {
            if (_objectsParent == null)
                InstantiateParent();

            GameObject prefab = Resources.Load<GameObject>(path);
            return _diContainer.InstantiatePrefab(prefab, Vector3.zero, Quaternion.identity, _objectsParent);
        }

        public GameObject Instantiate(EnemyStatsData enemyStatsData, Transform parent)
        {
            return _diContainer.InstantiatePrefab(enemyStatsData.Prefab, parent.position, Quaternion.identity, parent);
        }

        private void InstantiateParent() =>
            _objectsParent = GameObject.Instantiate(Resources.Load<GameObject>("ObjectsParent")).transform;
    }
}
