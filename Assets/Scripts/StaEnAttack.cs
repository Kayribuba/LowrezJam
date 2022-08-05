using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaEnAttack : MonoBehaviour
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] GameObject Barrel;
    [SerializeField] float barrelLenght = 1f;
    [SerializeField] EnemyAttackPatern eap;

    int eapsIndex = 0;
    float targetTime = float.MinValue;
    float gridSize;


    void Start()
    {
        gridSize = FindObjectOfType<GridSizer>().GetGridSize();
    }
    void Update()
    {
        if(targetTime <= Time.time)
        {
            EAPStruct currentStruct = eap.attackPatern[eapsIndex];

            List<Dir> usedDirs = new List<Dir>();
            System.Random rand = new System.Random();
            foreach(Dir attackDir in currentStruct.directionsToAttack)
            {
                if(!usedDirs.Contains(attackDir) && projectiles != null)
                {
                    Barrel.transform.position = Functions.AddVector2sTogether(transform.position, DirectionEnum.GetVector2DirFromEnum(attackDir) * barrelLenght);

                    Vector2 barrelCorrectorVector = Barrel.transform.position;
                    barrelCorrectorVector.x -= barrelCorrectorVector.x % gridSize;
                    barrelCorrectorVector.y -= barrelCorrectorVector.y % gridSize;
                    Barrel.transform.position = barrelCorrectorVector;

                    GameObject bulletInstantiated = Instantiate(projectiles[rand.Next(0, projectiles.Length)], Barrel.transform.position, Quaternion.identity);
                    bulletInstantiated.GetComponent<EnemyBulletScript>().SetFlightVector(DirectionEnum.GetVector2DirFromEnum(attackDir), gridSize);
                    bulletInstantiated.GetComponent<EnemyBulletScript>().SetParentGO(gameObject);

                    usedDirs.Add(attackDir);
                }
            }

            if (eapsIndex == eap.attackPatern.Length - 1)
                eapsIndex = 0;
            else
                eapsIndex++;

            targetTime = currentStruct.waitAfter + Time.time;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Barrel.transform.position, new Vector3(gridSize, gridSize, 1));
    }
}
