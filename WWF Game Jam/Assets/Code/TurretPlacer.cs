using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class TurretPlacer : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float turretPlacementCooldown;

    [HideInInspector] public Turret CurTurret;
    private GameManager gm;
    private float currentTurretPlacementCooldown;

    private void Awake()
    {
        gm = GetComponent<GameManager>();
    }

    public void EnterPlacementMode(Turret turret)
    {
        if (CurTurret != null || currentTurretPlacementCooldown > 0f) return;
        else CurTurret = turret;
    }

    private void Update()
    {
        if(currentTurretPlacementCooldown > 0f)
        {
            currentTurretPlacementCooldown -= Time.deltaTime;
        }

        if(CurTurret != null)
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            CurTurret.transform.position = mouseWorldPos;

            bool canBePlaced = CurTurret.SetPlacementValidity(
                !gm.IsMouseCollidingWithTilemap(CurTurret.Stats.TrackCollisionRadius));

            if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) && canBePlaced)
            {
                CurTurret.SetPlacementValidity(true);
                CurTurret.Activate();
                CurTurret = null;

                currentTurretPlacementCooldown = turretPlacementCooldown;
            }
            else if(Input.GetMouseButtonDown(1))
            {
                Destroy(CurTurret.gameObject);
                CurTurret = null;
            }
        }
    }
}
