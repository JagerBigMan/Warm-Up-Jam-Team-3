using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " took damage: " + damage + " | health left: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}