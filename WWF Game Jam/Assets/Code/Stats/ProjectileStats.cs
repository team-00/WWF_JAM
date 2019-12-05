using UnityEngine;

[CreateAssetMenu(fileName = "new ProjectileStats", menuName = "ScriptableObjects/ProjectileStats", order = 2)]
public class ProjectileStats : ScriptableObject
{
    public float ProjectileSpeed = 20f;
    public float CollisionRadius  = .25f;
    public float MaxTrashKills = 1;
    public int TrashDamage = 1;
}
