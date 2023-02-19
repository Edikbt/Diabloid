using Zenject;

namespace Diabloid
{
    public class BootstrapInstaller : MonoInstaller
    {
        [Inject] private DiContainer _diContainer;

        public override void InstallBindings()
        {
            DiContainerRef.Container = _diContainer;

            Container.Bind(typeof(IInputService), typeof(ITickable)).To<InputService>().AsSingle();
            Container.Bind(typeof(IStatsDataService), typeof(IInitializable)).To<StatsDataService>().AsSingle();

            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
        }
    }
}