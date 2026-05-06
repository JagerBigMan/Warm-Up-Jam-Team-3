using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TurretManager : MonoBehaviour
{
    public GameObject normalBulletPrefab;
    public GameObject chargedBulletPrefab;
    public Transform firePoint;
    public Transform barrelPivot;

    public int maxAmmo = 5;
    public int currentAmmo;
    public float reloadTime = 1f;

    public float maxChargeTime = 1.5f;
    public float chargeThreshold = 0.15f;

    public bool canShoot = true;

    private float chargeTimer;
    private bool isCharging;
    private bool isHolding;
    private float reloadTimer;

    private GameObject activeChargedEffect;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (!canShoot)
        {
            ResetCharge();
            return;
        }

        HandleInput();
        HandleReload();
    }

    void HandleInput()
    {
        if (Mouse.current == null) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (currentAmmo <= 0) return;

            isHolding = true;
            chargeTimer = 0f;
        }

        if (Mouse.current.leftButton.isPressed && isHolding)
        {
            chargeTimer += Time.deltaTime;

            if (chargeTimer >= chargeThreshold && !isCharging)
            {
                isCharging = true;
                StartChargedEffect();
            }

            chargeTimer = Mathf.Clamp(chargeTimer, 0f, maxChargeTime);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && isHolding)
        {
            Shoot();

            isHolding = false;
            isCharging = false;
            chargeTimer = 0f;
            StopChargedEffect();
        }
    }

    void Shoot()
    {
        if (!canShoot) return;

        currentAmmo--;

        float chargePercent = chargeTimer / maxChargeTime;

        GameObject prefab = chargePercent < chargeThreshold
            ? normalBulletPrefab
            : chargedBulletPrefab;

        GameObject bullet = Instantiate(
            prefab,
            firePoint.position,
            firePoint.rotation
        );

        bullet.transform.localScale = normalBulletPrefab.transform.localScale;

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetCharge(chargePercent);
        }

        if (ParticleManager.Instance != null)
        {
            ParticleManager.Instance.PlayFireEffect(firePoint);
        }
    }

    void StartChargedEffect()
    {
        if (activeChargedEffect != null) return;

        if (ParticleManager.Instance != null)
        {
            activeChargedEffect = ParticleManager.Instance.StartChargedEffect(firePoint);
        }
    }

    void StopChargedEffect()
    {
        if (ParticleManager.Instance != null)
        {
            ParticleManager.Instance.StopEffect(activeChargedEffect);
        }

        activeChargedEffect = null;
    }

    void HandleReload()
    {
        if (currentAmmo >= maxAmmo) return;

        reloadTimer += Time.deltaTime;

        if (reloadTimer >= reloadTime)
        {
            currentAmmo++;
            reloadTimer = 0f;
        }
    }

    public bool IsChargingShot()
    {
        return isCharging;
    }

    public void DisableShooting()
    {
        canShoot = false;
        ResetCharge();
    }

    void ResetCharge()
    {
        isHolding = false;
        isCharging = false;
        chargeTimer = 0f;
        StopChargedEffect();
    }
}