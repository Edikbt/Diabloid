using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class Test : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;

        private void Start()
        {
            _diContainer.InstantiatePrefab(Resources.Load("Goblin"));
        }
    }
}
