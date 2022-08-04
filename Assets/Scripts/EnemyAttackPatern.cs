using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyAttackPatern")]
public class EnemyAttackPatern : ScriptableObject
{
    [SerializeField] struct EAPStruct
    {
        public Dir[] directionsToAttack;

    }
}
