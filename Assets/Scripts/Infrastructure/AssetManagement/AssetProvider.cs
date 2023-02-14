using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class AssetProvider : IAssetProvider
    {
        private DiContainer _diContainer;

        public AssetProvider(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, null);
            //return GameObject.Instantiate(prefab, at, Quaternion.identity, null);
        }

        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return _diContainer.InstantiatePrefab(prefab);
            //return GameObject.Instantiate(prefab);
        }
    }
}
