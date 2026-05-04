using UnityEngine;
using UnityEngine.InputSystem;

public class AutoRotatingBarrel : MonoBehaviour
{
    public GameObject normalBulletPrefab;
    public GameObject chargedBulletPrefab;
    public Transform firePoint;

    public float rotationSpeed = 120f;

    public int maxAmmo = 5;
    public int currentAmmo;
    public float reloadTime = 1f;

    public float maxChargeTime = 1.5f;
    public float chargeThreshold = 0.15f;

    private float chargeTimer;
    private bool isCharging;
    private bool isHolding;

    private float reloadTimer;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        HandleInput();
        HandleReload();

        if (!isCharging)
        {
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
    }

    void HandleInput()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (currentAmmo <= 0) return;

            isHolding = true;
            chargeTimer = 0f;
        }

        if (Mouse.current.leftButton.isPressed && isHolding)
        {
            chargeTimer += Time.deltaTime;

            if (chargeTimer >= chargeThreshold)
            {
                isCharging = true;
            }

            chargeTimer = Mathf.Clamp(chargeTimer, 0f, maxChargeTime);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && isHolding)
        {
            Shoot();

            isHolding = false;
            isCharging = false;
        }
    }

    void Shoot()
    {
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

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetCharge(chargePercent);
        }
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
}