using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentedHealthBarUI : MonoBehaviour
{
    public GameObject hpBlockPrefab;
    public Transform hpBlockParent;

    // your colors
    public Color fullColor = new Color32(0x3B, 0xFF, 0xB6, 255);
    public Color emptyColor = new Color32(0xFF, 0x66, 0x66, 255);

    private List<Image> hpBlocks = new List<Image>();

    public void CreateBlocks(int maxHP)
    {
        foreach (Transform child in hpBlockParent)
        {
            Destroy(child.gameObject);
        }

        hpBlocks.Clear();

        for (int i = 0; i < maxHP; i++)
        {
            GameObject block = Instantiate(hpBlockPrefab, hpBlockParent);
            Image image = block.GetComponent<Image>();

            if (image != null)
            {
                hpBlocks.Add(image);
            }
        }
    }

    public void UpdateHealth(int currentHP)
    {
        for (int i = 0; i < hpBlocks.Count; i++)
        {
            hpBlocks[i].color = i < currentHP ? fullColor : emptyColor;
        }
    }
}