using System.Collections.Generic;
using UnityEngine;

public class Trashbag : MonoBehaviour
{
    [HideInInspector] public int CurrentHitpoints = 1;
    [HideInInspector] public float trackProgress = 0f;
    [HideInInspector] public TrackManager TrackManager;

    public SpriteRenderer SpriteRenderer;
    private readonly HashSet<int> previouslyHitProjectiles = new HashSet<int>();

    [SerializeField] private TrashbagStats stats;
    public TrashbagStats Stats
    {
        get => stats;
        set
        {
            stats = value;
            stats.ApplyStats(this);
        }
    }

    private void DestroyTrashbag()
    {
        for (int i = 0; i < Stats.ChildTrashbagStats.Length; i++)
        {
            Trashbag trashbag = TrackManager.CreateTrashbag(Stats.ChildTrashbagStats[i]);
            trashbag.trackProgress = trackProgress - i * .2f;
        }
        Destroy(gameObject);
    }

    public bool AttackTrashbag(Projectile projectile)
    {
        if(!previouslyHitProjectiles.Contains(projectile.ID))
        {
            previouslyHitProjectiles.Add(projectile.ID);
            CurrentHitpoints--;
            if(CurrentHitpoints < 1)
            {
                DestroyTrashbag();
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
