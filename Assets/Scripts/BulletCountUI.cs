using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountUI : MonoBehaviour
{
    public TurretManager shooter;

    public GameObject bulletIconPrefab;
    public Transform bulletIconParent;

    public Color fullColor = Color.white;
    public Color emptyColor = new Color(1f, 1f, 1f, 0.25f);

    private List<Image> bulletIcons = new List<Image>();

    void Start()
    {
        CreateBulletIcons();
        UpdateAmmoUI();
    }

    void Update()
    {
        UpdateAmmoUI();
    }

    void CreateBulletIcons()
    {
        for (int i = 0; i < shooter.maxAmmo; i++)
        {
            GameObject icon = Instantiate(bulletIconPrefab, bulletIconParent);
            Image image = icon.GetComponent<Image>();

            if (image != null)
            {
                bulletIcons.Add(image);
            }
        }
    }

    void UpdateAmmoUI()
    {
        if (shooter == null) return;

        for (int i = 0; i < bulletIcons.Count; i++)
        {
            bulletIcons[i].color = i < shooter.currentAmmo ? fullColor : emptyColor;
        }
    }
}