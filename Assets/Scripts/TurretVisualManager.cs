using UnityEngine;

public class TurretVisualManager : MonoBehaviour
{
    public SpriteRenderer turretRenderer;
    public SpriteRenderer barrelRenderer;

    public Sprite turretAliveSprite;
    public Sprite turretDeadSprite;

    public Sprite barrelAliveSprite;
    public Sprite barrelDeadSprite;

    void Start()
    {
        SetAlive();
    }

    public void SetAlive()
    {
        if (turretRenderer != null && turretAliveSprite != null)
        {
            turretRenderer.sprite = turretAliveSprite;
        }

        if (barrelRenderer != null && barrelAliveSprite != null)
        {
            barrelRenderer.sprite = barrelAliveSprite;
        }
    }

    public void SetDead()
    {
        if (turretRenderer != null && turretDeadSprite != null)
        {
            turretRenderer.sprite = turretDeadSprite;
        }

        if (barrelRenderer != null && barrelDeadSprite != null)
        {
            barrelRenderer.sprite = barrelDeadSprite;
        }
    }
}