using UnityEngine;

[CreateAssetMenu(fileName = "new TrashbagStats", menuName = "ScriptableObjects/TrashbagStats", order = 0)]
public class TrashbagStats : ScriptableObject
{
    [Header("Children on Destruction")]
    public TrashbagStats ChildTrashbagStats;
    public int ChildSpawnCount;

    [Header("Visuals")]
    public Color trashColor = Color.white;

    [Header("Stats")]
    public float progressionSpeed;
}
