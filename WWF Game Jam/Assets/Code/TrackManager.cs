using UnityEngine;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour
{
    [Header("Gizmos")]
    [SerializeField] private float arrowheadAngle = 20f;
    [SerializeField] private float arrowheadLength = .3f;

    [Header("Track Fields")]
    [SerializeField] private int debugSpawnCount;
    [SerializeField] private Trashbag trashbagPrefab;
    [SerializeField] private Vector2[] waypoints;
    public AnimationCurve curve;

    public float trackLength;
    private readonly List<Trashbag> trashbags = new List<Trashbag>();
    private GameManager gm;

    private void Awake()
    {
        gm = GetComponent<GameManager>();

        // calc track length
        trackLength = 0f;
        for(int i = 0; i < waypoints.Length - 1; )
        {
            curve.AddKey(new Keyframe(trackLength, i, 0f, 0f, 0f, 0f));
            trackLength += Vector2.Distance(waypoints[i], waypoints[++i]);
        }
        curve.AddKey(new Keyframe(trackLength, waypoints.Length - 1, 0f, 0f, 0f, 0f));

        // debug trashbag
        CreateTrashbag();
    }

    private void Update()
    {
        MoveTrashbags();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < debugSpawnCount; i++) CreateTrashbag();
        }
    }

    private void MoveTrashbags()
    {
        Trashbag trashbag;
        float dt = Time.deltaTime;
        for (int i = 0; i < trashbags.Count; i++)
        {
            trashbag = trashbags[i];

            // remove trashbag if its been destroyed
            if(trashbag == null)
            {
                trashbags.RemoveAt(i--);
                continue;
            }

            float actualProgress = curve.Evaluate(trashbag.trackProgress);
            int actualIndex = (int)actualProgress;

            // check if trash reached the end
            if (actualIndex + 2 > waypoints.Length)
            {
                gm.Health -= DeductHealth(trashbag.Stats);
                DestroyTrashbag(trashbag, ref i);
                continue;
            }
            Vector2 resultPos = Vector2.Lerp(
                waypoints[actualIndex],
                waypoints[actualIndex + 1],
                actualProgress - actualIndex);
            trashbag.transform.position = GetTilemapPos(resultPos, 0f);

            trashbag.trackProgress += trashbag.Stats.ProgressionSpeed * dt;
        }
    }

    private int DeductHealth(TrashbagStats stats)
    {
        int totalHealthReduc = 1;
        for(int i = 0; i < stats.ChildTrashbagStats.Length; i++)
        {
            totalHealthReduc += DeductHealth(stats.ChildTrashbagStats[i]);
        }
        return totalHealthReduc;
    }

    private void DestroyTrashbag(Trashbag trashbag, ref int i)
    {
        trashbags.RemoveAt(i--);
        Destroy(trashbag.gameObject);
    }

    public Trashbag CreateTrashbag(TrashbagStats stats = null)
    {
        Trashbag bag = Instantiate(trashbagPrefab, GetTilemapPos(waypoints[0], 0f), Quaternion.identity);
        AddTrashbag(bag);
        bag.TrackManager = this;
        if (stats != null) bag.Stats = stats;
        else bag.Stats.ApplyStats(bag);
        return bag;
    }

    public void AddTrashbag(Trashbag trashbag)
    {
        trashbags.Add(trashbag);
    }

    public static Vector2 GetTilemapPos(float x, float y)
    {
        return new Vector2(x + .5f, y + .5f);
    }
    public static Vector2 GetTilemapPos(Vector2 vec)
    {
        return new Vector2(vec.x + .5f, vec.y + .5f);
    }
    public static Vector3 GetTilemapPos(Vector2 vec, float z)
    {
        return new Vector3(vec.x + .5f, vec.y + .5f, z);
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for(int i = 0; i < waypoints.Length - 1; )
        {
            DrawArrow(waypoints[i], waypoints[++i]);
        }
    }

    private void DrawArrow(Vector2 start, Vector2 end)
    {
        start = GetTilemapPos(start);
        end = GetTilemapPos(end);

        Gizmos.DrawLine(start, end);
        Vector2 dir = (end - start).normalized * arrowheadLength;

        Vector2 v = Quaternion.Euler(0f, 0f, arrowheadAngle) * dir;
        Gizmos.DrawLine(end, end - v);

        v = Quaternion.Euler(0f, 0f, -arrowheadAngle) * dir;
        Gizmos.DrawLine(end, end - v);
    }
    #endregion
}
