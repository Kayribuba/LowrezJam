using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] struct AEASBarrel
{
    public GameObject barrel;
    public EnemyAttackPatern[] attackPatterns;
}

public class AdvancedEnemyAttackScript : MonoBehaviour
{
    [SerializeField] AEASBarrel[] barrels;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
