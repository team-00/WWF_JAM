using UnityEngine;

public enum TurretType
{
    Default
}

[RequireComponent(typeof(GameManager))]
public class TurretManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float turretPlacementCooldown;
    [SerializeField] private Turret turretPrefab;
    [SerializeField] private TurretStats[] turretStats;

    [HideInInspector] public Turret CurTurret;
    private GameManager gm;
    private float currentTurretPlacementCooldown;

    private void Awake()
    {
        gm = GetComponent<GameManager>();

        gm.ui.SetUI(gm.Health, gm.Health, gm.Gold, turretStats);
    }

    public void EnterPlacementMode(Turret turret)
    {
        if (CurTurret != null) return;
        else CurTurret = turret;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RequestTurretPlacement(0);
        }


        if (currentTurretPlacementCooldown > 0f)
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
                gm.Gold -= CurTurret.Stats.TurretPrice;
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

    public void RequestTurretPlacement(int turretID)
    {
        if (CurTurret != null || currentTurretPlacementCooldown > 0f || gm.Gold < turretStats[turretID].TurretPrice) return;

        // check monies
        // suck dick
        // ocean man

        Turret go = Instantiate(turretPrefab, Input.mousePosition, Quaternion.identity);
        go.Stats = turretStats[turretID];
        go.gm = gm;
        EnterPlacementMode(go);
    }

    public string[] GetTurretInfo(int turretID)
    {
        return new string[] { turretStats[turretID].name, turretStats[turretID].TurretInfo };
    }
}
