using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int health = 10;
    public int maxHealth = 10;
    public float invincibleTime = 0.5f;

    [Header("UI")]
    public SegmentedHealthBarUI healthBar;
    public GameOverUI gameOverUI;
    public TurretSpriteManager turretSpriteManager;

    [Header("Turret Visuals")]
    public TurretVisualManager turretVisualManager;

    private bool isDead = false;
    private bool isInvincible = false;

    void Start()
    {
        Time.timeScale = 1f;

        health = maxHealth;

        if (healthBar != null)
        {
            healthBar.CreateBlocks(maxHealth);
            healthBar.UpdateHealth(health);
        }

        if (turretSpriteManager != null)
        {
            turretSpriteManager.SetAlive();
        }

        if (turretVisualManager != null)
        {
            turretVisualManager.SetAlive();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead || isInvincible) return;

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                TakeDamage(enemy.damage);
                enemy.TakeDamage(999f);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvincible) return;

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.UpdateHealth(health);
        }

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibleRoutine());
        }
    }

    IEnumerator InvincibleRoutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    void Die()
    {
        isDead = true;

        if (turretSpriteManager != null)
        {
            turretSpriteManager.SetDead();
        }

        if (turretVisualManager != null)
        {
            turretVisualManager.SetDead();
        }

        EnemySpawner spawner = FindAnyObjectByType<EnemySpawner>();
       
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        StopPlayerShooting();
        StopAllEnemies();

        if (ParticleManager.Instance != null)
        {
            ParticleManager.Instance.PlayPlayerExplosion(transform.position);
        }

        if (gameOverUI != null)
        {
            gameOverUI.Show();
        }
    }

    void StopPlayerShooting()
    {
        TurretManager shooter = GetComponent<TurretManager>();

        if (shooter != null)
        {
            shooter.DisableShooting();
            shooter.enabled = false;
        }
    }

    void StopAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                if (script != null && script.GetType() != typeof(Enemy))
                {
                    script.enabled = false;
                }
            }

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
}