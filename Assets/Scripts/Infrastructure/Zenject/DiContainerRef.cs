using Zenject;

namespace Diabloid
{
    public static class DiContainerRef
    {
        private static DiContainer container;

        public static DiContainer Container
        {
            get => container;
            set => container ??= value;
        }
    }
}
