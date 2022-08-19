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
            gridSize = FindObjectOfType<GridSizer>().GetGridSize();//gridSizer'ý al
        else
        {
            Debug.Log("Grid size nerde lan yok diye grid sizeyi 0.15625f yabdým");
            gridSize = 0.15625f;
        }

        HealthScript = GetComponent<StationaryEnemyHealthScript>();
        HealthScript.PhaseChangedEvent += HealthScript_PhaseChangedEvent;//healthScript'i al, phase eventine abone ol

        targetTimes = new float[barrels.Length];
        EAPSINDEXES = new int[barrels.Length];//Arrayleri oluþtur

        foreach(AEASBarrel barrel in barrels)//barrellerin yerini gride uygun hale getir
        {
            Vector2 barrelCorrectorVector = barrel.barrelGO.transform.position;
            barrelCorrectorVector.x -= barrelCorrectorVector.x % gridSize;
            barrelCorrectorVector.y -= barrelCorrectorVector.y % gridSize;
            barrel.barrelGO.transform.position = barrelCorrectorVector;
        }
    }

    void HealthScript_PhaseChangedEvent(object sender, int e)//abone olunan eventin metodu (phase deðiþince çalýþýyo, e güncel phase)
    {
        phase = e - 1;
    }

    void Update()
    {
        if (oldPhase != phase)//phase deðiþtiyse herþeyi sýfýrla ki gereksiz beklemesin
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
                    currentStruct = new EAPStruct { directionsToAttack = null, waitAfter = 1 };// currentStruct'ý null bi deðere eþitle
                }

                //AAAAAAAAAAAAAA

                if (currentStruct.directionsToAttack != null)//currentStruct null deðilse
                {
                    List<Dir> usedDirs = new List<Dir>();//ayný Dir'i tekrar kullanmamak için liste aç
                    System.Random rand = new System.Random();

                    foreach (Dir attackDir in currentStruct.directionsToAttack)
                    {
                        if (!usedDirs.Contains(attackDir) && projectiles != null)//ayný Dir deðilse ve atýcak mermi varsa
                        {
                            GameObject bulletInstantiated = Instantiate(projectiles[rand.Next(0, projectiles.Length)], barrel.barrelGO.transform.position, Quaternion.identity);//mermiyi oluþtur...
                            bulletInstantiated.GetComponent<EnemyBulletScript>().SetFlightVector(DirectionEnum.GetVector2DirFromEnum(attackDir), gridSize);//...hareket ettir...
                            bulletInstantiated.GetComponent<EnemyBulletScript>().SetParentGO(gameObject);//...parentini ayarla.

                            usedDirs.Add(attackDir);//bu dir artýk kullanýldý (ayný anda iki kez atmamasý için, waitTime geçtikten sonra bu silinicek)
                        }
                    }
                }

                if (phase > barrel.PAP.Length - 1 || phase < 0)//phasenin sonuna geldiysem yada error varsa
                    EAPSINDEXES[i] = 0;
                else if (eapsIndex == barrel.PAP[phase].ap.attackPatern.Length - 1)//attackPatternin sonuna geldiysem
                    EAPSINDEXES[i] = 0;
                else
                    EAPSINDEXES[i]++;//yoksa arttýr

                targetTimes[i] = currentStruct.waitAfter + Time.time;//tekrar ateþ edilecek zaman þu anki zamanýn waitTime kadar sonrasý
            }

            //AAAAAAAAAAAAAA
        }
    }
}
