using UnityEngine.Tilemaps;
using UnityEngine;
using System;

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
            ui.UpdateHeath(currentHealth);
            if(currentHealth < 1)
            {
                Time.timeScale = 0f;
                ui.ActivateFailureWindow();
            }
        }
    }

    private int currentGold;
    public int Gold
    {
        get => currentGold;
        set
        {
            currentGold = value;
            ui.UpdateCoin(currentGold); // yannick needs to die
        }
    }

    [SerializeField] private MainUIWindow ui;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask turretLayer;

    private TilemapCollider2D tilemapCollider;
    private Turret lastHoveredTurret;
    private TurretManager turManager;

    private void Awake()
    {
        turManager = GetComponent<TurretManager>();
        tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();

        ui.SetUI(new int[0], currentHealth, currentHealth, 0, new Sprite[0]);
    }

    private void Update()
    {
        UpdateHoveredTurret();
    }

    private void UpdateHoveredTurret()
    {
        if(lastHoveredTurret != null)
        {
            lastHoveredTurret.SetRangeIndicatorActive(false);
        }

        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction, 100f, turretLayer);
        Turret targetedTurret;
        for(int i = 0; i < hit.Length; i++)
        {
            // activate raycast turret range
            targetedTurret = hit[i].collider.GetComponent<Turret>();
            if(targetedTurret.IsActive)
            {
                targetedTurret.SetRangeIndicatorActive(true);
                lastHoveredTurret = targetedTurret;
                break;
            }
        }
        if(lastHoveredTurret == null && turManager.CurTurret != null)
        {
            // see current built turret range
            lastHoveredTurret = turManager.CurTurret;
            turManager.CurTurret.SetRangeIndicatorActive(true);
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
