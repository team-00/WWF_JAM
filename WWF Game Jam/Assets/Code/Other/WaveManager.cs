using System.Collections;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private TrackManager trackManager;
    [SerializeField] private int goldPerRound;
    [SerializeField] private WaveInfo[] waves;
    [SerializeField] private TextMeshProUGUI rounds;

    private int currentWave = 0;

    public void StartNextWave()
    {
        StartCoroutine(WaveHandler());

        gm.Gold += goldPerRound;
        AudioManager.PlayButtonSound();
        rounds.text = "ROUND\n" + currentWave;
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
