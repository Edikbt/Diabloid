using UnityEngine;
using TMPro;
using Zenject;

namespace Diabloid
{
    public class GoldCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;
        private WorldData _worldData;

        [Inject]
        private void Construct(IPersistentProgressService progressService)
        {
            _worldData = progressService.Progress.WorldData;
            _worldData.GoldData.Changed += UpdateCounter;
        }

        private void Start()
        {
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            _counter.text = $"{_worldData.GoldData.Amount}";
        }
    }
}
