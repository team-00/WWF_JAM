using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Turret originTurret;

    public float MaxDist { set { timeToMaxDist = value / Stats.ProjectileSpeed; } }

    public ProjectileStats Stats;
    [SerializeField] private LayerMask trashLayer;

    private int currentTrashKills;
    private float timeToMaxDist;
    private float currentTime;


    private void OnEnable()
    {
        currentTrashKills = 0;
        currentTime = 0f;
    }

    private void Update()
    {
        if(currentTime < timeToMaxDist)
        {
            transform.position = transform.position + transform.right * Time.deltaTime * Stats.ProjectileSpeed;
            currentTime += Time.deltaTime;
        }
        else
        {
            DestroyProjectile();
        }
    }

    private void FixedUpdate()
    {
        var collisions = Physics2D.OverlapCircleAll(transform.position, Stats.CollisionRadius, trashLayer);
        for(int i = 0; i < collisions.Length; i++)
        {
            if(currentTrashKills < Stats.MaxTrashKills)
            {
                Destroy(collisions[i].gameObject);
                currentTrashKills++;

                if (currentTrashKills == Stats.MaxTrashKills)
                {
                    DestroyProjectile();
                }
            }
            else
            {
                DestroyProjectile();
            }
        }
    }

    private void DestroyProjectile()
    {
        originTurret.PoolProjectile(this);
    }
}
