using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        private HeroHealth _heroHealth;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {            
            _heroHealth = gameFactory.Hero.GetComponent<HeroHealth>();
            _heroHealth.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy() => 
            _heroHealth.HealthChanged -= UpdateHpBar;

        private void UpdateHpBar() => 
            _hpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
    }
}
