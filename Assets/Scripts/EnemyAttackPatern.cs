using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] struct EAPStruct
{
    public Dir[] directionsToAttack;
    public float waitAfter;
}


[CreateAssetMenu(fileName = "EnemyAttackPatern")]
public class EnemyAttackPatern : ScriptableObject
{
    [SerializeField] EAPStruct[] enemyAttackPatern;

}
