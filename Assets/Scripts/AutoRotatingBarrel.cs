using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AutoRotatingTurretShooter : MonoBehaviour
{
    public GameObject normalBulletPrefab;
    public GameObject chargedBulletPrefab;
    public Transform firePoint;
    public GameObject fireParticlePrefab;


    public Image chargeFillImage;

    public float rotationSpeed = 120f;

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

    void Start()
    {
        currentAmmo = maxAmmo;

        if (chargeFillImage != null)
        {
            chargeFillImage.fillAmount = 0f;
        }
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
        UpdateChargeBar();

        if (!isCharging)
        {
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
        }
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
            chargeTimer = 0f;
        }
    }

    void Shoot()
    {
        if (!canShoot) return;

        if (fireParticlePrefab != null)
        {
            GameObject fx = Instantiate(fireParticlePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f,90f,-0f), firePoint);
            Destroy(fx, 2f);
        }

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

    void UpdateChargeBar()
    {
        if (chargeFillImage == null) return;

        chargeFillImage.fillAmount = isHolding
            ? chargeTimer / maxChargeTime
            : 0f;
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

        if (chargeFillImage != null)
        {
            chargeFillImage.fillAmount = 0f;
        }
    }
}