using UnityEngine;

public class EnemyFaceTurret : MonoBehaviour
{
    private Transform turret;

    public void SetTurret(Transform t)
    {
        turret = t;
    }

    void Start()
    {
        if (turret == null)
        {
            turret = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Update()
    {
        if (turret == null) return;

        Vector2 direction = turret.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float offset = transform.position.x < turret.position.x ? 0f : 180f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);
    }
}