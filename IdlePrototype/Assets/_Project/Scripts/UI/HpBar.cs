using UnityEngine;
using UnityEngine.UI;

namespace IdleArcade.UI
{
    public class HpBar : MonoBehaviour
    {
        public Image ImageCurrent;

        public void SetValue(float current, float max)
        {
            ImageCurrent.fillAmount = current / max;
        }
    }
}
