using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1f;
    public int scoreValue = 1;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (ParticleManager.Instance != null)
            {
                ParticleManager.Instance.PlayEnemyExplosion(transform.position);
            }

            ScoreManager.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}