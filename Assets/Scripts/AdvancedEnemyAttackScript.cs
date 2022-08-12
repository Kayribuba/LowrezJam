using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] struct AEASBarrel
{
    public GameObject barrelGO;
    public PhasedAttackPatterns[] PAP;
}
[Serializable]
struct PhasedAttackPatterns
{
    public EnemyAttackPatern ap;
}

[RequireComponent(typeof(StationaryEnemyHealthScript))]
public class AdvancedEnemyAttackScript : MonoBehaviour
{
    [SerializeField] AEASBarrel[] barrels;
    [SerializeField] GameObject[] projectiles;

    [Header("BU GEÇÝCÝ SÝL BUNUAAÐÐ")]
    [SerializeField] int phase;
    int oldPhase = -1;

    StationaryEnemyHealthScript HealthScript;
    int[] EAPSINDEXES;
    float[] targetTimes;
    float gridSize;

    void Start()
    {
        if (FindObjectOfType<GridSizer>() != null)
            gridSize = FindObjectOfType<GridSizer>().GetGridSize();
        else
        {
            Debug.Log("Grid size nerde lan yok diye grid sizeyi 0.15625f yabdým");
            gridSize = 0.15625f;
        }

        HealthScript = GetComponent<StationaryEnemyHealthScript>();
        HealthScript.PhaseChangedEvent += HealthScript_PhaseChangedEvent;

        targetTimes = new float[barrels.Length];
        EAPSINDEXES = new int[barrels.Length];
    }

    void HealthScript_PhaseChangedEvent(object sender, int e)
    {
        phase = e - 1;
    }

    void Update()
    {
        if (oldPhase != phase)
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                targetTimes[i] = Time.time;
                EAPSINDEXES[i] = 0;
            }

            oldPhase = phase;
        }

        for (int i = 0; i < targetTimes.Length; i++)
        {
            int eapsIndex = EAPSINDEXES[i];
            float t = targetTimes[i];
            AEASBarrel barrel = barrels[i];


            if (t <= Time.time)
            {
                EAPStruct currentStruct;

                if (phase <= barrel.PAP.Length - 1 && phase >= 0)
                {
                    PhasedAttackPatterns p = barrel.PAP[phase];

                    if (eapsIndex > p.ap.attackPatern.Length - 1)
                    {
                        EAPSINDEXES[i] = 0;
                        eapsIndex = 0;
                    }

                    currentStruct = p.ap.attackPatern[eapsIndex];
                }
                else
                {
                    currentStruct = new EAPStruct { directionsToAttack = null, waitAfter = 1 };
                }

                //AAAAAAAAAAAAAA

                if (currentStruct.directionsToAttack != null)
                {
                    List<Dir> usedDirs = new List<Dir>();
                    System.Random rand = new System.Random();

                    foreach (Dir attackDir in currentStruct.directionsToAttack)
                    {
                        if (!usedDirs.Contains(attackDir) && projectiles != null)
                        {
                            Vector2 barrelCorrectorVector = barrel.barrelGO.transform.position;
                            barrelCorrectorVector.x -= barrelCorrectorVector.x % gridSize;
                            barrelCorrectorVector.y -= barrelCorrectorVector.y % gridSize;
                            barrel.barrelGO.transform.position = barrelCorrectorVector;

                            GameObject bulletInstantiated = Instantiate(projectiles[rand.Next(0, projectiles.Length)], barrel.barrelGO.transform.position, Quaternion.identity);
                            bulletInstantiated.GetComponent<EnemyBulletScript>().SetFlightVector(DirectionEnum.GetVector2DirFromEnum(attackDir), gridSize);
                            bulletInstantiated.GetComponent<EnemyBulletScript>().SetParentGO(gameObject);

                            usedDirs.Add(attackDir);
                        }
                    }
                }

                if (phase > barrel.PAP.Length - 1 || phase < 0)
                    EAPSINDEXES[i] = 0;
                else if (eapsIndex == barrel.PAP[phase].ap.attackPatern.Length - 1)
                    EAPSINDEXES[i] = 0;
                else
                    EAPSINDEXES[i]++;

                targetTimes[i] = currentStruct.waitAfter + Time.time;
            }

            //AAAAAAAAAAAAAA
        }
    }
}
