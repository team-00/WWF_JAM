using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [HideInInspector] public Trashbag target;
    [HideInInspector] public GameManager gm;
    [HideInInspector] public bool IsActive;
    [HideInInspector] public int TargetMode = 0;

    private TurretStats stats;
    public TurretStats Stats
    {
        get => stats;
        set
        {
            stats = value;
            stats.ApplyStats(this);

            rangeIndicator.transform.localScale = new Vector3(Stats.AttackRange, Stats.AttackRange, 0f);
            circleCollider.radius = Stats.ColliderRadius;
            sqrAttackRange = Stats.AttackRange * Stats.AttackRange;
        }
    }
    [SerializeField] Color invalidColor;
    [SerializeField] LayerMask trashLayer;
    [SerializeField] Projectile projectilePrefab;

    public SpriteRenderer SpriteRenderer;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rgb;

    private GameObject rangeIndicator;
    private float currentAttackTimer;
    private float sqrAttackRange;
    private int colCount;
    private readonly Queue<Projectile> pooledProjectiles = new Queue<Projectile>();

    private void Awake()
    {
        rgb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        rangeIndicator = transform.GetChild(0).gameObject;
    }

    private void OnDestroy()
    {
        while(pooledProjectiles.Count > 0)
        {
            Destroy(pooledProjectiles.Dequeue());
        }
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
        if(IsActive)
        {
            if(target != null)
            {
                if ((transform.position - target.transform.position).sqrMagnitude > sqrAttackRange)
                {
                    // release target lock and look for new target
                    target = null;
                    LookForTarget();
                }
            }
            else
            {
                LookForTarget();
            }
        }
    }

    private void Update()
    {
        // rotation towards target (move to update)
        if (target != null)
        {
            transform.up = target.transform.position - transform.position;

            if (currentAttackTimer < 0f)
            {
                Shoot();
                currentAttackTimer = Stats.AttackSpeed;
            }
        }

        currentAttackTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        Projectile proj;
        if(pooledProjectiles.Count > 0)
        {
            proj = pooledProjectiles.Dequeue();
            proj.transform.position = transform.position;
            proj.transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, 90f);
            proj.gameObject.SetActive(true);
        }
        else
        {
            proj = CreateNewProjectile();
        }
        proj.MaxDist = Stats.AttackRange * 2;
        proj.ReassignID();
        proj.ResetProjectile();
    }

    private Projectile CreateNewProjectile()
    {
        Projectile proj = Instantiate(
                projectilePrefab,
                transform.position,
                transform.rotation * Quaternion.Euler(0f, 0f, 90f));
        proj.Stats = stats.ProjectileStats;
        proj.originTurret = this;
        proj.gm = gm;
        return proj;
    }

    private void LookForTarget()
    {
        var collisions = Physics2D.OverlapCircleAll(transform.position, Stats.AttackRange, trashLayer);

        if(TargetMode == 0)
        {
            float highestTrashProgress = 0f;
            for (int i = 0; i < collisions.Length; i++)
            {
                Trashbag trash = collisions[i].GetComponent<Trashbag>();
                if (trash.trackProgress > highestTrashProgress)
                {
                    highestTrashProgress = trash.trackProgress;
                    target = trash;
                }
            }
        }
        else if(TargetMode == 1)
        {
            float highestTrashProgress = float.MaxValue;
            for (int i = 0; i < collisions.Length; i++)
            {
                Trashbag trash = collisions[i].GetComponent<Trashbag>();
                if (trash.trackProgress < highestTrashProgress)
                {
                    highestTrashProgress = trash.trackProgress;
                    target = trash;
                }
            }
        }
    }

    public bool SetPlacementValidity(bool valid)
    {
        if(valid && colCount == 0)
        {
            SpriteRenderer.color = Color.white;
            return true;
        }
        else
        {
            SpriteRenderer.color = invalidColor;
            return false;
        }
    }

    public void Activate()
    {
        Destroy(rgb);
        IsActive = true;
        rangeIndicator.SetActive(false);

        for(int i = 0; i < (int)(1f / stats.AttackSpeed); i++)
        {
            PoolProjectile(CreateNewProjectile());
        }
    }

    public void SetRangeIndicatorActive(bool activeState)
    {
        rangeIndicator.SetActive(activeState);
    }

    public void PoolProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
        //pooledProjectiles.Enqueue(projectile);
        //projectile.gameObject.SetActive(false);
    }
}