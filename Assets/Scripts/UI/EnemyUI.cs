using UnityEngine;

namespace Diabloid
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;
        private IHealth _health;

        private void Start()
        {
            _health = GetComponent<IHealth>();
            _health.HealthChanged += UpdateHpBar;
        }

        private void OnDestroy() =>
            _health.HealthChanged -= UpdateHpBar;

        private void UpdateHpBar() =>
            _hpBar.SetValue(_health.Current, _health.Max);
    }
}
