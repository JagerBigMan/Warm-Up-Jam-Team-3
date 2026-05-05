using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1f;
    public int scoreValue = 1;

    public int damage = 2;

    private bool isDead = false;

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (ParticleManager.Instance != null)
        {
            ParticleManager.Instance.PlayEnemyExplosion(transform.position);
        }

        ScoreManager.AddScore(scoreValue);

        Destroy(gameObject);
    }
}