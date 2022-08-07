using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointer : MonoBehaviour
{
    void Start()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = playerTransform.position;
    }

}
