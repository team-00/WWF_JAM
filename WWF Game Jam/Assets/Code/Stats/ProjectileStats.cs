using UnityEngine;

[CreateAssetMenu(fileName = "new ProjectileStats", menuName = "ScriptableObjects/ProjectileStats", order = 2)]
public class ProjectileStats : ScriptableObject
{
    public float ProjectileSpeed;
    public float CollisionRadius;
    public float MaxTrashKills;
}
