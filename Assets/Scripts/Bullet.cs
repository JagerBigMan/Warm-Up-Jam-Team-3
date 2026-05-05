using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float damage = 1f;
    public float lifeTime = 3f;

    private GameObject trail;

    void Start()
    {
        if (ParticleManager.Instance != null)
        {
            trail = ParticleManager.Instance.PlayBulletTrail(transform);
        }

        Destroy(gameObject, lifeTime);
    }

    public void SetCharge(float chargePercent)
    {
        damage = Mathf.Lerp(1f, 5f, chargePercent);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        Vector3 vp = Camera.main.WorldToViewportPoint(transform.position);

        if (vp.x < 0f || vp.x > 1f || vp.y < 0f || vp.y > 1f)
        {
            Destroy(gameObject);
        }
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