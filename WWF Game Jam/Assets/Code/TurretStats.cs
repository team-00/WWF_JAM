using UnityEngine;

[CreateAssetMenu(fileName = "new TurretStats", menuName = "ScriptableObjects/TurretStats", order = 1)]
public class TurretStats : ScriptableObject
{
    [Header("Turret Settings")]
    public float ColliderRadius;
    public float TrackCollisionRadius;

    [Header("Combat Settings")]
    public float AttackRange;
}
