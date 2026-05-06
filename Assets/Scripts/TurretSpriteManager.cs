using UnityEngine;
using UnityEngine.UI;

public class TurretSpriteManager : MonoBehaviour
{
    public Image hpImage;
    public Sprite aliveSprite;
    public Sprite deadSprite;

    void Start()
    {
        SetAlive();
    }

    public void SetAlive()
    {
        if (hpImage != null && aliveSprite != null)
        {
            hpImage.sprite = aliveSprite;
        }
    }

    public void SetDead()
    {
        if (hpImage != null && deadSprite != null)
        {
            hpImage.sprite = deadSprite;
        }
    }
}