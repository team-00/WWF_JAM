using UnityEngine;

[CreateAssetMenu(fileName = "new TrashbagStats", menuName = "ScriptableObjects/TrashbagStats", order = 0)]
public class TrashbagStats : ScriptableObject
{
    [Header("Children on Destruction")]
    public TrashbagStats[] ChildTrashbagStats;

    [Header("Visuals")]
    public Sprite TrashSprite;

    [Header("Stats")]
    public float ProgressionSpeed = 1f;
    public int Hitpoints = 1;

    public virtual void ApplyStats(Trashbag trashbag)
    {
        trashbag.SpriteRenderer.sprite = TrashSprite;
        trashbag.CurrentHitpoints = Hitpoints;
    }
}
