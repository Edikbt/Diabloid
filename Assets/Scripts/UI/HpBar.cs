using UnityEngine;
using UnityEngine.UI;

namespace Diabloid
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image ImageCurrent;

        public void SetValue(float current, float max) => 
            ImageCurrent.fillAmount = current / max;
    }
}