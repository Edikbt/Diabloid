using System.Collections;
using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class LootGold : MonoBehaviour
    {
        private Gold _gold;
        private bool _isPicked;
        private WorldData _worldData;

        [Inject]
        private void Construct(IPersistentProgressService progressService)
        {
            _worldData = progressService.Progress.WorldData;
        }

        public void Initialize(Gold gold)
        {
            _gold = gold;
        }

        private void OnTriggerEnter(Collider other) => 
            Pickup();

        private void Pickup()
        {
            if (_isPicked == true)
                return;

            _isPicked = true;
            _worldData.GoldData.Collect(_gold.Value);

            StartCoroutine(StartDestroyTimer());
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}
