using UnityEngine;

namespace Diabloid
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        private void Awake()
        {
            GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null) 
                return;

            Instantiate(BootstrapperPrefab);
        }
    }
}
