using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private HeroHealth _heroHealth;

        [Inject]
        public void Construct(/*HeroHealth heroHealth*/IGameFactory gameFactory)
        {
            //_heroHealth = heroHealth.GetComponent<HeroHealth>();
            _heroHealth = gameFactory.Hero.GetComponent<HeroHealth>();
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy()
        {
            _heroHealth.HealthChanged -= UpdateHpBar;
        }

        private void UpdateHpBar()
        {
            Debug.Log($"UpdateHpBar");

            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
        }
    }
}
