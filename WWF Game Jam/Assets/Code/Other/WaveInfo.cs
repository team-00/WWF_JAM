using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new WaveInfo", menuName = "ScriptableObjects/WaveInfo", order = 3)]
public class WaveInfo : ScriptableObject
{
    public TrashbagMob[] TrashbagMobs;
}


[Serializable]
public class TrashbagMob
{
    public TrashbagStats Trashbag;
    public int Count;
    public float spawnFrequency;
}
