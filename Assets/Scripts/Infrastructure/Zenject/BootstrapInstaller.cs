using Zenject;

namespace Diabloid
{
    public class BootstrapInstaller : MonoInstaller
    {
        [Inject] private DiContainer _diContainer;

        public override void InstallBindings()
        {
            DiContainerRef.Container = _diContainer;

            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
        }
    }
}