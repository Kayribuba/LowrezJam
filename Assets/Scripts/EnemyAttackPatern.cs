using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] public struct EAPStruct
{
    public Dir[] directionsToAttack;
    public float waitAfter;
}


[CreateAssetMenu(fileName = "EnemyAttackPatern")]
public class EnemyAttackPatern : ScriptableObject
{
    public EAPStruct[] attackPatern;

}
