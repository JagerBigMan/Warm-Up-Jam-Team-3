using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float damage = 1f;
    public float lifeTime = 3f;

    public void SetCharge(float chargePercent)
    {
        damage = Mathf.Lerp(1f, 5f, chargePercent);
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}