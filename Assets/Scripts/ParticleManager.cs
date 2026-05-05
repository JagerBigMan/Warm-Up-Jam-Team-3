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
    public float chargedEffectLifeTime = 2f;
    public float explosionLifeTime = 3f;

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
            firePoint.rotation * Quaternion.Euler(0f, 0f, -90f),
            firePoint
        );

        Destroy(fx, fireEffectLifeTime);
    }

    public void PlayChargedShotEffect(Transform firePoint)
    {
        if (chargedShotEffectPrefab == null || firePoint == null) return;

        GameObject fx = Instantiate(
            chargedShotEffectPrefab,
            firePoint.position,
            firePoint.rotation * Quaternion.Euler(0f, 0f, -90f),
            firePoint
        );

        Destroy(fx, chargedEffectLifeTime);
    }

    public void PlayPlayerExplosion(Vector3 position)
    {
        if (playerExplosionPrefab == null) return;

        GameObject fx = Instantiate(
            playerExplosionPrefab,
            position,
            Quaternion.identity
        );

        Destroy(fx, explosionLifeTime);
    }

    public void PlayEnemyExplosion(Vector3 position)
    {
        if (enemyExplosionPrefab == null) return;

        GameObject fx = Instantiate(
            enemyExplosionPrefab,
            position,
            Quaternion.identity
        );

        Destroy(fx, explosionLifeTime);
    }

    public GameObject PlayBulletTrail(Transform bullet)
    {
        if (bulletTrailPrefab == null || bullet == null) return null;

        GameObject fx = Instantiate(
            bulletTrailPrefab,
            bullet.position,
            bullet.rotation * Quaternion.Euler(0f, 0f, 90f),
            bullet
        );

        return fx;
    }
}