using System.Collections.Generic;
using UnityEngine;

namespace Diabloid
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject Hero { get; private set; }

        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHero()
        {
            Hero = InstantiateRegistered(AssetAddress.HeroPath);

            return Hero;
        }

        public GameObject CreateMonster()
        {
            GameObject monster = InstantiateRegistered(AssetAddress.GoblinPath);

            if (monster.TryGetComponent(out MoveTo moveToPlayer))
                moveToPlayer.SetTaret(Hero.transform);

            return monster;
        }

        public GameObject CreateHud() =>
            InstantiateRegistered(AssetAddress.HUDPath);

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}
