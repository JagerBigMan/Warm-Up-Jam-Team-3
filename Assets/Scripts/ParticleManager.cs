using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    public GameObject fireEffectPrefab;
    public GameObject chargedShotEffectPrefab;
    public GameObject playerExplosionPrefab;
    public GameObject enemyExplosionPrefab;
    public GameObject bulletTrailPrefab;

    public float fireEffectLifeTime = 2f;
    public float explosionLifeTime = 3f;
    public float bulletTrailBackOffset = 0.5f;

    void Awake()
    {
        Instance = this;
    }

    public void PlayFireEffect(Transform firePoint)
    {
        if (fireEffectPrefab == null || firePoint == null) return;

        GameObject fx = Instantiate(
            fireEffectPrefab,
            firePoint.position,
            firePoint.rotation * Quaternion.Euler(0f, 90f, 0f),
            firePoint
        );

        Destroy(fx, fireEffectLifeTime);
    }

    public void PlayChargedFireEffect(Transform firePoint)
    {
        if (fireEffectPrefab == null || firePoint == null) return;

        GameObject fx = Instantiate(
            fireEffectPrefab,
            firePoint.position,
            firePoint.rotation * Quaternion.Euler(0f, 90f, 0f),
            firePoint
        );

        fx.transform.localScale *= 1.5f;

        Destroy(fx, fireEffectLifeTime);
    }

    public GameObject StartChargedEffect(Transform firePoint)
    {
        if (chargedShotEffectPrefab == null || firePoint == null) return null;

        GameObject fx = Instantiate(
            chargedShotEffectPrefab,
            firePoint.position,
            firePoint.rotation * Quaternion.Euler(0f, 90f, 0f),
            firePoint
        );

        return fx;
    }

    public void StopEffect(GameObject fx)
    {
        if (fx != null)
        {
            Destroy(fx);
        }
    }

    public void PlayPlayerExplosion(Vector3 position)
    {
        if (playerExplosionPrefab == null) return;

        GameObject fx = Instantiate(playerExplosionPrefab, position, Quaternion.identity);
        Destroy(fx, explosionLifeTime);
    }

    public void PlayEnemyExplosion(Vector3 position)
    {
        if (enemyExplosionPrefab == null) return;

        GameObject fx = Instantiate(enemyExplosionPrefab, position, Quaternion.identity);
        Destroy(fx, explosionLifeTime);
    }

    public GameObject PlayBulletTrail(Transform bullet)
    {
        if (bulletTrailPrefab == null || bullet == null) return null;

        GameObject fx = Instantiate(
            bulletTrailPrefab,
            bullet.position,
            bullet.rotation * Quaternion.Euler(0f, -90f, 0f),
            bullet
        );

        fx.transform.localPosition = Vector3.left * bulletTrailBackOffset;

        return fx;
    }
}