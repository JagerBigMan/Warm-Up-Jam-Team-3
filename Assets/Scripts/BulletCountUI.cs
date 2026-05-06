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
        if (shooter == null)
        {
            shooter = FindAnyObjectByType<TurretManager>();
        }

        if (bulletIconParent == null)
        {
            bulletIconParent = transform;
        }

        CreateBulletIcons();
        UpdateAmmoUI();
    }

    void Update()
    {
        UpdateAmmoUI();
    }

    void CreateBulletIcons()
    {
        if (shooter == null) return;
        if (bulletIconPrefab == null) return;
        if (bulletIconParent == null) return;

        foreach (Transform child in bulletIconParent)
        {
            Destroy(child.gameObject);
        }

        bulletIcons.Clear();

        for (int i = 0; i < shooter.maxAmmo; i++)
        {
            GameObject icon = Instantiate(bulletIconPrefab, bulletIconParent);
            icon.SetActive(true);

            RectTransform rect = icon.GetComponent<RectTransform>();

            if (rect != null)
            {
                rect.localScale = Vector3.one;
            }

            Image image = icon.GetComponent<Image>();

            if (image != null)
            {
                image.enabled = true;
                image.color = fullColor;
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