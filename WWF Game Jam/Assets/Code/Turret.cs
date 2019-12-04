using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public TurretStats Stats;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] Color invalidColor;
    [SerializeField] LayerMask trashLayer;

    private bool isActive;
    private int colCount;
    private readonly List<Projectile> activeProjectiles = new List<Projectile>();
    private readonly List<Projectile> pooledProjectiles = new List<Projectile>();

    private void Awake()
    {
        circleCollider.radius = Stats.ColliderRadius;
    }

    private void OnDestroy()
    {
        for(int i = 0; i < activeProjectiles.Count; i++)
        {
            Destroy(activeProjectiles[i].gameObject);
        }
        for (int i = 0; i < pooledProjectiles.Count; i++)
        {
            Destroy(pooledProjectiles[i].gameObject);
        }

        activeProjectiles.Clear();
        pooledProjectiles.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        colCount++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        colCount--;
    }

    private void FixedUpdate()
    {
        if(isActive)
        {
            var collisions = Physics2D.OverlapCircleAll(transform.position, Stats.AttackRange, trashLayer);

            Trashbag highestPriorityTrash = null;
            float highestTrashProgress = 0f;
            for (int i = 0; i < collisions.Length; i++)
            {
                Trashbag trash = collisions[i].GetComponent<Trashbag>();
                if(trash.trackProgress > highestTrashProgress)
                {
                    highestTrashProgress = trash.trackProgress;
                    highestPriorityTrash = trash;
                }
            }

            if(highestPriorityTrash != null)
            {
                transform.up = highestPriorityTrash.transform.position - transform.position;
            }
        }
    }

    public bool SetPlacementValidity(bool valid)
    {
        if(valid && colCount == 0)
        {
            spriteRenderer.color = Color.white;
            return true;
        }
        else
        {
            spriteRenderer.color = invalidColor;
            return false;
        }
    }

    public void Activate()
    {
        isActive = true;
    }
}