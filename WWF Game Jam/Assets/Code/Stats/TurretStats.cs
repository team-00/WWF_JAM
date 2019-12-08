using UnityEngine;

[CreateAssetMenu(fileName = "new TurretStats", menuName = "ScriptableObjects/TurretStats", order = 1)]
public class TurretStats : ScriptableObject
{
    [Header("Turret Settings")]
    public float ColliderRadius;
    public float TrackCollisionRadius;
    public Sprite TurretSprite;
    public Sprite ShopSprite;

    [Header("Combat Settings")]
    public ProjectileStats ProjectileStats;
    public float AttackRange;
    public float AttackSpeed;

    [Header("Shop Settings")]
    public int TurretPrice;
    public int TurretSellPrice;
    [TextArea] public string TurretInfo = "It shoots";

    public virtual void ApplyStats(Turret turret)
    {
        turret.SpriteRenderer.sprite = TurretSprite;
    }
}
