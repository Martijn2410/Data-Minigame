using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;

    public void SetHealth(int health)
    {
        healthBarImage.fillAmount = (float)health / 100f;
    }
}
