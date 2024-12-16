using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecableSpawner : MonoBehaviour
{
    [SerializeField] private GameObject log;
    //[SerializeField] private int woodAmount;
    private GameObject spawnedWood;
    public static CollecableSpawner instance;
    void Start()
    {
        instance = this;  
    }

    void Update()
    {
        
    }

    public void SpawnLog(Vector3 pos, int _amount)
    {
        spawnedWood = Instantiate(log, pos + new Vector3(0,1,0), transform.rotation);
        if(spawnedWood.TryGetComponent<Log>(out Log l))
        {
            l.amount = _amount;
        }
    }
}
