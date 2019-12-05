using UnityEngine;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TrackManager : MonoBehaviour
{
    [Header("Gizmos")]
    [SerializeField] private float arrowheadAngle = 20f;
    [SerializeField] private float arrowheadLength = .3f;

    [Header("Track Fields")]
    [SerializeField] private GameObject trashbag;
    [SerializeField] private Vector2[] waypoints;
    public AnimationCurve curve;


    public float trackLength;
    private readonly List<Trashbag> trashbags = new List<Trashbag>();

    private void Awake()
    {
        trackLength = 0f;
        for(int i = 0; i < waypoints.Length - 1; )
        {
            curve.AddKey(new Keyframe(trackLength, i, 0f, 0f, 0f, 0f));
            trackLength += Vector2.Distance(waypoints[i], waypoints[++i]);
        }
        curve.AddKey(new Keyframe(trackLength, waypoints.Length - 1, 0f, 0f, 0f, 0f));

        trashbags.Add(Instantiate(trashbag, GetTilemapPos(waypoints[0], 0f), Quaternion.identity).GetComponent<Trashbag>());
    }

    private void Update()
    {
        MoveTrashbags();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            trashbags.Add(Instantiate(trashbag, GetTilemapPos(waypoints[0], 0f), Quaternion.identity).GetComponent<Trashbag>());
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

            if (actualIndex + 2 > waypoints.Length)
            {
                DestroyTrashbag(trashbag, ref i);
                continue;
            }
            Vector2 resultPos = Vector2.Lerp(
                waypoints[actualIndex],
                waypoints[actualIndex + 1],
                actualProgress - actualIndex);
            trashbag.transform.position = GetTilemapPos(resultPos, 0f);

            trashbag.trackProgress += trashbag.stats.ProgressionSpeed * dt;
        }
    }

    private void DestroyTrashbag(Trashbag trashbag, ref int i)
    {
        trashbags.RemoveAt(i--);
        Destroy(trashbag.gameObject);
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
