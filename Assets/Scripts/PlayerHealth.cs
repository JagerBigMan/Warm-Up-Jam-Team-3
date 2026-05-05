using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image hpBarImage;
    public Sprite fullSprite;
    public Sprite emptySprite;
    public GameOverUI gameOverUI;

    private bool isDead = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (hpBarImage != null && fullSprite != null)
        {
            hpBarImage.sprite = fullSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (hpBarImage != null && emptySprite != null)
        {
            hpBarImage.sprite = emptySprite;
        }

        AutoRotatingTurretShooter shooter = GetComponent<AutoRotatingTurretShooter>();

        if (shooter != null)
        {
            shooter.DisableShooting();
        }

        if (gameOverUI != null)
        {
            gameOverUI.Show();
        }
    }
}