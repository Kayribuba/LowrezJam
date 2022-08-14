using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlackout : MonoBehaviour
{
    [SerializeField] GameObject blackout;

    bool boIsSpawned;

    public void SpawnBO()
    {
        if (!boIsSpawned)
        {
            Instantiate(blackout, Vector3.zero, Quaternion.identity);
            boIsSpawned = true;
        }
    }
}
