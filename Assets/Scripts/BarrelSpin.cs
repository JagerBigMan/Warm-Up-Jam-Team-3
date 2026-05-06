using UnityEngine;

public class BarrelSpin : MonoBehaviour
{
    public float rotationSpeed = 120f;
    public TurretManager shooter;

    void Update()
    {
        if (shooter == null) return;
        if (!shooter.canShoot) return;
        if (shooter.IsChargingShot()) return;

        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }
}