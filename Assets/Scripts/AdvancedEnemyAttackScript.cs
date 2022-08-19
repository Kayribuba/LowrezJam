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

    [Header("BU GE��C� S�L BUNUAA��")]
    [SerializeField] int phase;
    int oldPhase = -1;

    StationaryEnemyHealthScript HealthScript;
    int[] EAPSINDEXES;
    float[] targetTimes;
    float gridSize;

    void Start()
    {
        if (FindObjectOfType<GridSizer>() != null)
            gridSize = FindObjectOfType<GridSizer>().GetGridSize();//gridSizer'� al
        else
        {
            Debug.Log("Grid size nerde lan yok diye grid sizeyi 0.15625f yabd�m");
            gridSize = 0.15625f;
        }

        HealthScript = GetComponent<StationaryEnemyHealthScript>();
        HealthScript.PhaseChangedEvent += HealthScript_PhaseChangedEvent;//healthScript'i al, phase eventine abone ol

        targetTimes = new float[barrels.Length];
        EAPSINDEXES = new int[barrels.Length];//Arrayleri olu�tur

        foreach(AEASBarrel barrel in barrels)//barrellerin yerini gride uygun hale getir
        {
            Vector2 barrelCorrectorVector = barrel.barrelGO.transform.position;
            barrelCorrectorVector.x -= barrelCorrectorVector.x % gridSize;
            barrelCorrectorVector.y -= barrelCorrectorVector.y % gridSize;
            barrel.barrelGO.transform.position = barrelCorrectorVector;
        }
    }

    void HealthScript_PhaseChangedEvent(object sender, int e)//abone olunan eventin metodu (phase de�i�ince �al���yo, e g�ncel phase)
    {
        phase = e - 1;
    }

    void Update()
    {
        if (oldPhase != phase)//phase de�i�tiyse her�eyi s�f�rla ki gereksiz beklemesin
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
            float t = targetTimes[i];

            if (t <= Time.time)//herhangi barrelin vakti geldiyse
            {
                int eapsIndex = EAPSINDEXES[i];
                AEASBarrel barrel = barrels[i];

                EAPStruct currentStruct;

                if (phase <= barrel.PAP.Length - 1 && phase >= 0)//barrelde phaseye uygun attack pattern varsa...
                {
                    PhasedAttackPatterns p = barrel.PAP[phase];

                    if (eapsIndex > p.ap.attackPatern.Length - 1)
                    {
                        EAPSINDEXES[i] = 0;
                        eapsIndex = 0;
                    }

                    currentStruct = p.ap.attackPatern[eapsIndex];//phaseye uygun attack patterni currentStruct'a koy
                }
                else//...yoksa
                {
                    currentStruct = new EAPStruct { directionsToAttack = null, waitAfter = 1 };// currentStruct'� null bi de�ere e�itle
                }

                //AAAAAAAAAAAAAA

                if (currentStruct.directionsToAttack != null)//currentStruct null de�ilse
                {
                    List<Dir> usedDirs = new List<Dir>();//ayn� Dir'i tekrar kullanmamak i�in liste a�
                    System.Random rand = new System.Random();

                    foreach (Dir attackDir in currentStruct.directionsToAttack)
                    {
                        if (!usedDirs.Contains(attackDir) && projectiles != null)//ayn� Dir de�ilse ve at�cak mermi varsa
                        {
                            GameObject bulletInstantiated = Instantiate(projectiles[rand.Next(0, projectiles.Length)], barrel.barrelGO.transform.position, Quaternion.identity);//mermiyi olu�tur...
                            bulletInstantiated.GetComponent<EnemyBulletScript>().SetFlightVector(DirectionEnum.GetVector2DirFromEnum(attackDir), gridSize);//...hareket ettir...
                            bulletInstantiated.GetComponent<EnemyBulletScript>().SetParentGO(gameObject);//...parentini ayarla.

                            usedDirs.Add(attackDir);//bu dir art�k kullan�ld� (ayn� anda iki kez atmamas� i�in, waitTime ge�tikten sonra bu silinicek)
                        }
                    }
                }

                if (phase > barrel.PAP.Length - 1 || phase < 0)//phasenin sonuna geldiysem yada error varsa
                    EAPSINDEXES[i] = 0;
                else if (eapsIndex == barrel.PAP[phase].ap.attackPatern.Length - 1)//attackPatternin sonuna geldiysem
                    EAPSINDEXES[i] = 0;
                else
                    EAPSINDEXES[i]++;//yoksa artt�r

                targetTimes[i] = currentStruct.waitAfter + Time.time;//tekrar ate� edilecek zaman �u anki zaman�n waitTime kadar sonras�
            }

            //AAAAAAAAAAAAAA
        }
    }
}
