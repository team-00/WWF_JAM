using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private TrackManager trackManager;
    [SerializeField] private WaveInfo[] waves;

    private int currentWave = 0;

    public void StartNextWave()
    {
        StartCoroutine(WaveHandler());

    }

    private IEnumerator WaveHandler()
    {
        WaveInfo wave = waves[currentWave++];

        for(int i = 0; i < wave.TrashbagMobs.Length; i++)
        {
            var waiter = new WaitForSeconds(wave.TrashbagMobs[i].spawnFrequency);
            for(int k = 0; k < wave.TrashbagMobs[i].Count; k++)
            {
                trackManager.CreateTrashbag(wave.TrashbagMobs[i].Trashbag);
                yield return waiter;
            }
        }
    }
}
