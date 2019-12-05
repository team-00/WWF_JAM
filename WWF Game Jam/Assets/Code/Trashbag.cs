using UnityEngine;

public class Trashbag : MonoBehaviour
{
    public TrashbagStats stats;
    public float trackProgress = 0f;

    private void OnDestroy()
    {
        Debug.Log("TRASH DED");
    }
}
