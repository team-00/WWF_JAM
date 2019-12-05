using UnityEngine.Tilemaps;
using UnityEngine;

[RequireComponent(typeof(TrackManager), typeof(TurretManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    public int Health
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            // TODO update ui
        }
    }

    private int currentGold;
    public int Gold
    {
        get => currentGold;
        set
        {
            currentGold = value;
            // TODO update ui
        }
    }

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera cam;
    [SerializeField] private Turret turretPrefab;
    [SerializeField] private LayerMask turretLayer;

    private TilemapCollider2D tilemapCollider;
    private TurretManager turretPlacer;
    private Turret lastHoveredTurret;

    private void Awake()
    {
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
        turretPlacer = GetComponent<TurretManager>();

        Health = currentHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && turretPlacer.CurTurret == null)
        {
            Turret go = Instantiate(turretPrefab, Input.mousePosition, Quaternion.identity);
            turretPlacer.EnterPlacementMode(go);
        }

        UpdateHoveredTurret();
    }

    private void UpdateHoveredTurret()
    {
        lastHoveredTurret?.SetRangeIndicatorActive(false);
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, turretLayer);
        if (hit)
        {
            lastHoveredTurret = hit.collider.GetComponent<Turret>();
            lastHoveredTurret.SetRangeIndicatorActive(true);
        }
    }

    public bool IsMouseCollidingWithTilemap(float colliderRadius)
    {
        var worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        var closestPoint = tilemapCollider.ClosestPoint(worldPoint);
        float dist = Vector2.Distance(closestPoint, worldPoint);
        return dist < colliderRadius;
    }
}
