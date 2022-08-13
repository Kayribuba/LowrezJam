using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFFoodSpawn : MonoBehaviour
{
    [SerializeField] GameObject food;
    [SerializeField] float cd;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFood();
    }
    public void SpawnFood()
    {
        StartCoroutine(SpawnAfter());
    }
    IEnumerator SpawnAfter()
    {
        yield return new WaitForSeconds(cd);
        System.Random rn = new System.Random();
        Instantiate(food, new Vector3(rn.Next(-1,1), rn.Next(-1, 1), 0), Quaternion.identity);
    }


}
