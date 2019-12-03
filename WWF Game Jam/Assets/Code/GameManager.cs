using UnityEngine.Tilemaps;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;


    void Start()
    {
        tilemap.RefreshAllTiles();
    }
}
