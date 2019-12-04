using UnityEngine.Tilemaps;
using UnityEngine;

[RequireComponent(typeof(TrackManager), typeof(TurretPlacer))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject turretPrefab;

    private TilemapCollider2D tilemapCollider;
    private TurretPlacer turretPlacer;

    private void Awake()
    {
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
        turretPlacer = GetComponent<TurretPlacer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && turretPlacer.CurTurret == null)
        {
            GameObject go = Instantiate(turretPrefab, Input.mousePosition, Quaternion.identity);
            turretPlacer.EnterPlacementMode(go.GetComponent<Turret>());
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
