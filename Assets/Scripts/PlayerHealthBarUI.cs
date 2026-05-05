using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    public Image healthFillImage;

    private float maxHealth;

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
        SetHealth(value);
    }

    public void SetHealth(float value)
    {
        if (healthFillImage == null) return;

        healthFillImage.fillAmount = value / maxHealth;
    }
}